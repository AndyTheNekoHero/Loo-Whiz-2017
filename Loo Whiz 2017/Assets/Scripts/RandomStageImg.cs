using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStageImg : MonoBehaviour
{
    public Sprite Img1;
    public Sprite Img2;
    public Sprite Img3;

    private SpriteRenderer CurrentImg;
	// Use this for initialization
	void Start ()
    {
        CurrentImg = GetComponent<SpriteRenderer>();

        int r = Random.Range(1, 4);
        switch (r)
        {
            case 1:
                {
                    CurrentImg.sprite = Img1;
                }
                break;
            case 2:
                {
                    CurrentImg.sprite = Img2;
                }
                break;
            case 3:
                {
                    CurrentImg.sprite = Img3;
                }
                break;
        }
    }
}
