using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Button : MonoBehaviour
{
    public GameObject Buttons_Panel;
    public GameObject Volume_Panel;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void On_Volume()
    {
        Buttons_Panel.SetActive(false);
        Volume_Panel.SetActive(true);
    }
    public void Off_Volume()
    {
        Buttons_Panel.SetActive(true);
        Volume_Panel.SetActive(false);
    }
}
