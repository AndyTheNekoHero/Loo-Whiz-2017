using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wipe : Mess_Check
{
    float t = 0;
    public float TotalTime = 10;
    Scene currentScene;

    void Start()
    {
        type = MESS_TYPE.WIPE;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        StartCoroutine(DidntClean());
        currentScene = SceneManager.GetActiveScene();
    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Wipe_Selected && InRange && Selected)
        {
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Wipping", true);
        }
    }


    protected override void FinishedAction()
    {
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Wipping", false);
        gameObject.GetComponentInParent<Draw>().GraffiteCleaned();
        GlobalVar.Instance.MeterValue++;
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 8;

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
