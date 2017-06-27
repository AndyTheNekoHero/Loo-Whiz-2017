using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipe : Mess_Check
{
    void Start()
    {
        type = MESS_TYPE.WIPE;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

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
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Wipping", false);
    }
}
