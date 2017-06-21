﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Button : MonoBehaviour
{
    public GameObject Chara_button;

    private Vector2 TouchPosition;
    private bool ScreenTouched;
    private bool Once;
    public bool ButtonClick;
    Ray ray;

    // Use this for initialization
    void Start ()
    {
        Chara_button.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Chara_button.transform.position = transform.position;

        if ( ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0))) )
        {
            ScreenTouched = true;
            //for unity editor
#if UNITY_EDITOR
        TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
     
            //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        TouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

            Collider2D touchedObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (touchedObject)
            {
                if (touchedObject.tag == "Player")
                {
                    Chara_button.SetActive(true);
                    ButtonClick = true;
                    GetComponent<PlayerMovement>().RB2D.velocity = Vector2.zero;

                }
                else if (touchedObject.tag == "Button")
                {
                    ButtonClick = true;
                    GetComponent<PlayerMovement>().move = false;
                }
                else
                {
                    Chara_button.SetActive(false);
                    ButtonClick = false;
                }
            }

        }

    }
}
