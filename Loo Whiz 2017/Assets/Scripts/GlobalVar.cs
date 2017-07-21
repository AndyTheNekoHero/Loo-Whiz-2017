using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVar : MonoBehaviour
{
    public static GlobalVar Instance = null;

    public int Gender = 1;
    public int CustomerCount    = 0;
    public bool Roll_Selected   = false;
    public bool Wipe_Selected   = false;
    public bool Mop_Selected    = false;
    public bool Sweep_Selected  = false;
    public bool IsEnableInput   = true;
    public bool Cleaning        = false;
    public int ToiletPaper      = 4;
    public float MeterValue     = 0;
    public bool Win             = false;
    public bool Lose            = false;
    public bool FromStart_HS    = false;
    public float TimeUsedSecs   = 0;
    public float TimeUsedMins   = 0;
<<<<<<< HEAD
    public bool StartGame       = false;
    public int Tut_Steps        = 0;
    public int TotalLitter      = 0;
=======
    public bool StartGame = false;
    public int Tut_Steps = 0;
    public bool T_Check = false;
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
  
        DontDestroyOnLoad(gameObject);

       // QualitySettings.vSyncCount = 0;
       // Application.targetFrameRate = 60;
    }


}
