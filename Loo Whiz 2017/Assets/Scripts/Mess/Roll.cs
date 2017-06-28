using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : Mess_Check
{

    void Start()
    {
        type = MESS_TYPE.ROll;
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
    }
}
