using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipe : Mess_Check
{
    float t = 0;
    public float TotalTime = 10;

    void Start()
    {
        type = MESS_TYPE.WIPE;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        StartCoroutine(DidntClean());
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
        gameObject.GetComponentInParent<Draw>().GraffiteCleaned();
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
        yield break;
        
    }
}
