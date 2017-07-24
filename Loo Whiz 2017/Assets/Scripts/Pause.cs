using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance;
    
    public GameObject PausePanel;
    public bool IsPause;
    public bool pauseB;
    Character_Button player;
    Rigidbody2D RB2DPlayer;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

    }

    void Start ()
    {
        IsPause = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Button>();
        RB2DPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ActivePanel();
    }

    public void PauseBtn()
    {
        pauseB = true;
        player.ButtonClick = true;
        RB2DPlayer.velocity = Vector2.zero;
        if (!IsPause)
            IsPause = true;
    }
    public void ResumeBtn()
    {
        pauseB = false;
        if  (IsPause)
            IsPause = false;
    }


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

    public void ResetStage()
    {
        GlobalVar.Instance.Win = false;
        GlobalVar.Instance.Lose = false;
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.TotalLitter = 0;
<<<<<<< HEAD
        //GlobalVar.Instance.CustomerCount = 0;
=======
>>>>>>> 56a7f1bbc2427da7bf3e07ceb3808d48a42d036e
        Pause.Instance.IsPause = false;

        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
        GlobalVar.Instance.Roll_Selected    = false;
    }
    public void ResetHighscore()
    {
        GlobalVar.Instance.TimeUsedMins = Mathf.Abs(0);
        GlobalVar.Instance.TimeUsedSecs = Mathf.Abs(0);
        Pause.Instance.IsPause = false;
        GlobalVar.Instance.ToiletPaper = 4;
        GlobalVar.Instance.TotalLitter = 0;

        GlobalVar.Instance.Mop_Selected     = false;
        GlobalVar.Instance.Sweep_Selected   = false;
        GlobalVar.Instance.Wipe_Selected    = false;
        GlobalVar.Instance.Roll_Selected    = false;
    }
}
