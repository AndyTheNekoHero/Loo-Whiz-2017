using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.aspect = 856f / 535f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
