using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urinal : MonoBehaviour {

    private bool Occupied = false;
    public bool Peed = false;
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

    public bool IsPeed()
    {
        return Peed;
    }
    public void CreatedPeeMess()
    {
        Peed = true;
    }
    public void PeeCleaned()
    {
        Peed = false;
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
            P_Block = true;
        }
    }
    public void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "PathBlock")
        {
            P_Block = false;
        }
    }
}
