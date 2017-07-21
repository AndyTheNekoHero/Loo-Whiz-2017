using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mop : Mess_Check
{
<<<<<<< HEAD
    public AudioClip Cleaning_Sound;
    public AudioClip DoneCleaning_Sound;
    bool Once = true;
    Character_Button Player;
=======
    private bool TuTcheck = false;
    Scene currentScene;
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8

    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
<<<<<<< HEAD
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
=======
        Time.timeScale = 1.0f;
        currentScene = SceneManager.GetActiveScene();
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
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
<<<<<<< HEAD
        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Mopping", false);
        GlobalVar.Instance.MeterValue++;
        Once = true;
=======
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
            GlobalVar.Instance.Cleaning = false;
            anim.SetBool("Mopping", false);
            GlobalVar.Instance.MeterValue++;
        

>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
        if (gameObject.GetComponentInParent<Urinal>())
        {
            gameObject.GetComponentInParent<Urinal>().PeeCleaned();
            GlobalVar.Instance.Tut_Steps = 1;
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
