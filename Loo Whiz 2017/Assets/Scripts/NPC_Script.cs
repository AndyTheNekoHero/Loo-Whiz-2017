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
        DRAW,
        EXIT,
    };

    C_STATE State;
    public GameObject ParentToTakeFrom;

    bool Stop;
    public int RNG_Path;
    public List<Transform> Waypoint = new List<Transform>();
    public float speed = 2f;
    private float reachDist = 0.3f;
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
<<<<<<< HEAD
        //Debug.Log(currentPoint + " " + Waypoint.Count);
        //Debug.Log(Stop);
=======

        #region Debug Fast Forward
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Time.timeScale = 2f;
        }
        else
            Time.timeScale = 1f;
        #endregion

        if (ParentToTakeFrom == null)
        {
            Debug.LogWarning("Error404! GameObject Not Found! Ending Proramme Now!");
            Application.Quit();
        }
        //Debug.Log(currentPoint + " " + Waypoint.Count);
>>>>>>> 47185636cdb9705ebdb0314d49a396d3d827a780
    }

    #region Animation Ended
    public IEnumerator PeeAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (WaypointEnded() && State == C_STATE.PEE)
        {
            anim.SetBool("Peeing", false);
            WalkToSink();
            UrinalUnOccupy();
            State = C_STATE.WASH;
        }

    }
    public IEnumerator ShitAnimEnded()
    {
        yield return new WaitForSeconds(2f);

        if (WaypointEnded() && State == C_STATE.SHIT)
        {
            WalkToSink();
            BowlUnOccupy();
            State = C_STATE.WASH;
        }

    }
    public IEnumerator WashAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            anim.SetBool("Washing", false);
            SinkUnOccupy();
            ParentToTakeFrom = GameObject.Find("Exit");
            AddChild();
            State = C_STATE.EXIT;
        }
    }
    public IEnumerator DrawAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (WaypointEnded() && State == C_STATE.DRAW)
        {
            anim.SetBool("Drawing", false);
            ParentToTakeFrom = GameObject.Find("Exit");
            AddChild();
            State = C_STATE.EXIT;
        }
    }
    #endregion

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
        if (dist <= reachDist)
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
            case C_STATE.DRAW:
                {
                    Draw();
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
        //ParentToTakeFrom = GameObject.Find("Pee");
        if (!Stop)
        {
            WalkToPee();
        }
        //Move in the path
        MoveToWaypoint();
        if (WaypointEnded() && State == C_STATE.PEE)
        {
<<<<<<< HEAD
            //Stop = false;
=======
>>>>>>> 47185636cdb9705ebdb0314d49a396d3d827a780
            //run animation here
            anim.SetBool("Peeing", true);
            //Animation Ended
            StartCoroutine(PeeAnimEnded());

        }

    }
    //go to cubicle
    public void Shit()
    {
        //Change path to Cubicle
        if (!Stop)
        {
            WalkToShit();
        }
        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.SHIT)
        {
            //run animation here
            StartCoroutine(ShitAnimEnded());
        }

    }
    //go to basin
    public void Wash()
    {
        //Change path to wash
        //add child
<<<<<<< HEAD
        if (!Stop)
        {
            WalkToSink();
        }
=======
>>>>>>> 47185636cdb9705ebdb0314d49a396d3d827a780
        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            Stop = false;
            anim.SetBool("Washing", true);
            StartCoroutine(WashAnimEnded());
        }

    }
    //Go to wall
    public void Draw()
    {
        if (!Stop)
        {
            WalkToWall();
        }
        //Move in the path
        MoveToWaypoint();

        if (WaypointEnded() && State == C_STATE.DRAW )
        {
            //run animation here
            anim.SetBool("Drawing", true);
            //Animation Ended
            StartCoroutine(DrawAnimEnded());

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
            
            //Random Path
            if (EnviManager.Instance.UrinalAllFull()) { RNG_Path = Random.Range(2, 4); }
            else if (EnviManager.Instance.AllDrawn()) { RNG_Path =  Random.Range(1, 3); }
            else
                RNG_Path = Random.Range(1, 4);

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
                        //Draw
                        State = C_STATE.DRAW;
                    }
                    break;
<<<<<<< HEAD
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
=======
               case 4:
                   {
                       //go to basin
                       State = C_STATE.WASH;
                   }
                break;
>>>>>>> 47185636cdb9705ebdb0314d49a396d3d827a780
                default:
                    break;
            }
        }

        //Move to target path


    }

    public void NPC_Exit()
    {
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


    #region For Walk

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
<<<<<<< HEAD
}
=======

    public void WalkToShit()
    {
        ////Getting One Cubicle
        if (EnviManager.Instance.GetEmptyBowlSlots() >= 0)
        {
            Waypoint.Add(EnviManager.Instance.GetEmptyBowl());
        }

        Stop = true;
    }

    public void WalkToWall()
    {
        ////Getting One Cubicle
        if (EnviManager.Instance.GetEmptyWallSlots() >= 0)
        {
            Waypoint.Add(EnviManager.Instance.GetEmptyWall());
        }

        Stop = true;
    }

    #endregion

    #region UnOccupy

    private void UrinalUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<Urinal>())
            {
                Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<Urinal>().UnOccupy();
            }
        }
    }
    private void SinkUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<Sink>())
            {
                Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<Sink>().UnOccupy();
            }
        }
    }
    private void BowlUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<ToiletBowl>())
            {
                Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<ToiletBowl>().UnOccupy();
            }
        }
    }
    #endregion
}
>>>>>>> 47185636cdb9705ebdb0314d49a396d3d827a780
