using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sweep : Mess_Check
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

    void Start()
=======
    float TotalTime = 10;
    Scene currentScene;
    void Start ()
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
    {
        type = MESS_TYPE.SWEEP;
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
        if (GlobalVar.Instance.Sweep_Selected && InRange && Selected)
        {
            if (Once)
            {
                AudioManager.instance.PlaySound(Cleaning_Sound, transform.position);
                Once = false;
            }
            GlobalVar.Instance.IsEnableInput = false;
            CurrentActionTime += Time.deltaTime;
            GlobalVar.Instance.Cleaning = true;
            anim.SetBool("Sweeping", true);
            Player.Chara_button.SetActive(false);
        }
    }


    protected override void FinishedAction()
    {
<<<<<<< HEAD
        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        GlobalVar.Instance.IsEnableInput = true;
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Sweeping", false);
        GlobalVar.Instance.MeterValue++;
        Once = true;
=======
        if (currentScene.name != "Tutorial")
        {
            GlobalVar.Instance.IsEnableInput = true;
        }
        GlobalVar.Instance.Cleaning = false;
        anim.SetBool("Sweeping", false);
        GlobalVar.Instance.MeterValue++;
        GlobalVar.Instance.IsEnableInput = false;
        GlobalVar.Instance.Tut_Steps = 10;

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
