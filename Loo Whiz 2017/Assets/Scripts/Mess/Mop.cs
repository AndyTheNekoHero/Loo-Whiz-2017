using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mop : Mess_Check
{
    private bool TuTcheck = false;
    Scene currentScene;

    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Time.timeScale = 1.0f;
        currentScene = SceneManager.GetActiveScene();
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
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
            GlobalVar.Instance.Cleaning = false;
            anim.SetBool("Mopping", false);
            GlobalVar.Instance.MeterValue++;
        

        if (gameObject.GetComponentInParent<Urinal>())
        {
            gameObject.GetComponentInParent<Urinal>().PeeCleaned();
            GlobalVar.Instance.Tut_Steps = 1;
            if(currentScene.name == "Tutorial")
                GlobalVar.Instance.IsEnableInput = false;
        }
        else if (gameObject.GetComponentInParent<ToiletBowl>())
        {
            gameObject.GetComponentInParent<ToiletBowl>().ShitCleaned();
            if (TuTcheck == true)
            {
                GlobalVar.Instance.IsEnableInput = false;
            }
            else
                TuTcheck = true;
            GlobalVar.Instance.Tut_Steps = 4;


        }
        else if (gameObject.GetComponentInParent<Sink>())
        {
            gameObject.GetComponentInParent<Sink>().WaterPuddleCleaned();
            if (TuTcheck == true)
            {
                GlobalVar.Instance.IsEnableInput = false;
            }
            else
                TuTcheck = true;
            GlobalVar.Instance.Tut_Steps = 6;

        }
    }
}
