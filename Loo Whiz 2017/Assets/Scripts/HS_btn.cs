using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_btn : MonoBehaviour {

    public void HighScore()
    {
        GlobalVar.Instance.FromStart_HS = true;
    }
    public void StartBtn()
    {
        GlobalVar.Instance.StartGame = true;
    }
}
