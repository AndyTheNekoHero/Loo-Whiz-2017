using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Mess_Check
{
    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        cust = GameObject.FindGameObjectWithTag("Customer").GetComponent<NPC_Script>();

    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Mop_Selected && InRange && Selected)
        {
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Mopping", true);
        }
    }


    protected override void FinishedAction()
    {
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Mopping", false);

        CheckPeeClean();
    }

    private void CheckPeeClean()
    {   
        if (EnviManager.Instance.UrinalMess(cust.Waypoint[cust.Waypoint.Count - 1].GetComponent<Urinal>()))
        {
            cust.Waypoint[cust.Waypoint.Count - 1].GetComponent<Urinal>().PeeCleaned();
        }
    }
}
