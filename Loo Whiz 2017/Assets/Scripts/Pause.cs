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
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            
        }
        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            
        }
    }
}
