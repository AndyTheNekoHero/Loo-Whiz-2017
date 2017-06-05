using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Script : MonoBehaviour {

    public enum C_STATE
    {
        WALK = 1,
        PEE,
        SHIT,
        WASH,
        DRAW,
        EXIT,
    };

    C_STATE State;
    public SpriteRenderer SR;
    private Vector2 dir;
    public GameObject ParentToTakeFrom;

    bool tests;

    public List<Transform> test = new List<Transform>();

    // Use this for initialization
    void Start () {
        State = C_STATE.WALK;
        test = PathToFollow.pathParent;
        tests = false;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ProcessState());
	}

    public IEnumerator ProcessState()
    {
        switch (State)
        {
            case C_STATE.PEE:
                { 
                    SR.flipX = false;
                    Pee();
                }
                break;
            case C_STATE.SHIT:
                {
                    Shit();
                }
                break;
            case C_STATE.WASH:
                {
                    SR.flipX = true;
                    Wash();
                }
                break;
            case C_STATE.WALK:
                {
                    SR.flipX = true;
                    Walk();
                }
                break;
            case C_STATE.EXIT:
                {
                    //Customer go out
                }
                break;
            default:
                break;
        }
        yield return null;
    }
    //go to urinal
    public void Pee()
    {
        SR.flipX = false;
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Pee");
        //add child
        if(!tests)
            AddChild();
        //Move in the path
        PathToFollow.Instance.MoveToWaypoint();

    }
    //go to cubicle
    public void Shit()
    {
        SR.flipX = false;
        //Change path to Pee
       ParentToTakeFrom = GameObject.Find("Shit");
        //add child
        AddChild();
        //Move in the path
        PathToFollow.Instance.MoveToWaypoint();

    }
    //go to basin
    public void Wash()
    {
        SR.flipX = false;
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Wash");
        //add child
        AddChild();
        //Move in the path
        PathToFollow.Instance.MoveToWaypoint();

    }
    //walking
    public void Walk()
    {
        //Change path to Walk
       ParentToTakeFrom = GameObject.Find("Enter");
        //Add Child
        if(!tests)
           AddChild();

         PathToFollow.Instance.MoveToWaypoint();
        if (PathToFollow.Instance.WaypointEnded())
        {
            Debug.Log("IT ENDED");
        }
        //Debug.Log(test.Count);
        //Move to target path


    }

    //Loop to add child
    public void AddChild()
    {
        foreach (Transform child in ParentToTakeFrom.transform)
        {
            test.Add(child);
        }
        tests = true;
    }
}
