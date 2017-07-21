using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sweep : Mess_Check
{
    float t = 0;
    float TotalTime = 10;
    Scene currentScene;
    void Start ()
    {
        type = MESS_TYPE.SWEEP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        StartCoroutine(DidntClean());
        currentScene = SceneManager.GetActiveScene();
    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Sweep_Selected && InRange && Selected)
        {
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Sweeping", true);
        }
    }


    protected override void FinishedAction()
    {
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Sweeping", false);
        GlobalVar.Instance.MeterValue++;
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 10;

    }

    IEnumerator DidntClean()
    {
        while (true)
        {
            t += Time.deltaTime;
            if (t >= TotalTime)
            {
                GlobalVar.Instance.MeterValue--;
                t = 0;
            }
            yield return null;
        }
        //yield break;
    }
}
