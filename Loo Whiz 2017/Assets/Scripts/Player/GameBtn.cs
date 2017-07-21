using UnityEngine;
using System.Collections;

public class GameBtn : MonoBehaviour
{
    private Animator anim;
    Character_Button Player;

    void Start()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
    }

    public void Mop()
    {
        //Character Mop if on Mess
        GlobalVar.Instance.Mop_Selected     = true;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
        gameObject.SetActive(false);
        GlobalVar.Instance.IsEnableInput = true;
        Player.ButtonClick = false;

        //change animation on button click
        anim.SetBool("Mop_Selected",    true);
        anim.SetBool("Roll_Selected",   false);
        anim.SetBool("Sweep_Selected",  false);
        anim.SetBool("Wipe_Selected",   false);
    }
    public void FillRoll()
    {
        //Character FillRoll if on ToiletBowl
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = true;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
        gameObject.SetActive(false);
        GlobalVar.Instance.IsEnableInput = true;
        Player.ButtonClick = false;

        //change animation on button click
        anim.SetBool("Mop_Selected",    false);
        anim.SetBool("Roll_Selected",   true);
        anim.SetBool("Sweep_Selected",  false);
        anim.SetBool("Wipe_Selected",   false);
    }
    public void Sweep()
    {
        //Character Sweep if on Mess
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = true;
        GlobalVar.Instance.Wipe_Selected    = false;
        gameObject.SetActive(false);
        GlobalVar.Instance.IsEnableInput = true;
        Player.ButtonClick = false;

        //change animation on button click
        anim.SetBool("Mop_Selected",    false);
        anim.SetBool("Roll_Selected",   false);
        anim.SetBool("Sweep_Selected",  true);
        anim.SetBool("Wipe_Selected",   false);
    }
    public void Wipe()
    {
        //Character wipe if on Mess
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = true;
        gameObject.SetActive(false);
        GlobalVar.Instance.IsEnableInput = true;
        Player.ButtonClick = false;

        //change animation on button click
        anim.SetBool("Mop_Selected",    false);
        anim.SetBool("Roll_Selected",   false);
        anim.SetBool("Sweep_Selected",  false);
        anim.SetBool("Wipe_Selected",   true);
    }
}
