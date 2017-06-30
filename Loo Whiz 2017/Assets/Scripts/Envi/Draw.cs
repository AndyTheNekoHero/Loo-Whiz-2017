using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    private bool Occupied = false;
    private bool Graffite = false;

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

    public bool IsGraffite()
    {
        return Graffite;
    }
    public void CreatedGraffite()
    {
        Graffite = true;
    }
    public void GraffiteCleaned()
    {
        Graffite = false;
    }
}
