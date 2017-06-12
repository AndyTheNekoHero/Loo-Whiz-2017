﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private bool move = false;
    private Vector2 touchPosition;
    private Vector2 prevPos;
    public float movespeed = 100.0f;
    Rigidbody2D RB2D;

    public bool DragToMove = false;

    private Animator anim;

    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the screen is touched / clicked   
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)) && !DragToMove && GlobalVar.Instance.IsEnableInput)
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
        else if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)) || (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && DragToMove && GlobalVar.Instance.IsEnableInput)
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

        prevPos = transform.position;
         Vector2 dir = (touchPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (move && Vector2.Distance(transform.position, touchPosition) > 0.1f)
        {
            RB2D.velocity = Vector2.zero;
            RB2D.AddForce(dir * movespeed);
        }
        else if (move && Vector2.Distance(transform.position, touchPosition) < 0.1f)
        {
            move = false;
            RB2D.velocity = Vector2.zero;
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal

        //set animation
        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
        anim.SetBool("PlayerMoving", move);
    }

    void OnCollisionEnter2D(Collision2D colliderInfo)
    {
        move = false;
        touchPosition = prevPos;
    }

    void OnCollisionStay2D(Collision2D colliderInfo)
    {
        move = false;
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