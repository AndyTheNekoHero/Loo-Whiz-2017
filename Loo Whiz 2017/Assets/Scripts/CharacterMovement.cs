﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    private bool move = false;
    private Vector2 touchPosition;
    private Vector2 prevPos;
    public float movespeed = 100.0f;
    Rigidbody2D RB2D;

    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the screen is touched / clicked   
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
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
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (move && !Mathf.Approximately(transform.position.magnitude, touchPosition.magnitude))
        {
            Vector2 dir = (touchPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            RB2D.velocity = Vector2.zero;
            RB2D.AddForce(dir * movespeed);
        }
        else if (move && Mathf.Approximately(transform.position.magnitude, touchPosition.magnitude))
        {
            move = false;
            RB2D.velocity = Vector2.zero;
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        
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