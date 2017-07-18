using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance;
    
    public GameObject PausePanel;
    public bool IsPause;
    bool pauseB;
    // Use this for initialization

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start ()
    {
        IsPause = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ActivePanel();
    }

    public void PauseBtn()  { pauseB = true;    if (!IsPause) IsPause = true;   }
    public void ResumeBtn() { pauseB = false;   if  (IsPause) IsPause = false;  }


    public void ActivePanel()
    {
        if (IsPause)
        {
            Time.timeScale = 0f;
            if (pauseB)
                PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (!pauseB)
                PausePanel.SetActive(false);
        }
    }
}
