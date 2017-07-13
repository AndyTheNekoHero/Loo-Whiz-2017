using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private GameObject Panel;
    public Text Content;
    public Text FadeText;
    public bool FadeCheck;
    // Use this for initialization
    void Start () {


        Panel = this.gameObject;
        
        if (Panel.activeSelf == true)
        {
            Content.text = "Welcome to the world of LOO(Let's Observe Ourselves) WHIZ! Let's Begin the tutorial. \n \n Let's play this game to better appreciate the attendants' effort. Let's Observe Ourselves (LOO) in keeping our toilets clean so as to lighten the attendants' workload!";

        }
	}

	// Update is called once per frame
	void Update () {

        if (/*Input.GetKeyDown(KeyCode.M)*/Content.color.a < 0)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, FadeText));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(FadeTextToZeroAlpha(1f, FadeText));
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}

