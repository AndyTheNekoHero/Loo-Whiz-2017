using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Notifier : MonoBehaviour {

    public Text Stage_Name;
    Scene scene;

	// Use this for initialization
	void Start () {
        scene = SceneManager.GetActiveScene();
        Stage_Name.text = scene.name;
	}
	
}
