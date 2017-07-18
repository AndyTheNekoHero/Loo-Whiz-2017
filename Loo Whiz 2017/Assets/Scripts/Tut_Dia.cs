using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Dia : MonoBehaviour {

    public static Tut_Dia Instance;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnOnPress()
    {
        ActivateD(2);
    }

    public void ActivateD(int D)
    {
        switch (D)
        {
            case 1:
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    if (GlobalVar.Instance.Tut_Steps == 0)
                        transform.GetChild(1).gameObject.SetActive(true);
                }
                break;
            case 3:
                {
                   transform.GetChild(2).gameObject.SetActive(true);
                   transform.GetChild(3).gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }


    public void De_activateD(int D)
    {
        switch (D)
        {
            case 0:
                {
                  transform.GetChild(0).gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
            case 2:
                {
                    transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 3:
                {
                    transform.GetChild(3).gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
