using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {
    public static InfoPanel Instance;

    public List<GameObject> Panels = new List<GameObject>();
    public GameObject Fade, Next, Back;

    private int ChildCount = 0;

    public bool Welcome = true;
    public bool TutPart2 = false;
    public bool TutPart3 = false;

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

        Fade.SetActive(true);
        Next.SetActive(true);
        

        foreach (Transform child in transform)
        {
            Panels.Add(child.transform.gameObject);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (ChildCount == Panels.Count - 1)
        {
            Next.SetActive(false);
        }
    }

    public void Skip()
    {
        Panels[ChildCount].SetActive(false);
        Back.SetActive(false);
        ChildCount = Panels.Count - 1;
        Panels[ChildCount].SetActive(true);
    }

    public void Nextbtn()
    {
        ChildCount++;
        Panels[ChildCount].SetActive(true);

        if (ChildCount != 0)
            Panels[ChildCount - 1].SetActive(false);

        if (ChildCount > 0)
            Back.SetActive(true);
        else
            Back.SetActive(false);
        #region Dialogue
        if (ChildCount == 4 && Welcome == true)
        {
            Welcome = false;
            transform.gameObject.SetActive(false);
            Fade.SetActive(false);
            Next.SetActive(false);
            Back.SetActive(false);
        }
        else if (ChildCount == 5 && Welcome == true)
        {
            Welcome = false;
            transform.gameObject.SetActive(false);
            Fade.SetActive(false);
            Next.SetActive(false);
            Back.SetActive(false);
        }
        else if (ChildCount == 6 && Welcome == true)
        {
            Welcome = false;
            transform.gameObject.SetActive(false);
            Fade.SetActive(false);
            Next.SetActive(false);
            Back.SetActive(false);
        }
        #endregion
    }
    public void Backbtn()
    {
        if (ChildCount == 0)
            return;

        ChildCount--;
        Panels[ChildCount].SetActive(true);

        if (ChildCount != Panels.Count - 1)
            Panels[ChildCount + 1].SetActive(false);

        if (ChildCount > 0)
            Back.SetActive(true);
        else
            Back.SetActive(false);

    }
}


