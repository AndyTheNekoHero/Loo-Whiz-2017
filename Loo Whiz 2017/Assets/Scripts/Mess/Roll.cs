using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Roll : Mess_Check
{
    public AudioClip DoneCleaning_Sound;
    Character_Button Player;
    Scene currentScene;

    void Start()
    {
        type = MESS_TYPE.ROll;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
        currentScene = SceneManager.GetActiveScene();
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
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
        GlobalVar.Instance.Cleaning = false;
        gameObject.GetComponentInParent<ToiletBowl>().RestockToiletPaper();
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.MeterValue++;
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 12;


    }
}
