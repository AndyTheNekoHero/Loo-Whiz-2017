﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoodMeter : MonoBehaviour
{
    Slider slider;
    public GameObject LoadLevel;
	// Use this for initialization
	void Start ()
    {
        slider = gameObject.GetComponent<Slider>();
        GlobalVar.Instance.MeterValue = 0;
        slider.value = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        slider.value = GlobalVar.Instance.MeterValue;
        if (slider.value == slider.maxValue)
        {
            GlobalVar.Instance.Win = true;
            GlobalVar.Instance.CustomerCount = 0;
            LoadLevel.GetComponent<LoadScene>().LoadSceneNum(4);

        }
        if (slider.value == slider.minValue)
        {
            GlobalVar.Instance.Lose = true;
            GlobalVar.Instance.CustomerCount = 0;
            LoadLevel.GetComponent<LoadScene>().LoadSceneNum(4);
        }
    }
}
