using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour {

    public bool Occupied = false;
    private bool WaterPuddle = false;
    public Animator anim;
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

    public bool IsWaterPuddle()
    {
        return WaterPuddle;
    }
    public void CreatedWaterPuddle()
    {
        WaterPuddle = true;
    }
    public void WaterPuddleCleaned()
    {
        WaterPuddle = false;
    }

    public void PlayAnimation()
    {
        anim.SetBool("Using", true);
    }
    public void StopAnimation()
    {
        anim.SetBool("Using", false);
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
            //Debug.Log("I AM COLLIDED");
            P_Block = true;
        }
    }
    public void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "PathBlock")
        {
            //Debug.Log("I EXIT!");
            P_Block = false;
        }
    }
}
