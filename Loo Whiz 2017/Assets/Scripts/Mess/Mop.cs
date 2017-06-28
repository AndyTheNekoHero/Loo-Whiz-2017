using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Mess_Check
{
    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
<<<<<<< HEAD
        cust = GameObject.FindGameObjectWithTag("Customer").GetComponent<NPC_Script>();
=======

>>>>>>> 0d239c17016ecdaf4c6c62bc626acf57178634ad
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
<<<<<<< HEAD
        CheckPeeClean();
    }

    private void CheckPeeClean()
    {   
        if (EnviManager.Instance.UrinalMess(cust.Waypoint[cust.Waypoint.Count - 1].GetComponent<Urinal>()))
        {
            cust.Waypoint[cust.Waypoint.Count - 1].GetComponent<Urinal>().PeeCleaned();
        }
=======
>>>>>>> 0d239c17016ecdaf4c6c62bc626acf57178634ad
    }
}
