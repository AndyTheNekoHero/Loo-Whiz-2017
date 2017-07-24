using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceMeter : MonoBehaviour
{
    public Sprite Img1;
    public Sprite Img2;
    public Sprite Img3;
    public Sprite Img4;
    public Sprite Img5;

    private SpriteRenderer currentSprite;
    // Use this for initialization
    void Start ()
    {
        currentSprite = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GlobalVar.Instance.MeterValue <= -4)
            currentSprite.sprite = Img5;

    }
}
