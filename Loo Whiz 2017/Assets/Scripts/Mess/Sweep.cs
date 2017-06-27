using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : Mess_Check
{
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
    }
}
