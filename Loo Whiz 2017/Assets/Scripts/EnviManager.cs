using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviManager : MonoBehaviour
{
    public static EnviManager Instance;

    private int EmptySlots, EmptySinkSlots, EmptyBowlSlots, EmptyWallSlots;

    public List<Transform> UrinalList = new List<Transform>();
    public List<Transform> BowlList = new List<Transform>();
    public List<Transform> SinkList = new List<Transform>();
    public List<Transform> GraffitiList = new List<Transform>();


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.name == "Urinals")
            {
                foreach (Transform UrinalChild in child)
                {
                    if (UrinalChild.GetComponent<Urinal>())
                    {
                        UrinalList.Add(UrinalChild);
                    }
                }
            }
            if (child.name == "Cubicles")
            {
                foreach (Transform CubicleChild in child)
                {
                    if (CubicleChild.GetComponent<ToiletBowl>())
                    {
                        BowlList.Add(CubicleChild);
                    }
                }
            }
            if (child.name == "Sinks")
            {
                foreach (Transform SinkChild in child)
                {
                    if (SinkChild.GetComponent<Sink>())
                    {
                        SinkList.Add(SinkChild);
                    }
                }
            }
            if (child.name == "Graffiti")
            {
                foreach (Transform GraffitiChild in child)
                {
                    if (GraffitiChild.GetComponent<Draw>())
                    {
                        GraffitiList.Add(GraffitiChild);
                    }
                }
            }
        }
    }

    #region Urinal

    //True if no urinal left
    public bool UrinalAllFull()
    {
        foreach (Transform child in UrinalList)
        {
            if (child.GetComponent<Urinal>().InUse() == false)
            {
                return false;
            }
        }
        return true;
    }
    //Get Empty Urinal
    public int GetEmptyUrinalSlots()
    {
        EmptySlots = 0;
        foreach (Transform child in UrinalList)
        {
            if (child.GetComponent<Urinal>().InUse() == false)
            {
                EmptySlots++;
            }
        }
        return EmptySlots;
    }
    //Get one Transform of an Empty Urinal
    public Transform GetEmptyUrinal()
    {
        foreach (Transform child in UrinalList)
        {
            if (child.GetComponent<Urinal>().InUse() == false)
            {
                child.GetComponent<Urinal>().Occupy();
                //Debug.Log("Child in EnviManager: " + child.name);
                return child;
            }
        }
        return null;
    }
    public bool UrinalMess(Urinal current)
    {
        if (current == null)
            return false;

         if (current.IsPeed())
         {
             return true;
         }

        return false;
    }

    #endregion

    #region Sink

    //True if no Sink left
    public bool SinkAllFull()
    {
        foreach (Transform child in SinkList)
        {
            if (child.GetComponent<Sink>().InUse() == false)
            {
                return false;
            }
        }
        return true;
    }
    //Get Empty Sink
    public int GetEmptySinkSlots()
    {
        EmptySinkSlots = 0;
        foreach (Transform child in SinkList)
        {
            if (child.GetComponent<Sink>().InUse() == false)
            {
                EmptySinkSlots++;
            }
        }
        return EmptySinkSlots;
    }
    //Get one Transform of an Empty Urinal
    public Transform GetEmptySink()
    {
        foreach (Transform child in SinkList)
        {
            if (child.GetComponent<Sink>().InUse() == false)
            {
                child.GetComponent<Sink>().Occupy();
                return child;
            }
        }
        return null;
    }

    public bool SinkMess(Sink current)
    {
        if (current == null)
            return false;

        if (current.IsWaterPuddle())
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Cubicle

    //True if no Sink left
    public bool BowlAllFull()
    {
        foreach (Transform child in BowlList)
        {
            if (child.GetComponent<ToiletBowl>().InUse() == false)
            {
                return false;
            }
        }
        return true;
    }
    //Get Empty Sink
    public int GetEmptyBowlSlots()
    {
        EmptyBowlSlots = 0;
        foreach (Transform child in BowlList)
        {
            if (child.GetComponent<ToiletBowl>().InUse() == false)
            {
                EmptyBowlSlots++;
            }
        }
        return EmptyBowlSlots;
    }
    //Get one Transform of an Empty Urinal
    public Transform GetEmptyBowl()
    {
        foreach (Transform child in BowlList)
        {
            if (child.GetComponent<ToiletBowl>().InUse() == false)
            {
                child.GetComponent<ToiletBowl>().Occupy();
                return child;
            }
        }
        return null;
    }

    public bool ShitMess(ToiletBowl current)
    {
        if (current == null)
            return false;

        if (current.IsShit())
        {
            return true;
        }

        return false;
    }

    public bool RollMess(ToiletBowl current)
    {
        if (current == null)
            return false;

        if (current.IsToiletPaper())
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Graffiti

    //True if no Sink left
    public bool AllDrawn()
    {
        foreach (Transform child in GraffitiList)
        {
            if (child.GetComponent<Draw>().IsDrawing() == false)
            {
                return false;
            }
        }
        return true;
    }
    //Get Empty Sink
    public int GetEmptyWallSlots()
    {
        EmptyWallSlots = 0;
        foreach (Transform child in GraffitiList)
        {
            if (child.GetComponent<Draw>().IsDrawing() == false)
            {
                EmptyWallSlots++;
            }
        }
        return EmptyWallSlots;
    }
    //Get one Transform of an Empty Urinal
    public Transform GetEmptyWall()
    {
        foreach (Transform child in GraffitiList)
        {
            if (child.GetComponent<Draw>().IsDrawing() == false)
            {
                child.GetComponent<Draw>().HadDrawn();
                return child;
            }
        }
        return null;
    }

    public bool GraffitiMess(Draw current)
    {
        if (current == null)
            return false;

        if (current.IsGraffite())
        {
            return true;
        }

        return false;
    }

    #endregion

}