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

    bool Stop;
    public int RNG_Path;
    public List<Transform> test = new List<Transform>();

    public float speed = 2f;
    public float reachDist = 0.1f;
    public int currentPoint = 0;
    Rigidbody2D BodyMovement;

    // Use this for initialization
    void Start ()
    {
        State = C_STATE.WALK;
        Stop = false;
        BodyMovement = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(ProcessState());
	}

    void MoveToWaypoint()
    {
        //distance between point A and point B
        float dist = Vector2.Distance(test[currentPoint].position, transform.position);
        Vector2 dir = (test[currentPoint].position - transform.position).normalized;

        //Move to waypoint
        //transform.position = Vector2.Lerp(transform.position, test[currentPoint].position, Time.deltaTime * speed);

        if (dist > reachDist)
        {
            BodyMovement.AddForce(dir * speed);
        }
        else if (dist <= reachDist)
        {
            BodyMovement.velocity = Vector2.zero;
            currentPoint++;
        }
        WaypointEnded();
    }

    public bool WaypointEnded()
    {
        if (currentPoint >= test.Count - 1)
        {
            currentPoint = test.Count - 1;
            return true;
        }
        return false;
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
        if(!Stop)
            AddChild();
        //Move in the path
       MoveToWaypoint();
        if (WaypointEnded())
        {
            Stop = false;
            //run animation here


            //if(animation ended) wash hand
            State = C_STATE.WASH;
        }

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
       MoveToWaypoint();

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
        MoveToWaypoint();

    }
    //walking
    public void Walk()
    {
        //Change path to Walk
       ParentToTakeFrom = GameObject.Find("Enter");
        //Add Child
        if(!Stop)
           AddChild();

         MoveToWaypoint();

        if (!WaypointEnded())
        {
            Stop = false;
            RNG_Path = Random.Range(1, 5);
            switch (RNG_Path)
            {
                case 1:
                    {
                        //go to urinal
                        State = C_STATE.PEE;
                    }
                    break;
                case 2:
                    {
                        //go to cubicle
                        State = C_STATE.SHIT;
                    }
                    break;
                case 3:
                    {
                        //Draw Left
                        State = C_STATE.SHIT;
                    }
                    break;
                case 4:
                    {
                        //Draw Right
                        State = C_STATE.SHIT;
                    }
                    break;
                case 5:
                    {
                        //go to basin
                        State = C_STATE.WASH;
                    }
                    break;
                default:
                    break;
            }
            Debug.Log("IT ENDED, Set Path to " + ParentToTakeFrom.name);
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
        Stop = true;
    }
}
