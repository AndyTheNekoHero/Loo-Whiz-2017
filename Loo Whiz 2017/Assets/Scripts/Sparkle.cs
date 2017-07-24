using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    float Timer = 0;
    public float Duration = 2;

	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;
        if (Timer >= Duration)
        {
            Destroy(gameObject);
        }
	}
}
