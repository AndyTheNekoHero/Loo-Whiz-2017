using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour
{
    public Text TextTimer; 
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 0f)
            return;
        else
        {
            GlobalVar.Instance.TimeUsedSecs += Time.deltaTime;

            if (GlobalVar.Instance.TimeUsedSecs >= 60f)
            {
                GlobalVar.Instance.TimeUsedSecs -= 60;
                GlobalVar.Instance.TimeUsedMins += 1;
            }
            if (GlobalVar.Instance.TimeUsedSecs < 10f && GlobalVar.Instance.TimeUsedMins < 10f)
            {
                TextTimer.text = "0" + (int)GlobalVar.Instance.TimeUsedMins + ":0" + (int)GlobalVar.Instance.TimeUsedSecs;
            }
            else if (GlobalVar.Instance.TimeUsedSecs < 10f)
            {
                TextTimer.text = (int)GlobalVar.Instance.TimeUsedMins + ":0" + (int)GlobalVar.Instance.TimeUsedSecs;
            }
            else if (GlobalVar.Instance.TimeUsedMins < 10f)
            {
                TextTimer.text = "0" + (int)GlobalVar.Instance.TimeUsedMins + ":" + (int)GlobalVar.Instance.TimeUsedSecs;
            }
        }
    }
}
