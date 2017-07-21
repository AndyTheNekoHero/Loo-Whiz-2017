using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : MonoBehaviour
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

    #region Time
    [SerializeField]
    private float peeTime = 5;
    [SerializeField]
    private float shitTime = 5;
    [SerializeField]
    private float washTime = 5;
    [SerializeField]
    private float angryTime = 8;
    [SerializeField]
    private float DrawTime = 5;
    [SerializeField]
    private int RandomSpawnOfMess = 80;
    [SerializeField]
    private int RandomSpawnOfLitter = 40;
    #endregion

    bool Stop;
    public int RNG_Path;
    public List<Transform> Waypoint = new List<Transform>();
    public float speed = 2f;
    private float reachDist = 0.3f;
    public int currentPoint = 0;
    Rigidbody2D BodyMovement;

    ObjectPool NPC;
    Tut_spawner Spwn;
    private Animator anim;
    public bool DoOnce;
    public float ThrowPaperTime = 100;
    private bool ExitAngry = false;

    GameObject Cub_Door;

    //private float countdown = 0.0f;

    // Use this for initialization
    void Start()
    {
        State = C_STATE.WALK;
        Stop = false;
        BodyMovement = GetComponent<Rigidbody2D>();
        //NPC = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        Spwn = GameObject.Find("tut_Spawner").GetComponent<Tut_spawner>();
        transform.position = Spwn.transform.position;
        anim = GetComponent<Animator>();
        Pause.Instance.IsPause = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (InfoPanel.Instance.Welcome == true)
            return;

        ProcessState();
        MoveToWaypoint();

        #region Debug Fast Forward
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Time.timeScale = 4f;
        }
        else
            Time.timeScale = 1f;
        #endregion

        if (ParentToTakeFrom == null)
        {
            Debug.LogWarning("Error404! GameObject Not Found! Ending Programme Now!");
            Application.Quit();
        }

        if (GlobalVar.Instance.Win || GlobalVar.Instance.Lose)
        {
            UrinalUnOccupy();
            SinkUnOccupy();
            BowlUnOccupy();
            WalllUnOccupy();
        }
        TutorialSteps();
        //Debug.Log(currentPoint + " " + Waypoint.Count);
    }

    public void TutorialSteps()
    {
        if (GlobalVar.Instance.Tut_Steps == 1)
        {
            Tut_Dia.Instance.De_activateD(1);
            if (WaypointEnded())
            {
                Destroy(this.gameObject);
                GlobalVar.Instance.CustomerCount--;
                ChangeState(C_STATE.WAITING_TO_SPAWN);
                GlobalVar.Instance.Tut_Steps = 2;
            }
        }
        else if (GlobalVar.Instance.Tut_Steps == 4)
        {
            Tut_Dia.Instance.De_activateD(2);           
            //GlobalVar.Instance.Tut_Steps = 5;
            if (GlobalVar.Instance.T_Check == true)
            {
                GlobalVar.Instance.Tut_Steps = 5;
            }
            else
            {
                GlobalVar.Instance.T_Check = true;
                GlobalVar.Instance.Tut_Steps = 99;
            }
            Destroy(this.gameObject);

        }
        else if (GlobalVar.Instance.Tut_Steps == 6)
        {
            Tut_Dia.Instance.De_activateD(3);            
            if (GlobalVar.Instance.T_Check == true)
            {
                GlobalVar.Instance.Tut_Steps = 5;
            }
            else
            {
                GlobalVar.Instance.T_Check = true;
                GlobalVar.Instance.Tut_Steps = 99;
            }
            Destroy(this.gameObject);
        }
        else if (GlobalVar.Instance.Tut_Steps == 8)
        {
            Tut_Dia.Instance.De_activateD(5);
            Destroy(this.gameObject);
            GlobalVar.Instance.CustomerCount = 0;
            ChangeState(C_STATE.WAITING_TO_SPAWN);
            GlobalVar.Instance.Tut_Steps = 9;
        }
        else if (GlobalVar.Instance.Tut_Steps == 10)
        {
            Tut_Dia.Instance.De_activateD(6);
            Destroy(this.gameObject);
            GlobalVar.Instance.CustomerCount = 0;
            ChangeState(C_STATE.WAITING_TO_SPAWN);
            GlobalVar.Instance.Tut_Steps = 11;
        }
        if (GlobalVar.Instance.Tut_Steps == 12)
        {
            Tut_Dia.Instance.De_activateD(7);
            GlobalVar.Instance.Tut_Steps = 13;
        }

        if (GlobalVar.Instance.Tut_Steps == 5)
        {
            GlobalVar.Instance.Tut_Steps = 7;
            GlobalVar.Instance.CustomerCount = 0;
            ChangeState(C_STATE.WAITING_TO_SPAWN);
            BowlUnOccupy();
            SinkUnOccupy();
        }
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
            if (r <= RandomSpawnOfMess)
            {
                //spawn mess
                CreatedPeeMess();
                if (currentPoint - 1 < Waypoint.Count)
                {
                    GameObject EnviroPee = (GameObject)Instantiate(Resources.Load("Pee"), (Waypoint[currentPoint - 1].GetComponent<Urinal>().transform));
                    EnviroPee.transform.position = (EnviroPee.transform.position + new Vector3(3.6f, -7.0f, 0));
                }
                //EnviroPee.transform.localScale = new Vector2(1.5f, 1.5f);
            }

            Tut_Dia.Instance.ActivateD(1);
            Pause.Instance.IsPause = false;
                ParentToTakeFrom = GameObject.Find("Exit2");
                AddChild();
                ChangeState(C_STATE.EXIT);

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

            if (Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>() && Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().PathIsBlocked() == false && anim.GetBool("Angry") == true
                && !Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().IsShit() && !Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>().IsToiletPaper())
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
        ExitAngry = true;
        UrinalUnOccupy();
        SinkUnOccupy();
        BowlUnOccupy();

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

        if (WaypointEnded() && State == C_STATE.SHIT)
        {
            BowlUnOccupy();
            GlobalVar.Instance.ToiletPaper--;
            OpenDoor();
            int r = 0;
            r = Random.Range(1, 100);
            if (r <= RandomSpawnOfMess)
            {
                CreatedShitMess();
                if (currentPoint - 1 < Waypoint.Count)
                {
                    GameObject EnviroShit = (GameObject)Instantiate(Resources.Load("Shit"), (Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().transform));
                    EnviroShit.transform.position = (EnviroShit.transform.position + new Vector3(0.0f, -8.5f, 0));
                }
            }

            if (GlobalVar.Instance.ToiletPaper == 0)
            {
                CreatedLackofRolls();
                GameObject LackOfToiletPaper = (GameObject)Instantiate(Resources.Load("Toilet_Paper_Icon"), (Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().transform));
                LackOfToiletPaper.transform.position = LackOfToiletPaper.transform.position + new Vector3(0.0f, -3.0f, 0);
            }

            Pause.Instance.IsPause = false;

            if (GlobalVar.Instance.Tut_Steps == 3)
                Tut_Dia.Instance.ActivateD(3);
            else if (GlobalVar.Instance.Tut_Steps == 11)
            {
                Tut_Dia.Instance.ActivateD(7);
            }
            ChangeState(C_STATE.EXIT);
            ParentToTakeFrom = GameObject.Find("Exit2");
            AddChild();

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
            Waypoint[Waypoint.Count - 1].GetComponent<Sink>().StopAnimation();
            SinkUnOccupy();

            int r = Random.Range(1, 100);
            if (r <= RandomSpawnOfMess)
            {
                //spawn mess
                CreatedWashMess();
                if (currentPoint - 1 < Waypoint.Count)
                {
                    GameObject EnviroWater = (GameObject)Instantiate(Resources.Load("Water"), (Waypoint[currentPoint - 1].GetComponent<Sink>().transform));
                    EnviroWater.transform.position = (EnviroWater.transform.position + new Vector3(0.0f, -0.5f, 0));
                }
            }

            Pause.Instance.IsPause = false;
            if (GlobalVar.Instance.Tut_Steps == 3)
                Tut_Dia.Instance.ActivateD(3);
            else if (GlobalVar.Instance.Tut_Steps == 9)
            {
                StartCoroutine(SpawnLitter());
                Tut_Dia.Instance.ActivateD(6);
            }
            ChangeState(C_STATE.EXIT);
            ParentToTakeFrom = GameObject.Find("Exit");
            AddChild();
        }
        yield break;
    }

    public IEnumerator DrawAnimEnded()
    {
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        float delay = 0;
        while (delay < DrawTime)
        {
            delay += Time.deltaTime;
            yield return null;
        }

        if (WaypointEnded() && State == C_STATE.DRAW)
        {
            anim.SetBool("Drawing", false);
            CreatedGraffitiMess();

            if (currentPoint - 1 < Waypoint.Count)
            {
                GameObject EnviroWall = (GameObject)Instantiate(Resources.Load("Graffiti"), (Waypoint[currentPoint - 1].GetComponent<Draw>().transform));
                EnviroWall.transform.position = transform.position + new Vector3(0.0f, 3.0f);
                //EnviroWall.transform.localScale = new Vector2(1.0f, 1.0f);
            }

            Tut_Dia.Instance.ActivateD(4);
            Pause.Instance.IsPause = false;
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

    public void Pee()
    {
        //Change path to Pee
        if (!Stop)
        {
            WalkToPee();
            return;
        }

        if (WaypointEnded())
        {
            processed = true;
            StartCoroutine(PeeAction());
        }
    }

    IEnumerator ShitAction()
    {
        if (EnviManager.Instance.ShitMess(Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>()) == false && EnviManager.Instance.RollMess(Waypoint[Waypoint.Count - 1].GetComponent<ToiletBowl>()) == false
            && Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().PathIsBlocked() == false)
        {
            CloseDoor();
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
            StartCoroutine(ShitAction());
        }
    }

    IEnumerator WashAction()
    {
        if (EnviManager.Instance.SinkMess(Waypoint[Waypoint.Count - 1].GetComponent<Sink>()) == false)
        {
            anim.SetBool("Washing", true);
            Waypoint[Waypoint.Count - 1].GetComponent<Sink>().PlayAnimation();
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
        if (WaypointEnded())
        {
            Stop = false;
            processed = true;
            StartCoroutine(WashAction());
        }

    }

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
            if (ExitAngry)
                GlobalVar.Instance.MeterValue--;

            ExitAngry = false;
            //NPC.ReturnObject(gameObject);
            //GlobalVar.Instance.CustomerCount--;
            //ChangeState(C_STATE.WAITING_TO_SPAWN);
        }

    }

    IEnumerator SpawnLitter()
    {
        GameObject Litter = (GameObject)Instantiate(Resources.Load("Litter"));
        Litter.transform.position = transform.position;
        ThrowPaperTime = 100;
        yield break;
    }

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
            if (GlobalVar.Instance.Tut_Steps == 0)
                ChangeState(C_STATE.PEE);
            else if (GlobalVar.Instance.Tut_Steps == 2)
            { ChangeState(C_STATE.SHIT); GlobalVar.Instance.Tut_Steps = 3; }
            else if (GlobalVar.Instance.Tut_Steps == 3)
            { WalkToSink(); ChangeState(C_STATE.WASH); }
            else if (GlobalVar.Instance.Tut_Steps == 7)
            { WalkToWall(); ChangeState(C_STATE.DRAW); }
            else if (GlobalVar.Instance.Tut_Steps == 9)
            { WalkToSink(); ChangeState(C_STATE.WASH); RandomSpawnOfMess = 0; }
            else if (GlobalVar.Instance.Tut_Steps == 11)
            { ChangeState(C_STATE.SHIT); RandomSpawnOfMess = 0; }
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
        if (EnviManager.Instance.GetEmptyUrinalSlots() >= 0 && EnviManager.Instance.UrinalList != null)
        {
            Transform temp = EnviManager.Instance.GetEmptyUrinal();
            if (temp)
                Waypoint.Add(temp);
        }

        Stop = true;
    }

    public void WalkToSink()
    {
        //Getting One Sink
        if (EnviManager.Instance.GetEmptySinkSlots() >= 0 && EnviManager.Instance.SinkList != null)
        {
            Transform temp = EnviManager.Instance.GetEmptySink();
            if (temp)
                Waypoint.Add(temp);
        }
        Stop = true;
    }

    public void WalkToShit()
    {
        ////Getting One Cubicle
        if (EnviManager.Instance.GetEmptyBowlSlots() >= 0)
        {
            Transform temp = EnviManager.Instance.GetEmptyBowl();
            if (temp)
                Waypoint.Add(temp);
        }


        Stop = true;
    }

    public void WalkToWall()
    {
        ////Getting One Cubicle
        if (EnviManager.Instance.GetEmptyWallSlots() >= 0)
        {
            Transform temp = EnviManager.Instance.GetEmptyWall();
            if (temp)
                Waypoint.Add(temp);
        }

        Stop = true;
    }

    public void Spawn()
    {
        transform.position = Spwn.transform.position;
        currentPoint = 0;
        Waypoint.Clear();
        ChangeState(C_STATE.WALK);
        DoOnce = true;
        ThrowPaperTime = 100;

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
    private void WalllUnOccupy()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<Draw>())
            {
                child.GetComponent<Draw>().NotDrawn();
                break;
            }
        }
    }
    #endregion

    #region Miscellaneous
    private void CloseDoor()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<ToiletBowl>())
            {
                //Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<ToiletBowl>().CloseDoor();
                break;
            }
        }
    }
    private void OpenDoor()
    {
        foreach (Transform child in Waypoint)
        {
            if (child.GetComponent<ToiletBowl>())
            {
                //Debug.Log("Child in NPC: " + child.name);
                child.GetComponent<ToiletBowl>().OpenDoor();
                break;
            }
        }
    }
    public void PathIsBeingBlocked()
    {
        if (Waypoint[currentPoint - 1].GetComponent<ToiletBowl>() && Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().PathIsBlocked() == true
         && !Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().IsShit() && !Waypoint[currentPoint - 1].GetComponent<ToiletBowl>().IsToiletPaper())
        {
            reachDist = 20f;
            tempState = C_STATE.SHIT;
            ChangeState(C_STATE.ANGRY);
        }

    }
    #endregion


}