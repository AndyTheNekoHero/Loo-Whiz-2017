using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Mess_Check
{
    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
            GlobalVar.Instance.MeterValue++;

            if (gameObject.GetComponentInParent<Urinal>())
            {
                gameObject.GetComponentInParent<Urinal>().PeeCleaned();
            }
            else if (gameObject.GetComponentInParent<ToiletBowl>())
            {
                gameObject.GetComponentInParent<ToiletBowl>().ShitCleaned();
            }
            else if (gameObject.GetComponentInParent<Sink>())
            {
                gameObject.GetComponentInParent<Sink>().WaterPuddleCleaned();
            }
    }
}
