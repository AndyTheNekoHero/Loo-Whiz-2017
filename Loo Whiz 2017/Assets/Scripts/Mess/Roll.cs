using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : Mess_Check
{

    void Start()
    {
        type = MESS_TYPE.ROll;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Roll_Selected && InRange && Selected)
        {
            CurrentActionTime = ActionTime;
        }
    }


    protected override void FinishedAction()
    {
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        gameObject.GetComponentInParent<ToiletBowl>().RestockToiletPaper();
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.MeterValue++;
    }
}
