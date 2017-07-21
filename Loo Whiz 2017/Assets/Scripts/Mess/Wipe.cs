using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wipe : Mess_Check
{
    float t = 0;
    public float TotalTime = 20;
    public GameObject AngryFace;
    Animator anim2;
    public AudioClip Cleaning_Sound;
    public AudioClip DoneCleaning_Sound;
    bool Once = true;
    Character_Button Player;
    Scene currentScene;


    void Start()
    {
        type = MESS_TYPE.WIPE;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim2 = AngryFace.GetComponent<Animator>();
        StartCoroutine(DidntClean());
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();

        currentScene = SceneManager.GetActiveScene();

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
        if (currentScene.name != "Tutorial")
            GlobalVar.Instance.IsEnableInput = true;
        else
            GlobalVar.Instance.IsEnableInput = false;
        AudioManager.instance.PlaySound(DoneCleaning_Sound, transform.position);
        anim.SetBool("Wipping", false);
        gameObject.GetComponentInParent<Draw>().GraffiteCleaned();
        GlobalVar.Instance.MeterValue++;
        Once = true;
        GlobalVar.Instance.Tut_Steps = 8;
        GlobalVar.Instance.Cleaning = false;

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
