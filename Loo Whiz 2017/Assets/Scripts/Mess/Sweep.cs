using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : Mess_Check
{
    float t = 0;
    float TotalTime = 10;

    void Start ()
    {
        type = MESS_TYPE.SWEEP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        StartCoroutine(DidntClean());
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
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Sweeping", false);
        GlobalVar.Instance.MeterValue++;
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
<<<<<<< HEAD
=======

>>>>>>> a13e3083bf35ddf8d6624f7bde4662128fa078bf
    }
}
