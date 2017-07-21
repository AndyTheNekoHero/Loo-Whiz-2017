using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wipe : Mess_Check
{
    float t = 0;
<<<<<<< HEAD
    public float TotalTime = 20;

    public GameObject AngryFace;
    Animator anim2;

    public AudioClip Cleaning_Sound;
    public AudioClip DoneCleaning_Sound;
    bool Once = true;
    Character_Button Player;
=======
    public float TotalTime = 10;
    Scene currentScene;
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8

    void Start()
    {
        type = MESS_TYPE.WIPE;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim2 = AngryFace.GetComponent<Animator>();
        StartCoroutine(DidntClean());
<<<<<<< HEAD
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
=======
        currentScene = SceneManager.GetActiveScene();
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
    }

    protected override void DoAction()
    {
        if (GlobalVar.Instance.Wipe_Selected && InRange && Selected)
        {
            if (Once)
            {
                AudioManager.instance.PlaySound(Cleaning_Sound, transform.position);
                Once = false;
            }
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Wipping", true);
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
        anim.SetBool("Wipping", false);
        gameObject.GetComponentInParent<Draw>().GraffiteCleaned();
        GlobalVar.Instance.MeterValue++;
<<<<<<< HEAD
        Once = true;
=======
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 8;

>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
    }

    IEnumerator DidntClean()
    {
        while (true)
        {
            t += Time.deltaTime;
            if (t >= TotalTime)
            {
                GlobalVar.Instance.MeterValue--;
                AngryFace.SetActive(true);
                t = 0;
            }
            if (AngryFace.activeSelf)
            {
                if (anim2.GetCurrentAnimatorStateInfo(0).IsName("AngryFace"))
                {
                    yield return new WaitForSeconds(anim2.GetCurrentAnimatorStateInfo(0).length + anim2.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    AngryFace.SetActive(false);
                }
            }
            yield return null;
        }
        //yield break;
    }
}
