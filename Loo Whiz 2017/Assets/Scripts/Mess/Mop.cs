using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mop : Mess_Check
{

    public AudioClip Cleaning_Sound;
    public AudioClip DoneCleaning_Sound;
    bool Once = true;
    Character_Button Player;

    private bool TuTcheck = false;
    Scene currentScene;


    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
        currentScene = SceneManager.GetActiveScene();

    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Mop_Selected && InRange && Selected)
        {
            if (Once)
            {
                AudioManager.instance.PlaySound(Cleaning_Sound, transform.position);
                Once = false;
            }
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Mopping", true);
            Player.Chara_button.SetActive(false);
        }
    }


    protected override void FinishedAction()
    {

        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        Once = true;
        if (currentScene.name != "Tutorial")
            GlobalVar.Instance.IsEnableInput = true;
        else
            GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Mopping", false);
        GlobalVar.Instance.MeterValue++;    

        if (gameObject.GetComponentInParent<Urinal>())
        {
            gameObject.GetComponentInParent<Urinal>().PeeCleaned();
            GlobalVar.Instance.Tut_Steps = 1;
            if (currentScene.name == "Tutorial")
                GlobalVar.Instance.IsEnableInput = false;
        }
        else if (gameObject.GetComponentInParent<ToiletBowl>())
        {
            gameObject.GetComponentInParent<ToiletBowl>().ShitCleaned();
            if (TuTcheck == true && currentScene.name == "Tutorial")
                GlobalVar.Instance.IsEnableInput = false;
            else
            {
                TuTcheck = true;
                GlobalVar.Instance.Tut_Steps = 4;
            }
        }
        else if (gameObject.GetComponentInParent<Sink>())
        {
            gameObject.GetComponentInParent<Sink>().WaterPuddleCleaned();
            if (TuTcheck == true && currentScene.name == "Tutorial")
                GlobalVar.Instance.IsEnableInput = false;
            else
            {
                TuTcheck = true;
                GlobalVar.Instance.Tut_Steps = 6;
            }

        }
    }
}
