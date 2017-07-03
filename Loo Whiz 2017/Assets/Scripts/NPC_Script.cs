﻿using System.Collections;
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
        ANGRY,
        EXIT,
    };

    C_STATE State, tempState;
    public GameObject ParentToTakeFrom;
    [SerializeField]
    private float peeTime = 10;
    [SerializeField]
    private float shitTime = 4;
    [SerializeField]
    private float washTime = 3;
    [SerializeField]
    private float angryTime = 8;

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
        ProcessState();
        MoveToWaypoint();

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
    }

    #region Animation Ended

    public IEnumerator PeeAnimEnded()
    {
        float delay = 0;
        while (delay < peeTime)
        {
            delay += Time.deltaTime;
            yield return null;
        }

        if (WaypointEnded() && State == C_STATE.PEE)
        {
            anim.SetBool("Peeing", false);
            UrinalUnOccupy();

            int r = Random.Range(1, 100);
            if (r <= 10)
            {
                //spawn mess
                CreatedPeeMess();
                GameObject EnviroPee = (GameObject)Instantiate(Resources.Load("Pee"), (Waypoint[Waypoint.Count - 1].GetComponent<Urinal>().transform));
                EnviroPee.transform.position = (EnviroPee.transform.position + new Vector3(3.6f, -8.0f, 0));
                EnviroPee.transform.localScale = new Vector2(2.5f, 4.0f);
            }


            if (EnviManager.Instance.GetEmptySinkSlots() <= 0)
            {
                ParentToTakeFrom = GameObject.Find("Exit");
                AddChild();
                ChangeState(C_STATE.EXIT);
            }
            else
            {
                ChangeState(C_STATE.WASH);
                WalkToSink();
            }
        }
        yield break;
    }

    public IEnumerator AngryAnimEnded()
    {
        float delay = 0;
        while (delay < angryTime)
        {
            delay += Time.deltaTime;
            if (EnviManager.Instance.UrinalMess(Waypoint[Waypoint.Count - 1].GetComponent<Urinal>()) == false && State == C_STATE.PEE)
            {
                anim.SetBool("Angry", false);
                anim.SetBool("Peeing", true);
                StartCoroutine(PeeAnimEnded());
                yield break;
            }

            if (EnviManager.Instance.ShitMess(Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>()) == false && EnviManager.Instance.RollMess(Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>()) == false && State == C_STATE.SHIT)
            {
                anim.SetBool("Angry", false);
                StartCoroutine(ShitAnimEnded());
                yield break;
            }
            if (EnviManager.Instance.SinkMess(Waypoint[Waypoint.Count - 1].GetComponent<Sink>()) == false && State == C_STATE.WASH)
            {
                anim.SetBool("Angry", false);
                anim.SetBool("Washing", true);
                StartCoroutine(WashAnimEnded());
                yield break;
            }

            if (Waypoint[Waypoint.Count - 1].GetComponent<Urinal>() && Waypoint[Waypoint.Count - 1].GetComponent<Urinal>().PathIsBlocked() == false && anim.GetBool("Angry") == true)
            {
                anim.SetBool("Angry", false);
                reachDist = 0.3f;
                currentPoint -= 1;
                if (tempState == C_STATE.PEE)
                {
                    ChangeState(C_STATE.PEE);
                }
                yield break;
            }
            if (Waypoint[Waypoint.Count - 1].GetComponent<Sink>() && Waypoint[Waypoint.Count - 1].GetComponent<Sink>().PathIsBlocked() == false && anim.GetBool("Angry") == true)
            {
                anim.SetBool("Angry", false);
                reachDist = 0.3f;
                currentPoint -= 1;
                ChangeState(C_STATE.WASH);
                anim.SetBool("Washing", true);
                StartCoroutine(WashAnimEnded());
                yield break;
            }
            if (Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>() && Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().PathIsBlocked() == false && anim.GetBool("Angry") == true)
            {
                anim.SetBool("Angry", false);
                reachDist = 0.3f;
                currentPoint -= 1;
                if (tempState == C_STATE.SHIT)
                {
                    ChangeState(C_STATE.SHIT);
                }
                yield break;
            }

            yield return null;
        }

        anim.SetBool("Angry", false);
        UrinalUnOccupy();
        SinkUnOccupy();

        ChangeState(C_STATE.EXIT);
        ParentToTakeFrom = GameObject.Find("Exit");
        AddChild();

        yield break;
    }

    public IEnumerator ShitAnimEnded()
    {
        float delay = 0;
        while (delay < shitTime)
        {
            delay += Time.deltaTime;
            yield return null;
        }

        int i = 0;
        if (WaypointEnded() && State == C_STATE.SHIT)
        {
            BowlUnOccupy();
            EnviManager.Instance.Cub_Door.SetActive(false);

            //cant radom, need have int totaltoiletpaper
            i = Random.Range(1, 2);
            if (i == 1)
                CreatedShitMess();
            else if (i == 2)
                CreatedLackofRolls();

            if (EnviManager.Instance.GetEmptySinkSlots() <= 0)
            {
                ParentToTakeFrom = GameObject.Find("Exit");
                AddChild();
                ChangeState(C_STATE.EXIT);
            }
            else
            {
                ChangeState(C_STATE.WASH);

                //spawn mess

                WalkToSink();
            }
        }
        yield break;

    }

    public IEnumerator WashAnimEnded()
    {
        float delay = 0;
        while (delay < washTime)
        {
            delay += Time.deltaTime;
            yield return null;
        }

        if (WaypointEnded() && State == C_STATE.WASH)
        {
            anim.SetBool("Washing", false);
            CreatedWashMess();
            SinkUnOccupy();
            ChangeState(C_STATE.EXIT);

            //spawn mess

            ParentToTakeFrom = GameObject.Find("Exit");
            AddChild();
        }
        yield break;
    }

    public IEnumerator DrawAnimEnded()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (WaypointEnded() && State == C_STATE.DRAW)
        {
            anim.SetBool("Drawing", false);
            ChangeState(C_STATE.EXIT);
            ParentToTakeFrom = GameObject.Find("Exit");
            AddChild();
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
            speed = 30f;
            BodyMovement.velocity = dir * speed;
        }
        if (dist <= reachDist)
        {
            speed = 0f;
            BodyMovement.velocity = Vector2.zero;
            currentPoint++;
        }
    }

    void ChangeState(C_STATE state)
    {
        State = state;
        processed = false;
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

    bool processed = false;
    public void ProcessState()
    {
        if (processed)
            return;

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
            case C_STATE.ANGRY:
                {
                    PlayerInTheWay();
                }
                break;
            case C_STATE.WAITING_TO_SPAWN:
                {
                    Spawn();
                }
                break;
            default:
                break;
        }
        //yield return null;
    }

    public void PlayerInTheWay()
    {
        if (WaypointEnded() && State != C_STATE.EXIT && anim.GetBool("Angry") == false)
        {
            anim.SetBool("Angry", true);
            StartCoroutine(AngryAnimEnded());
        }
        
    }

    IEnumerator PeeAction()
    {
        if (EnviManager.Instance.UrinalMess(Waypoint[Waypoint.Count - 1].GetComponent<Urinal>()) == false)
        {
            anim.SetBool("Peeing", true);
            StartCoroutine(PeeAnimEnded());
        }
        else
        {
            anim.SetBool("Angry", true);
            StartCoroutine(AngryAnimEnded());
        }
        yield break;
    }

    //go to urinal
    public void Pee()
    {
        //Change path to Pee
        if (!Stop)
        {
            WalkToPee();
            return;
        }

        PathIsBeingBlocked();

        if (WaypointEnded())
        {
            processed = true;
            StartCoroutine(PeeAction());
        }
    }

    //go to cubicle
    IEnumerator ShitAction()
    {
        if (EnviManager.Instance.ShitMess(Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>()) == false)
        {
            StartCoroutine(ShitAnimEnded());
        }
        else
        {
            anim.SetBool("Angry", true);
            StartCoroutine(AngryAnimEnded());
        }
        yield break;
    }

    public void Shit()
    {
        if (!Stop)
        {
            WalkToShit();
            return;
        }
        PathIsBeingBlocked();
        if (WaypointEnded())
        {
            processed = true;
            EnviManager.Instance.Cub_Door.SetActive(true);
            StartCoroutine(ShitAction());
        }
    }

    //go to basin
    IEnumerator WashAction()
    {
        if (EnviManager.Instance.SinkMess(Waypoint[Waypoint.Count - 1].GetComponent<Sink>()) == false)
        {
            anim.SetBool("Washing", true);
            StartCoroutine(WashAnimEnded());
        }
        else
        {
            anim.SetBool("Angry", true);
            StartCoroutine(AngryAnimEnded());
        }
        yield break;
    }

    public void Wash()
    {
        PathIsBeingBlocked();

        if (WaypointEnded())
        {
            Stop = false;
            processed = true;
            StartCoroutine(WashAction());
        }

    }

    //Go to wall
    public void Draw()
    {
        if (!Stop)
            WalkToWall();

        if (WaypointEnded() && State == C_STATE.DRAW)
        {
            //run animation here
            anim.SetBool("Drawing", true);
            //Animation Ended
            StartCoroutine(DrawAnimEnded());
        }

    }

    public void NPC_Exit()
    {
        if (WaypointEnded())
        {
            Stop = false;
            //run animation here

            //Delete
            NPC.ReturnObject(gameObject);
            GlobalVar.Instance.CustomerCount--;
            ChangeState(C_STATE.WAITING_TO_SPAWN);
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

        if (WaypointEnded())
        {
            Stop = false;

            //Random Path
            if (EnviManager.Instance.UrinalAllFull()) { RNG_Path = Random.Range(2, 4); }
            else if (EnviManager.Instance.AllDrawn()) { RNG_Path = Random.Range(1, 3); }
            else
                RNG_Path = Random.Range(1, 1);

            switch (RNG_Path)
            {
                case 1:
                    {
                        //go to urinal
                        ChangeState(C_STATE.PEE);
                    }
                    break;
                case 2:
                    {
                        //go to cubicle
                        ChangeState(C_STATE.SHIT);
                    }
                    break;
                case 3:
                    {
                        //Draw
                        ChangeState(C_STATE.DRAW);
                    }
                    break;
                case 4:
                    {
                        //go to basin
                        ChangeState(C_STATE.WASH);
                    }
                    break;
                default:
                    break;
            }
        }

        //Move to target path


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
        if (EnviManager.Instance.GetEmptyUrinalSlots() >= 0 && EnviManager.Instance.UrinalList != null)
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
            //Debug.Log(EnviManager.Instance.GetEmptySink());
        }
        Stop = true;
    }

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

    public void Spawn()
    {
        transform.position = Spwn.transform.position;
        currentPoint = 0;
        Waypoint.Clear();
        ChangeState(C_STATE.WALK);
    }

    #endregion

    #region Mess

    private void CreatedPeeMess()
    {
        if (Waypoint.Count < 0)
            return;

        if (Waypoint[Waypoint.Count - 1].GetComponent<Urinal>())
            Waypoint[Waypoint.Count - 1].GetComponent<Urinal>().CreatedPeeMess();
    }

    private void CreatedWashMess()
    {
        if (Waypoint.Count < 0)
            return;

        if (Waypoint[Waypoint.Count - 1].GetComponent<Sink>())
            Waypoint[Waypoint.Count - 1].GetComponent<Sink>().CreatedWaterPuddle();
    }

    private void CreatedShitMess()
    {
        if (Waypoint.Count < 0)
            return;

        if (Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>())
            Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().CreatedShit();
    }

    private void CreatedLackofRolls()
    {
        if (Waypoint.Count < 0)
            return;

        if (Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>())
            Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().NoToiletPaper();
    }

    private void CreatedGraffitiMess()
    {
        if (Waypoint.Count < 0)
            return;

        if (Waypoint[Waypoint.Count - 1].GetComponent<Draw>())
            Waypoint[Waypoint.Count - 1].GetComponent<Draw>().CreatedGraffite();
    }
    #endregion

    #region UnOccupy

    private void UrinalUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<Urinal>())
            {
                //Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<Urinal>().UnOccupy();
                break;
            }
        }
    }
    private void SinkUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<Sink>())
            {
                // Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<Sink>().UnOccupy();
                break;
            }
        }
    }
    private void BowlUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<ToiletBowl>())
            {
                child.GetComponent<ToiletBowl>().UnOccupy();
                break;
            }
        }
    }
    #endregion

    public void PathIsBeingBlocked()
    {

            if (Waypoint[Waypoint.Count - 1].GetComponent<Urinal>() && Waypoint[Waypoint.Count - 1].GetComponent<Urinal>().PathIsBlocked() == true && anim.GetBool("Peeing") == false)
            {
                reachDist = 20f;
                tempState = C_STATE.PEE;
                ChangeState(C_STATE.ANGRY);
            }
            if (Waypoint[Waypoint.Count - 1].GetComponent<Sink>() && Waypoint[Waypoint.Count - 1].GetComponent<Sink>().PathIsBlocked() == true && anim.GetBool("Washing") == false)
            {
                reachDist = 20f;
                tempState = C_STATE.WASH;
                ChangeState(C_STATE.ANGRY);
            }
            if (Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>() && Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().PathIsBlocked() == true && shitTime >3)
            {
                reachDist = 20f;
                tempState = C_STATE.SHIT;
                ChangeState(C_STATE.ANGRY);
            }
        
    }
}