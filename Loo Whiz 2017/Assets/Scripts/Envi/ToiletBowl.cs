using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBowl : MonoBehaviour {

    private bool Occupied = false;
    private bool Shit = false;
    private bool ToiletPaper = false;
    public bool P_Block = false;

    public bool InUse()
    {
        return Occupied;
    }
    public void UnOccupy()
    {
        Occupied = false;
    }
    public void Occupy()
    {
        Occupied = true;
    }

    public bool IsShit()
    {
        return Shit;
    }
    public void CreatedShit()
    {
        Shit = true;
    }
    public void ShitCleaned()
    {
        Shit = false;
    }

    public bool IsToiletPaper()
    {
        return ToiletPaper;
    }
    public void NoToiletPaper()
    {
        ToiletPaper = true;
    }
    public void RestockToiletPaper()
    {
        ToiletPaper = false;
    }

    //Check Path Block
    public bool PathIsBlocked()
    {
        return P_Block;
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "PathBlock")
        {
            Debug.Log("I AM COLLIDED");
            P_Block = true;
        }
    }
    public void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "PathBlock")
        {
            Debug.Log("I EXIT!");
            P_Block = false;
        }
    }
}
