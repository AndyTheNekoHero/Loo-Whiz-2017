using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoodMeter : MonoBehaviour
{
    Slider slider;
	// Use this for initialization
	void Start ()
    {
        slider = gameObject.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        slider.value = GlobalVar.Instance.MeterValue;
        if (slider.value == slider.maxValue)
        {
            GlobalVar.Instance.Win = true;
            SceneManager.LoadScene("EndScreen");

        }
        if (slider.value == slider.minValue)
        {
            GlobalVar.Instance.Lose = true;
            SceneManager.LoadScene("EndScreen");
        }
    }
}
