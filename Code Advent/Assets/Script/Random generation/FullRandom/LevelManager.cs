using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Generation")]
    public Vector2 roomSizeWorldUnits = new Vector2(25, 25);
    public int maxWalker = 3;
    public GameObject wallObj, floorObj;
    public GameObject startPosition;
    public GameObject exitPosition;
    public GameObject[] enemies;
    public int maxEnemyCount; // ? You can use scriptable objects on some of this variables instead
    //public GameObject abilitySkillUI;
    public Canvas canvas;
    public Camera mainCamera;
    [Space]
    //Change to scriptable objects
    public GameObject[] items;

    [HideInInspector]
    public int enemyCount;

    enum gridSpace { empty,floor,Wall}; //Add door ?
    gridSpace[,] grid;
    int roomHeight, roomwidth;
    float worldUnitsInOneGridCell = 1;
    struct walker
    {
        public Vector2 dir;
        public Vector2 pos;
    }
    List<walker> walkers;
    List<Vector2> floorList;
    float chanceWalkerChangeDir = 0.5f, chanceWalkerSpawn = 0.05f;
    float chanceWalkerDestroy = 0.05f;
    float percentToFill = 0.2f;

    PlayerController player;
    private void Start()
    {
        Setup();
        CreateFloors();
        CreateWalls();
        SpawnLevel();
        SpawnStartingPoint();
        SpawnEndPoint();
        SpawnEnemies();
        SpawnUI();
        SpawnItems();

        player = PlayerController.instance;
        Instantiate(mainCamera);

        enemyCount = maxEnemyCount;
    }
    void Update()
    {

    }
    private void LateUpdate()
    {
        CheckIfAllEnemyIsDestroyed();
    }
    void CheckIfAllEnemyIsDestroyed()
    {
        if(enemyCount == 0)
        {
            exitPosition.SetActive(true);
        }
    }
    public void Setup()
    {
        //find grid size
        roomHeight = Mathf.RoundToInt(roomSizeWorldUnits.x / worldUnitsInOneGridCell);
        roomwidth = Mathf.RoundToInt(roomSizeWorldUnits.y / worldUnitsInOneGridCell);
        //create grid
        grid = new gridSpace[roomwidth, roomHeight];
        //set grid's default state
        for (int x = 0; x < roomwidth - 1; x++)
        {
            for (int y = 0; y < roomHeight -1; y++)
            {
                //make every cell "Empty";
                grid[x, y] = gridSpace.empty;
            }
        }
        //set first walker
        //init list
        walkers = new List<walker>();
        //create walker
        walker newWalker = new walker();
        newWalker.dir = RandomDirection();
        //find center of grid
        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(roomwidth / 2.0f), Mathf.RoundToInt(roomHeight / 2.0f));
        newWalker.pos = spawnPos;
        //add walker to list
        walkers.Add(newWalker);
        //init list of floor that has been instantiated
        floorList = new List<Vector2>();
    }
    public void CreateFloors()
    {
        int iterations = 0; //loop will not run forever
        do
        {
            //create floor at position of every walker
            foreach(walker myWalker in walkers)
            {
                grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = gridSpace.floor;
            }

            //chance destroy walker
            int numberChecks = walkers.Count; // might modify count while in this loop
            for (int i = 0; i < numberChecks; i++)
            {
                //only if its not the only one and at a low chance
                if(Random.value < chanceWalkerDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break; //destroy only one per iteration
                }
            }
            //chance walker pick new direction
            for (int i = 0; i < walkers.Count; i++)
            {
                if(Random.value < chanceWalkerChangeDir)
                {
                    walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }
            // chance pick new walker
            numberChecks = walkers.Count;// might modify while in this loop
            for (int i = 0; i < numberChecks; i++)
            {
                //only if # of walkers < max and at a low chance
                if(Random.value < chanceWalkerSpawn && walkers.Count < maxWalker)
                {
                    //create new walker
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }
            // move walkers; 
            for (int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                walkers[i] = thisWalker;
            }
            //avoid border of grid
            for (int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                //clamp x and y to leave a 1 space border : leave room for walls 
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomwidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }
            //check to exit loop
            if((float)NumberOfFloors() / (float)grid.Length > percentToFill)
            {
                break;
            }
            iterations++;
        } while (iterations < 10000);
    }
    public void SpawnLevel()
    {
        for (int x = 0; x < roomwidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                switch (grid[x, y])
                {
                    case gridSpace.empty:
                        Spawn(x, y, wallObj);
                        //can spawn here to closed the level
                        break;
                    case gridSpace.floor:
                        Spawn(x, y, floorObj);
                        floorList.Add(new Vector2(x, y)); //Get the floor position and save it to a list to use for spawning 
                        break;
                    case gridSpace.Wall:
                        Spawn(x, y, wallObj);
                        break;
                }
            }
        }
    }
    public void SpawnStartingPoint()
    {//Spawn the character
        int rand = Random.Range(0, floorList.Count);
        Vector2 offset = roomSizeWorldUnits / 2.0f;
        Vector2 spawnPos = floorList[rand] * worldUnitsInOneGridCell - offset;
        Instantiate(startPosition, spawnPos, Quaternion.identity);
    }
    public void SpawnEndPoint()
    {//spawns the end point where the player can move to the next stage;
        int rand = Random.Range(0, floorList.Count);
        Vector2 offset = roomSizeWorldUnits / 2.0f;
        Vector2 spawnPos = floorList[rand] * worldUnitsInOneGridCell - offset;
        exitPosition = Instantiate(exitPosition, spawnPos, Quaternion.identity);
        exitPosition.SetActive(false);
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemyCount; i++)
        {
            int rand = Random.Range(0, floorList.Count);
            Vector2 offset = roomSizeWorldUnits / 2.0f;
            Vector2 spawnPos = floorList[rand] * worldUnitsInOneGridCell - offset;
            for (int y = 0; y < enemies.Length; y++)
            {
                Instantiate(enemies[y], spawnPos, Quaternion.identity);
            }
        }
    }
    public void SpawnUI()
    {
        Instantiate(canvas, Vector3.zero, Quaternion.identity);
    }
    public void CreateWalls()
    {
        //loop though every grid space
        for (int x = 0; x < roomwidth -1; x++)
        {
            for (int y = 0; y < roomHeight -1; y++)
            {
                //if theres a floor, check the spaces around it
                if(grid[x,y] == gridSpace.floor)
                {
                    //if any surrounding spaces are empty, place a wall
                    if(grid[x,y + 1] == gridSpace.empty)
                    {
                        grid[x, y + 1] = gridSpace.Wall;
                    }
                    if (grid[x, y -1] == gridSpace.empty)
                    {
                        grid[x, y - 1] = gridSpace.Wall;
                    }
                    if (grid[x +1, y] == gridSpace.empty)
                    {
                        grid[x + 1, y] = gridSpace.Wall;
                    }
                    if (grid[x - 1, y] == gridSpace.empty)
                    {
                        grid[x -1,y] = gridSpace.Wall;
                    }
                }
            }
        }
    }
    public void Spawn(float x, float y, GameObject go)
    {
        //find position to spawn
        Vector2 offset = roomSizeWorldUnits / 2.0f;
        Vector2 spawnPos = new Vector2(x, y) * worldUnitsInOneGridCell - offset;
        //spawn object
        Instantiate(go, spawnPos, Quaternion.identity,transform);
    }
    public void SpawnItems()
    {
        //Spawning Items on random location;
        //You can spawn chest instead of item itself
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, floorList.Count);
            Vector2 offset = roomSizeWorldUnits / 2.0f;
            Vector2 spawnPos = floorList[rand] * worldUnitsInOneGridCell - offset;
            Instantiate(items[0], spawnPos, Quaternion.identity);
        }
    }
    public int NumberOfFloors()
    {
        int count = 0;
        foreach (gridSpace space in grid)
        {
            if(space == gridSpace.floor)
            {
                count++;
            }
        }
        return count;
    }

    public Vector2 RandomDirection()
    {
        //pick random int between 0 and 3
        int choice = Mathf.FloorToInt(Random.value * 3.99f);
        //use that int to choice direction
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right; 
        }
    }
}
