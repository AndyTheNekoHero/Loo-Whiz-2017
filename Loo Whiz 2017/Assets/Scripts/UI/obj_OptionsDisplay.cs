using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class obj_OptionsDisplay : MonoBehaviour {

    Vector3 default_position;
    Vector3 inactive_position;

    // Use this for initialization
    void Start () {
        default_position = transform.localPosition;
        inactive_position = new Vector3(-62.46115f, -9999.0f, -1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        // Move into view only in OptionScene; else stay far, far out of the touchscreen
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Active scene is '" + scene.name + "'.");
        Debug.Log(transform.localPosition);
        if (scene.name == "OptionScene")
        {
            transform.localPosition = default_position;
        }
        else
        {
            transform.localPosition = inactive_position;
        }
        
    }
}