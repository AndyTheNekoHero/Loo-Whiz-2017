using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    private float Mins,Secs,Score;
    public Text ScoreText1,ScoreText2,ScoreText3;

    public bool GStart = false;

    public InputField NameField;
    public GameObject NameFieldOBJ;
    public GameObject SubmitName;

	// Use this for initialization
	void Start () {

        Mins = GlobalVar.Instance.TimeUsedMins;
        Secs = GlobalVar.Instance.TimeUsedSecs;
        Score = (Mins * 60) + Secs;

        if (Score > PlayerPrefs.GetFloat("LowestTSec") && GlobalVar.Instance.FromStart_HS == false)
        {
            NameFieldOBJ.SetActive(true);
            SubmitName.SetActive(true);
        }
        else
            WriteScore();

    }
	
	// Update is called once per frame
	void Update () {

        //Reset Score
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Score is Reset!");
            PlayerPrefs.DeleteAll();
            WriteScore();

        }
	}
    public void CalculateScore()
    {
        if (Score >= PlayerPrefs.GetFloat("HighestTSec"))
        {
            //Push the Name && Time Down
            PlayerPrefs.SetFloat("LowestMin", PlayerPrefs.GetFloat("MiddleMin"));
            PlayerPrefs.SetFloat("LowestSec", PlayerPrefs.GetFloat("MiddleSec"));
            PlayerPrefs.SetString("LName", PlayerPrefs.GetString("MName"));
            PlayerPrefs.SetFloat("MiddleMin", PlayerPrefs.GetFloat("HighestMin"));
            PlayerPrefs.SetFloat("MiddleSec", PlayerPrefs.GetFloat("HighestSec"));
            PlayerPrefs.SetString("MName", PlayerPrefs.GetString("HName"));


            //Saving Name
            PlayerPrefs.SetString("HName", NameField.text);
            //Saving Time
            PlayerPrefs.SetFloat("HighestMin", Mins);
            PlayerPrefs.SetFloat("HighestSec", Secs);

            Score = (Mins * 60) + Secs;
            PlayerPrefs.SetFloat("HighestTSec", Score);
        }
        else if (Score < PlayerPrefs.GetFloat("HighestTSec") && Score >= PlayerPrefs.GetFloat("MiddleTSec"))
        {
            //if(same or greater)
            PlayerPrefs.SetFloat("LowestMin", PlayerPrefs.GetFloat("MiddleMin"));
            PlayerPrefs.SetFloat("LowestSec", PlayerPrefs.GetFloat("MiddleSec"));
            PlayerPrefs.SetString("LName", PlayerPrefs.GetString("MName"));

            PlayerPrefs.SetString("MName", NameField.text);

            PlayerPrefs.SetFloat("MiddleMin", Mins);
            PlayerPrefs.SetFloat("MiddleSec", Secs);

            Score = (Mins * 60) + Secs;
            PlayerPrefs.SetFloat("MiddleTSec", Score);

        }
        else if (Score < PlayerPrefs.GetFloat("MiddleTSec") && Score >= PlayerPrefs.GetFloat("LowestTSec"))
        {
            PlayerPrefs.SetFloat("LowestMin", Mins);
            PlayerPrefs.SetFloat("LowestSec", Secs);

            Score = (Mins * 60) + Secs;
            PlayerPrefs.SetFloat("LowestTSec", Score);
        }
        
    }

    public void SubmitBtn()
    {

        CalculateScore();

        WriteScore();

        NameField.text = "";
        NameFieldOBJ.SetActive(false);
        SubmitName.SetActive(false);
    }
    public void WriteScore()
    {

        if (PlayerPrefs.GetString("LName") == "")
        {
            PlayerPrefs.SetString("LName", "*****");
        }
        if (PlayerPrefs.GetString("MName") == "")
        {
            PlayerPrefs.SetString("MName", "*****");
        }
        if (PlayerPrefs.GetString("HName") == "")
        {
            PlayerPrefs.SetString("HName", "*****");
        }

        #region Min < 10 Sec < 10
        if (PlayerPrefs.GetFloat("HighestMin") < 10 && PlayerPrefs.GetFloat("HighestSec") < 10)
        {
            ScoreText1.text = "Rank 1: " + PlayerPrefs.GetString("HName") + ": 0" + (int)PlayerPrefs.GetFloat("HighestMin") + ":0" + (int)PlayerPrefs.GetFloat("HighestSec");
        }
        if (PlayerPrefs.GetFloat("MiddleMin") < 10 && PlayerPrefs.GetFloat("MiddleSec") < 10)
        {
            ScoreText2.text = "Rank 2: " + PlayerPrefs.GetString("MName") + ": 0" + (int)PlayerPrefs.GetFloat("MiddleMin") + ":0" + (int)PlayerPrefs.GetFloat("MiddleSec");
        }
        if (PlayerPrefs.GetFloat("LowestMin") < 10 && PlayerPrefs.GetFloat("LowestSec") < 10)
        {
            ScoreText3.text = "Rank 3: " + PlayerPrefs.GetString("LName") + ": 0" + (int)PlayerPrefs.GetFloat("LowestMin") + ":0" + (int)PlayerPrefs.GetFloat("LowestSec");
        }
        #endregion

        #region Min < 10 Sec >10
        if (PlayerPrefs.GetFloat("HighestMin") < 10 && PlayerPrefs.GetFloat("HighestSec") > 10)
        {
            ScoreText1.text = "Rank 1: " + PlayerPrefs.GetString("HName") + ": 0" + (int)PlayerPrefs.GetFloat("HighestMin") + ":" + (int)PlayerPrefs.GetFloat("HighestSec");
        }
        if (PlayerPrefs.GetFloat("MiddleMin") < 10 && PlayerPrefs.GetFloat("MiddleSec") > 10)
        {
            ScoreText2.text = "Rank 2: " + PlayerPrefs.GetString("MName") + ": 0" + (int)PlayerPrefs.GetFloat("MiddleMin") + ":" + (int)PlayerPrefs.GetFloat("MiddleSec");
        }
        if (PlayerPrefs.GetFloat("LowestMin") < 10 && PlayerPrefs.GetFloat("LowestSec") > 10)
        {
            ScoreText3.text = "Rank 3: " + PlayerPrefs.GetString("LName") + ": 0" + (int)PlayerPrefs.GetFloat("LowestMin") + ":" + (int)PlayerPrefs.GetFloat("LowestSec");
        }
        #endregion

        #region Min > 10 Sec < 10
        if (PlayerPrefs.GetFloat("HighestMin") > 10 && PlayerPrefs.GetFloat("HighestSec") < 10)
        {
            ScoreText1.text = "Rank 1: " + PlayerPrefs.GetString("HName") + ": " + (int)PlayerPrefs.GetFloat("HighestMin") + ":0" + (int)PlayerPrefs.GetFloat("HighestSec");
        }
        if (PlayerPrefs.GetFloat("MiddleMin") > 10 && PlayerPrefs.GetFloat("MiddleSec") < 10)
        {
            ScoreText2.text = "Rank 2: " + PlayerPrefs.GetString("MName") + ": " + (int)PlayerPrefs.GetFloat("MiddleMin") + ":0" + (int)PlayerPrefs.GetFloat("MiddleSec");
        }
        if (PlayerPrefs.GetFloat("LowestMin") > 10 && PlayerPrefs.GetFloat("LowestSec") < 10)
        {
            ScoreText3.text = "Rank 3: " + PlayerPrefs.GetString("LName") + ": " + (int)PlayerPrefs.GetFloat("LowestMin") + ":0" + (int)PlayerPrefs.GetFloat("LowestSec");
        }
        #endregion

        #region Min > 10 Sec > 10 
        if (PlayerPrefs.GetFloat("HighestMin") > 10 && PlayerPrefs.GetFloat("HighestSec") > 10)
        { ScoreText1.text = "Rank 1: " + PlayerPrefs.GetString("HName") + (int)PlayerPrefs.GetFloat("HighestMin") + ":" + (int)PlayerPrefs.GetFloat("HighestSec");  }
        if (PlayerPrefs.GetFloat("MiddleMin") > 10 && PlayerPrefs.GetFloat("MiddleSec") > 10)
        { ScoreText2.text = "Rank 2: " + PlayerPrefs.GetString("MName") + (int)PlayerPrefs.GetFloat("MiddleMin") + ":" + (int)PlayerPrefs.GetFloat("MiddleSec");    }
        if (PlayerPrefs.GetFloat("LowestMin") > 10 && PlayerPrefs.GetFloat("LowestSec") > 10)
        { ScoreText3.text = "Rank 3: " + PlayerPrefs.GetString("LName") + (int)PlayerPrefs.GetFloat("LowestMin") + ":" + (int)PlayerPrefs.GetFloat("LowestSec");    }
        #endregion
    }
}
