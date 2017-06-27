using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVar : MonoBehaviour
{
    public static GlobalVar Instance = null;

    public int CustomerCount    = 0;
    public bool Roll_Selected   = false;
    public bool Wipe_Selected   = false;
    public bool Mop_Selected    = false;
    public bool Sweep_Selected  = false;
    public bool IsEnableInput   = true;
    public bool Cleaning = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
  
        //DontDestroyOnLoad(gameObject);
    }


}
