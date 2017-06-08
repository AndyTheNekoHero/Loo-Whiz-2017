using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D PlayerRB;
    private bool InRange = false;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        PlayerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dis = Vector2.Distance(Player.transform.position, transform.position);
        if (dis < 0.2f)
            InRange = true;
        
        if (InRange)
        {
            PlayerRB.velocity = Vector2.zero;
        }
        

	}
}
