using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    public bool move = false;
    private Vector2 touchPosition;
    public float movespeed = 100.0f;
    public Rigidbody2D RB2D;
    public bool DragToMove= false;
    private Animator anim;
    public Vector3 vel;
    public GameObject selected = null;

    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (GlobalVar.Instance.Gender == 0)
            anim.runtimeAnimatorController = Resources.Load("Player_Movement(F)") as RuntimeAnimatorController;
        else if (GlobalVar.Instance.Gender == 1)
            anim.runtimeAnimatorController = Resources.Load("Player_Movement(M)") as RuntimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the screen is touched / clicked   
        if ( ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)) ) && !DragToMove && GlobalVar.Instance.IsEnableInput)
        {
            move = true;
            //for unity editor
#if UNITY_EDITOR
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        }
        if ( ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)) || (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) ) && DragToMove && GlobalVar.Instance.IsEnableInput)
        {
            move = true;
            //for unity editor
#if UNITY_EDITOR
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        }

         Vector2 dir = (touchPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (move && Vector2.Distance(transform.position, touchPosition) > 0.5f && !GetComponent<Character_Button>().ButtonClick)
        {
            RB2D.velocity = Vector2.zero;
            RB2D.velocity = (dir * movespeed);
        }
        else if (move && Vector2.Distance(transform.position, touchPosition) <= 0.5f)
        {
            move = false;
            RB2D.velocity = Vector2.zero;
        }

        if (GetComponent<Character_Button>().ButtonClick)
            move = false;

        //set animation
        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
        anim.SetBool("PlayerMoving", move);

    }

    void OnCollisionEnter2D(Collision2D colliderInfo)
    {
        move = false;
        RB2D.velocity = Vector2.zero;
    }
}



//for(int i = 0; i < Input.touchCount; i++)
//switch (Input.touches[i].phase)
//{
//    case TouchPhase.Began:
//        break;
//    case TouchPhase.Ended:
//        break;
//}