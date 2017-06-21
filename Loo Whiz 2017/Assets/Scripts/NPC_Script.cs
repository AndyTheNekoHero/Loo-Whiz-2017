using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Script : MonoBehaviour
{

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
    private Vector2 dir;
    public GameObject ParentToTakeFrom;

    bool Stop;
    public int RNG_Path;
    public List<Transform> Waypoint = new List<Transform>();
    public float speed = 2f;
    public float reachDist = 0.1f;
    public int currentPoint = 0;
    Rigidbody2D BodyMovement;

    ObjectPool NPC;
    Spawner Spwn;
    private Animator anim;

    //private float countdown = 0.0f;

    // Use this for initialization
    void Start()
    {
        State = C_STATE.WALK;
        Stop = false;
        BodyMovement = GetComponent<Rigidbody2D>();
        NPC = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        Spwn = GameObject.Find("Spawner").GetComponent<Spawner>();
        transform.position = Spwn.transform.position;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ProcessState());
        WaypointEnded();
        Debug.Log(currentPoint + " " + Waypoint.Count);
        Debug.Log(Stop);
    }

    public IEnumerator PeeAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (WaypointEnded() && State == C_STATE.PEE)
        {
            anim.SetBool("Peeing", false);
            State = C_STATE.WASH;
        }

    }
    public IEnumerator WashAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            anim.SetBool("Washing", false);
            State = C_STATE.EXIT;
        }
    }

    void MoveToWaypoint()
    {
        //distance between point A and point B
        if (currentPoint >= Waypoint.Count)
            return;

        float dist = Vector2.Distance(Waypoint[currentPoint].position, transform.position);
        Vector2 dir = (Waypoint[currentPoint].position - transform.position).normalized;

        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);

        //Move to waypoint
        //transform.position = Vector2.Lerp(transform.position, test[currentPoint].position, Time.deltaTime * speed);

        if (dist > reachDist)
        {
            BodyMovement.velocity = dir * speed;
        }
        else if (dist <= reachDist)
        {
            BodyMovement.velocity = Vector2.zero;
            currentPoint++;
        }
    }

    public bool WaypointEnded()
    {
        if (currentPoint >= Waypoint.Count)
        {
            currentPoint = Waypoint.Count;
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
                    Wash();
                }
                break;
            case C_STATE.WALK:
                {
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
                    Waypoint.Clear();
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
        //ParentToTakeFrom = GameObject.Find("Sinks");
        if (!Stop)
        {
            WalkToPee();
        }
        //Move in the path
        MoveToWaypoint();
        if (WaypointEnded() && State == C_STATE.PEE)
        {
            //Stop = false;
            //run animation here
            anim.SetBool("Peeing", true);
            //Animation Ended
            StartCoroutine(PeeAnimEnded());

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
        //Change path to wash
        //ParentToTakeFrom = GameObject.Find("Sinks");
        //add child
        if (!Stop)
        {
            WalkToSink();  
        }
        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            Stop = false;
            anim.SetBool("Washing", true);
            StartCoroutine(WashAnimEnded());
        }

    }
    //walking
    public void Walk()
    {
        //Change path to Walk
        ParentToTakeFrom = GameObject.Find("Enter");
        //Add Child
        if (!Stop)
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
        }

        //Move to target path


    }

    public void NPC_Exit()
    {
        //Stop = false;
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Exit");
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
            Waypoint.Add(child);
        }
        Stop = true;
    }

    public void WalkToPee()
    {
        ////Getting One Urinal
        if (EnviManager.Instance.GetEmptyUrinalSlots() >= 0)
        {
            Waypoint.Add(EnviManager.Instance.GetEmptyUrinal());
        }
        Stop = true;
    }
    public void WalkToSink()
    {
        //Getting One Sink
        if (EnviManager.Instance.GetEmptySinkSlots() >= 0 && EnviManager.Instance.SinkList != null)
        {
            Waypoint.Add(EnviManager.Instance.GetEmptySink());
        }
        Stop = true;
    }
}
