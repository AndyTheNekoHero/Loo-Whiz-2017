using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : Mess_Check
{
    float t = 0;
    float TotalTime = 5;

    void Start ()
    {
        type = MESS_TYPE.SWEEP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

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

    void Update()
    {
        DidntClean();
    }

    private void DidntClean()
    {
        t += Time.deltaTime;
        if (t >= TotalTime)
        {
            GlobalVar.Instance.MeterValue--;
            t = 0;
        }
    }
}
