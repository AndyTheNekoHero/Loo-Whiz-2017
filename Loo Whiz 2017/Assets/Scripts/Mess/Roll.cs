using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : Mess_Check
{
    public AudioClip DoneCleaning_Sound;
    Character_Button Player;

    void Start()
    {
        type = MESS_TYPE.ROll;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Roll_Selected && InRange && Selected)
        {
            CurrentActionTime = ActionTime;
            Player.Chara_button.SetActive(false);
        }
    }


    protected override void FinishedAction()
    {
        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        gameObject.GetComponentInParent<ToiletBowl>().RestockToiletPaper();
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.MeterValue++;
    }
}
