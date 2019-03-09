using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GOAPAgent : MonoBehaviour
{

    private FSM stateMachine;

    private FSM.FSMState idleState; // finds something to do
    private FSM.FSMState moveToState; // moves to a target
    private FSM.FSMState performActionState; // performs an action

    private HashSet<GOAPAction> availableActions;
    private Queue<GOAPAction> currentActions;

    private IGOAP dataProvider; // this is the implementing class that provides our world data and listens to feedback on planning

    private GOAPPlanner planner;


    void Start()
    {
        stateMachine = new FSM();
        availableActions = new HashSet<GOAPAction>();
        currentActions = new Queue<GOAPAction>();
        planner = new GOAPPlanner();
        FindDataProvider();
        CreateIdleState();
        CreateMoveToState();
        CreatePerformActionState();
        stateMachine.pushState(idleState);
        LoadActions();
    }


    void Update()
    {
        stateMachine.Update(this.gameObject);
    }


    public void AddAction(GOAPAction a)
    {
        availableActions.Add(a);
    }

    public GOAPAction GetAction(Type action)
    {
        foreach (GOAPAction g in availableActions)
        {
            if (g.GetType().Equals(action))
                return g;
        }
        return null;
    }

    public void RemoveAction(GOAPAction action)
    {
        availableActions.Remove(action);
    }

    private bool HasActionPlan()
    {
        return currentActions.Count > 0;
    }

    private void CreateIdleState()
    {
        idleState = (fsm, gameObj) => {
            // GOAP planning

            // get the world state and the goal we want to plan for
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorldState();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoalState();

            // Plan
            Queue<GOAPAction> plan = planner.Plan(gameObject, availableActions, worldState, goal);
            if (plan != null)
            {
                // we have a plan, hooray!
                currentActions = plan;
                dataProvider.planFound(goal, plan);

                fsm.popState(); // move to PerformAction state
                fsm.pushState(performActionState);

            }
            else
            {
                // ugh, we couldn't get a plan
                Debug.Log("<color=orange>Failed Plan:</color>" + PrettyPrint(goal));
                dataProvider.planFailed(goal);
                fsm.popState(); // move back to IdleAction state
                fsm.pushState(idleState);
            }

        };
    }

    private void CreateMoveToState()
    {
        moveToState = (fsm, gameObj) => {
            // move the game object

            GOAPAction action = currentActions.Peek();
            if (action.requiresInRange() && action.target == null)
            {
                Debug.Log("<color=red>Fatal error:</color> Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
                fsm.popState(); // move
                fsm.popState(); // perform
                fsm.pushState(idleState);
                return;
            }

            // get the agent to move itself
            if (dataProvider.moveAgent(action))
            {
                fsm.popState();
            }

            /*MovableComponent movable = (MovableComponent) gameObj.GetComponent(typeof(MovableComponent));
			if (movable == null) {
				Debug.Log("<color=red>Fatal error:</color> Trying to move an Agent that doesn't have a MovableComponent. Please give it one.");
				fsm.popState(); // move
				fsm.popState(); // perform
				fsm.pushState(idleState);
				return;
			}
			float step = movable.moveSpeed * Time.deltaTime;
			gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position, action.target.transform.position, step);
			if (gameObj.transform.position.Equals(action.target.transform.position) ) {
				// we are at the target location, we are done
				action.setInRange(true);
				fsm.popState();
			}*/
        };
    }

    private void CreatePerformActionState()
    {
        performActionState = (fsm, obj) => {

            if (!HasActionPlan())
            {
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.actionsFinished();
                return;
            }

            GOAPAction action = currentActions.Peek();
            if (action.IsDone())
            {
                currentActions.Dequeue();
            }

            if (HasActionPlan())
            {
                action = currentActions.Peek();
                bool inRange = action.requiresInRange() ? action.IsInRange() : true;

                if (inRange)
                {
                    bool success = action.Perform(obj);
                    if (!success)
                    {
                        fsm.popState();
                        fsm.pushState(idleState);
                        CreateIdleState();
                        dataProvider.planAborted(action);
                    }
                }
                else
                {
                    fsm.pushState(moveToState);
                }
            }
            else
            {
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.actionsFinished();
            }
        };
    }

    private void FindDataProvider()
    {
        foreach (Component comp in gameObject.GetComponents(typeof(Component)))
        {
            if (typeof(IGOAP).IsAssignableFrom(comp.GetType()))
            {
                dataProvider = (IGOAP)comp;
                return;
            }
        }
    }

    private void LoadActions()
    {
        GOAPAction[] actions = gameObject.GetComponents<GOAPAction>();
        foreach (GOAPAction a in actions)
        {
            availableActions.Add(a);
        }
        Debug.Log("Found actions: " + PrettyPrint(actions));
    }

    public static string PrettyPrint(HashSet<KeyValuePair<string, object>> state)
    {
        String s = "";
        foreach (KeyValuePair<string, object> kvp in state)
        {
            s += kvp.Key + ":" + kvp.Value.ToString();
            s += ", ";
        }
        return s;
    }

    public static string PrettyPrint(Queue<GOAPAction> actions)
    {
        String s = "";
        foreach (GOAPAction a in actions)
        {
            s += a.GetType().Name;
            s += "-> ";
        }
        s += "GOAL";
        return s;
    }

    public static string PrettyPrint(GOAPAction[] actions)
    {
        String s = "";
        foreach (GOAPAction a in actions)
        {
            s += a.GetType().Name;
            s += ", ";
        }
        return s;
    }

    public static string PrettyPrint(GOAPAction action)
    {
        String s = "" + action.GetType().Name;
        return s;
    }
}