using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    private bool Occupied = false;

    public bool IsDrawing()
    {
        return Occupied;
    }
    public void NotDrawn()
    {
        Occupied = false;
    }
    public void HadDrawn()
    {
        Occupied = true;
    }
}
