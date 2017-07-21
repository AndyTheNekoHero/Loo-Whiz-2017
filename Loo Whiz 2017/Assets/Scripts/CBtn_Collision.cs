using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBtn_Collision : MonoBehaviour
{
    private GameObject selected = null;
    private Rigidbody2D RB2D;
    private Animator anim;


    // Use this for initialization
    void Start ()
    {
        RB2D = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D info)
    {
        Mess_Check m = info.GetComponent<Mess_Check>();
        if (m && selected == null)
        {
            StartCoroutine(m.StartAction());
        }
        RB2D.velocity = Vector2.zero;

    }
    void OnTriggerExit2D(Collider2D info)
    {
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Sweeping", false);
        anim.SetBool("Wipping", false);
        anim.SetBool("Mopping", false);
    }
}
