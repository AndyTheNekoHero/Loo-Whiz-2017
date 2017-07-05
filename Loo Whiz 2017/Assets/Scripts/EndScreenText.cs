﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenText : MonoBehaviour
{
    Text text;
	// Use this for initialization
	void Start ()
    {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GlobalVar.Instance.Win)
            text.text = "You win";
        else if (GlobalVar.Instance.Lose)
            text.text = "You Lose";
	}
}