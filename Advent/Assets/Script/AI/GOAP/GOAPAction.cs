using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour
{

    private HashSet<KeyValuePair<string, object>> preconditions;
    private HashSet<KeyValuePair<string, object>> effects;

    private bool inRange = false;

    /* The cost of performing the action. 
	 * Figure out a weight that suits the action. 
	 * Changing it will affect what actions are chosen during planning.*/
    public float cost = 1f;

    /**
	 * An action often has to perform on an object. This is that object. Can be null. */
    public GameObject target;

    public GOAPAction()
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void doReset()
    {
        inRange = false;
        target = null;
        _Reset();
    }

    /**
	 * Reset any variables that need to be reset before planning happens again.
	 */
    public abstract void _Reset();

    /**
	 * Is the action done?
	 */
    public abstract bool IsDone();

    /**
	 * Procedurally check if this action can run. Not all actions
	 * will need this, but some might.
	 */
    public abstract bool CheckProceduralPrecondition(GameObject agent);

    /**
	 * Run the action.
	 * Returns True if the action performed successfully or false
	 * if something happened and it can no longer perform. In this case
	 * the action queue should clear out and the goal cannot be reached.
	 */
    public abstract bool Perform(GameObject agent);

    /**
	 * Does this action need to be within range of a target game object?
	 * If not then the moveTo state will not need to run for this action.
	 */
    public abstract bool requiresInRange();


    /**
	 * Are we in range of the target?
	 * The MoveTo state will set this and it gets reset each time this action is performed.
	 */
    public bool IsInRange()
    {
        return inRange;
    }

    public void SetInRange(bool inRange)
    {
        this.inRange = inRange;
    }


    public void AddPrecondition(string key, object value)
    {
        Preconditions.Add(new KeyValuePair<string, object>(key, value));
    }


    public void RemovePrecondition(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in Preconditions)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            Preconditions.Remove(remove);
    }


    public void AddEffect(string key, object value)
    {
        Effects.Add(new KeyValuePair<string, object>(key, value));
    }


    public void RemoveEffect(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in Effects)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            Effects.Remove(remove);
    }

    public HashSet<KeyValuePair<string, object>> Preconditions
    {
        get
        {
            return preconditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effects
    {
        get
        {
            return effects;
        }
    }
}
