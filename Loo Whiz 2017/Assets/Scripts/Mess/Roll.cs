using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Roll : Mess_Check
{
<<<<<<< HEAD
    public AudioClip DoneCleaning_Sound;
    Character_Button Player;

    void Start()
    {
        type = MESS_TYPE.ROll;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
=======
    Scene currentScene;
    void Start()
    {
        type = MESS_TYPE.ROll;
        currentScene = SceneManager.GetActiveScene();
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
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
<<<<<<< HEAD
        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        GlobalVar.Instance.IsEnableInput = true;
=======
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
        GlobalVar.Instance.Cleaning = false;
        gameObject.GetComponentInParent<ToiletBowl>().RestockToiletPaper();
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.MeterValue++;
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 12;


    }
}
