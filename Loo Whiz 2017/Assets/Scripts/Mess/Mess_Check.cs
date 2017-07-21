using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MESS_TYPE
{
    NULL = -1,
    SWEEP,
    MOP,
    WIPE,
    ROll
}
public class Mess_Check : MonoBehaviour
{
    public bool InRange = false;
    public Animator anim;
    public NPC_Script cust;

    [SerializeField]
    protected float ActionTime = 5.0f;
    [SerializeField]
    protected float CurrentActionTime = 0.0f;
    protected MESS_TYPE type = MESS_TYPE.NULL;
    [SerializeField]
    protected bool Selected = false;
    private Vector2 TouchPosition;

    bool StartDoingAction = false;

    public MESS_TYPE gettype()
    {
        return type;
    }

    // Use this for initialization
    void Start ()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D info)
    {
        if (info.tag != "Player")
            return;
        InRange = true;
    }
    void OnTriggerExit2D(Collider2D info)
    {
        InRange = false;
    }

    public virtual void StartAction()
    {
        StartDoingAction = true;
        //while (CurrentActionTime < ActionTime)
        //{
        //    DoAction();
        //    yield return null;
        //}
        //yield break;
    }

    protected virtual void DoAction()
    {
        print("NO ACTION ASSIGNED");
    }

    protected virtual void FinishedAction()
    {
        print("NO FINISHED ACTION ASSIGNED");      
    }

    // Update is called once per frame
    void Update ()
    {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0))))
        {
            //for unity editor
#if UNITY_EDITOR
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        TouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        }

        if (GetComponent<Collider2D>().OverlapPoint(TouchPosition))
        {
            Selected = true;
        }
        else
        {
            if(!GlobalVar.Instance.Cleaning)
                Selected = false;
        }

        if (StartDoingAction)
            DoAction();

        if (CurrentActionTime >= ActionTime || GlobalVar.Instance.Win || GlobalVar.Instance.Lose)
        {
            FinishedAction();
            GameObject Sparkle = (GameObject)Instantiate(Resources.Load("Sparkle"));
            Sparkle.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
