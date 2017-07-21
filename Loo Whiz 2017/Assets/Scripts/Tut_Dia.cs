using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Dia : MonoBehaviour {

    public static Tut_Dia Instance;

    public GameObject TPanels, NxtBtn, BackBtn;

    public bool Panel2 = false;
    public bool Panel3 = false;
    public bool Panel4 = false;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start () {
        TPanels = GameObject.FindGameObjectWithTag("T_Panels");
        NxtBtn = GameObject.Find("Next");
    }
	
	// Update is called once per frame
	void Update () {
        if (GlobalVar.Instance.Tut_Steps == 7 && Panel2 == false)
        {
            TPanels.SetActive(true);
            InfoPanel.Instance.Welcome = true;
            NxtBtn.SetActive(true);
            Panel2 = true;
        }
        else if (GlobalVar.Instance.Tut_Steps == 9 && Panel3 == false)
        {
            TPanels.SetActive(true);
            InfoPanel.Instance.Welcome = true;
            NxtBtn.SetActive(true);
            Panel3 = true;
        }
        else if (GlobalVar.Instance.Tut_Steps == 13 && Panel4 == false)
        {
            TPanels.SetActive(true);
            InfoPanel.Instance.Welcome = true;
            NxtBtn.SetActive(true);
            Panel4 = true;
        }

    }

    public void BtnOnPress()
    {
        if (transform.GetChild(0).gameObject.activeSelf == true)
            ActivateD(2);
    }
    public void BtnOnPress2()
    {
        if (GlobalVar.Instance.Tut_Steps == 7)
        {
            if (transform.GetChild(4).gameObject.activeSelf == true)
            {
                De_activateD(4);
                ActivateD(5);
            }
        }
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
            case 4:
                {
                    transform.GetChild(4).gameObject.SetActive(true);

                }
                break;
            case 5:
                {
                    transform.GetChild(5).gameObject.SetActive(true);

                }
                break;
            case 6:
                {
                    transform.GetChild(6).gameObject.SetActive(true);

                }
                break;
            case 7:
                {
                    transform.GetChild(7).gameObject.SetActive(true);

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
            case 4:
                {
                    transform.GetChild(4).gameObject.SetActive(false);
                }
                break;
            case 5:
                {
                    transform.GetChild(5).gameObject.SetActive(false);

                }
                break;
            case 6:
                {
                    transform.GetChild(6).gameObject.SetActive(false);

                }
                break;
            case 7:
                {
                    transform.GetChild(7).gameObject.SetActive(false);

                }
                break;
            default:
                break;
        }
    }
}
