using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Advent.Dungeons;

[CustomEditor(typeof(BoardGenerator))]
public class GenerateBoardCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BoardGenerator myScript = (BoardGenerator)target;
        if (GUILayout.Button("Generate Board"))
        {
            myScript.ClearAllTiles();
            myScript.SetupBoard();
        }
    }
}
