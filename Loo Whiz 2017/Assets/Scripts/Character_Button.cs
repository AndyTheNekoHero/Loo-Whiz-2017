using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Button : MonoBehaviour
{
    public GameObject Chara_button;

    private Vector2 TouchPosition;
    private bool ScreenTouched;
    private bool Once;
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
            //Debug.DrawRay(ray.origin, ray.direction * 10000000000000000000f, Color.green, 1000f);

            //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        TouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

            Collider2D touchedObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (touchedObject)
            {
                if (touchedObject.tag == "Button" || touchedObject.tag == "Player")
                {
                    Chara_button.SetActive(true);
                }
                else
                {
                    Chara_button.SetActive(false);
                }
            }

        }

        if (ScreenTouched && Vector2.Distance(transform.position, TouchPosition) <= 0.5f)
        {
            Chara_button.SetActive(true);
        }
        else
        {
            ScreenTouched = false;
        }

    }
}
