using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject PausePanel;
    public bool IsPause;
    // Use this for initialization

    void Start ()
    {
        IsPause = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ActivePanel();
    }

    public void PauseBtn()  { if (!IsPause) IsPause = true;  }
    public void ResumeBtn() { if  (IsPause) IsPause = false; }


    public void ActivePanel()
    {
        if (IsPause)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
        }
    }
}
