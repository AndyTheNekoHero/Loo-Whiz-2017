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
        WAITING_TO_SPAWN,
        DRAW_L,
        DRAW_R,
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

    ObjectPool NPC;
    Spawner Spwn;
    EnviManager EnviMag;
    private Animator anim;
    //private float countdown = 0.0f;

    // Use this for initialization
    void Start ()
    {
        State = C_STATE.WALK;
        Stop = false;
        BodyMovement = GetComponent<Rigidbody2D>();
        NPC = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        Spwn = GameObject.Find("Spawner").GetComponent<Spawner>();
        EnviMag = GameObject.Find("Envi GameObject").GetComponent<EnviManager>();
        transform.position = Spwn.transform.position;
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(ProcessState());
        WaypointEnded();
        //Debug.Log("Waypoint: " + WaypointEnded());
        Debug.Log(State.ToString());
    }

    public IEnumerator AnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        switch (State)
        {
            case C_STATE.WASH:
                {
                    anim.SetBool("Washing", false);
                    State = C_STATE.EXIT;
                }
                break;
            case C_STATE.PEE:
                {
                    anim.SetBool("Peeing", false);
                    State = C_STATE.WASH;
                }
                break;
            default:
                break;
        }

    }

    void MoveToWaypoint()
    {
        //distance between point A and point B
        float dist = Vector2.Distance(test[currentPoint].position, transform.position);
        Vector2 dir = (test[currentPoint].position - transform.position).normalized;

        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);

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
    }

    public bool WaypointEnded()
    {
        if (currentPoint >= test.Count )
        {
            currentPoint = test.Count ;
            return true;
        }
        else
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
                    NPC_Exit();
                }
                break;
            case C_STATE.WAITING_TO_SPAWN:
                {
                    transform.position = Spwn.transform.position;
                    currentPoint = 0;
                    test.Clear();
                    State = C_STATE.WALK;   
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
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Pee");
        //add child
        if(!Stop)
            AddChild();
        //Move in the path
       MoveToWaypoint();
        if (WaypointEnded() && State == C_STATE.PEE)
        {
            Stop = false;
            //run animation here
            anim.SetBool("Peeing", true);
            //Animation Ended
            StartCoroutine(AnimEnded());
        }

    }
    //go to cubicle
    public void Shit()
    { 
        //Change path to Pee
       ParentToTakeFrom = GameObject.Find("Shit");
        //add child
        AddChild();
        //Move in the path
       MoveToWaypoint();
        if (WaypointEnded() && State == C_STATE.SHIT)
        {
            Stop = false;
            //run animation here

            //if(animation ended) wash hand
            State = C_STATE.WASH;
        }

    }
    //go to basin
    public void Wash()
    {
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Wash");
        //add child
        if (!Stop)
            AddChild();
        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            Stop = false;
            anim.SetBool("Washing", true);
            StartCoroutine(AnimEnded());
        }

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

        if (WaypointEnded())
        {
            Stop = false;
            RNG_Path = Random.Range(1, 1);
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
                //case 4:
                //    {
                //        //Draw Right
                //        State = C_STATE.SHIT;
                //    }
                //    break;
                //case 5:
                //    {
                //        //go to basin
                //        State = C_STATE.WASH;
                //    }
                   // break;
                default:
                    break;
            }
        }
        //Debug.Log(test.Count);
        //Move to target path


    }

    public void NPC_Exit()
    {
        SR.flipX = true;
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Exit");
        Debug.Log(Stop);
        //add child
        if (!Stop)
            AddChild();

        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.EXIT)
        {
            Stop = false;
            //run animation here

           //Delete
            NPC.ReturnObject(gameObject);
            GlobalVar.Instance.CustomerCount--;
            State = C_STATE.WAITING_TO_SPAWN;
        }

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
