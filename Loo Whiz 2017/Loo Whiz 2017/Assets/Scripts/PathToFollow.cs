using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathToFollow : MonoBehaviour {
    public static PathToFollow Instance = null;

    public static List<Transform> pathParent = new List<Transform>();
    public float speed = 1000f;
    public float reachDist = 0.1f;
    public int currentPoint = 0;
    public bool ExitToilet;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(pathParent.Count);
    }

    public void MoveToWaypoint()
    {
        NPC_Script NPC = GameObject.Find("Customer").GetComponent<NPC_Script>();
        //distance between point A and point B
        float dist = Vector2.Distance(pathParent[currentPoint].position, NPC.transform.position);
        //Move to waypoint

        NPC.transform.position = Vector2.Lerp(NPC.transform.position, pathParent[currentPoint].position, Time.deltaTime * speed);

        //true if reach current point
        if (dist <= reachDist)
        {
            currentPoint++;
        }
        WaypointEnded();

    }
    public bool WaypointEnded()
    { 
        if (currentPoint >= pathParent.Count-1)
        {
        currentPoint = pathParent.Count - 1;
        return true;
        }
        return false;
    }

    ////Render waypoints
    //void OnDrawGizmos()
    //{
    //    for (var i = 0; i < ParentToTakeFrom.transform.childCount; i++)
    //    {
    //        if (ParentToTakeFrom.transform.GetChild(i) != null)
    //        {
    //            Gizmos.DrawWireSphere(ParentToTakeFrom.transform.GetChild(i).position, 0.5f);
    //        }
    //    }

    //}

}
