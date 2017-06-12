using UnityEngine;
using System.Collections;

public class GameBtn : MonoBehaviour {

    public void Mop()
    {
        //Character Mop if on Mess
        GlobalVar.Instance.Mop_Selected     = true;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
    }
    public void FillRoll()
    {
        //Character FillRoll if on ToiletBowl
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = true;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
    }
    public void Sweep()
    {
        //Character Sweep if on Mess
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = true;
        GlobalVar.Instance.Wipe_Selected    = false;
    }
    public void Wipe()
    {
        //Character wipe if on Mess
        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Roll_Selected    = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = true;
    }
}
