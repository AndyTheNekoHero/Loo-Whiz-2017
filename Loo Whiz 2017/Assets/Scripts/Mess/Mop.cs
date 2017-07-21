using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Mess_Check
{
    public AudioClip Cleaning_Sound;
    public AudioClip DoneCleaning_Sound;
    bool Once = true;
    Character_Button Player;

    void Start()
    {
        type = MESS_TYPE.MOP;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
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
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Mopping", false);
        GlobalVar.Instance.MeterValue++;
        Once = true;
        if (gameObject.GetComponentInParent<Urinal>())
        {
            gameObject.GetComponentInParent<Urinal>().PeeCleaned();
            GlobalVar.Instance.Tut_Steps = 1;
        }
        else if (gameObject.GetComponentInParent<ToiletBowl>())
        {
            gameObject.GetComponentInParent<ToiletBowl>().ShitCleaned();
            GlobalVar.Instance.Tut_Steps = 4;
        }
        else if (gameObject.GetComponentInParent<Sink>())
        {
            gameObject.GetComponentInParent<Sink>().WaterPuddleCleaned();
            GlobalVar.Instance.Tut_Steps = 5;
        }
    }
}
