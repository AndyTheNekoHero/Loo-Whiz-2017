using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathToFollow : MonoBehaviour {

    public GameObject ParentToTakeFrom;
    public List<Transform> pathParent = new List<Transform>();
    public float speed;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    private Vector2 dir;
    public SpriteRenderer SR;
    public bool ExitToilet;
    // Use this for initialization
    void Start () {
        ExitToilet = false;
        //add child
        StartCoroutine(AddChild());
    }
	
	// Update is called once per frame
	void Update () {
        //move to waypoint
        MoveToWaypoint();
    }

    public void FlipX()
    {
        if (dir.x > 0f)
        {
            SR.flipX = false;
        }
    }

    public void MoveToWaypoint()
    {
        //distance between point A and point B
        float dist = Vector2.Distance(pathParent[currentPoint].position, transform.position);

        //Move to waypoint
        transform.position = Vector2.Lerp(transform.position, pathParent[currentPoint].position, Time.deltaTime * speed);

        //true if reach current point
        if (dist <= reachDist)
        {
            currentPoint++;
            //if (currentPoint <= pathParent.Count - 1)
            //{
            //    dir = (pathParent[currentPoint].position - transform.position).normalized;
            //}
        }
        //true if current point ended
        if (currentPoint >= pathParent.Count)
        {
            WaypointEnded();
        }
    }
    public void WaypointEnded()
    {
        //check what AI gonna do
        currentPoint = pathParent.Count - 1;
        if(ExitToilet == false)
            Pee();
        
    }
    public void Pee()
    {
        ExitToilet = true;
        SR.flipX = false;
        //Change path to Pee
        ParentToTakeFrom = GameObject.Find("Pee");
        //add child
        StartCoroutine(AddChild());
        //Move in the path
        MoveToWaypoint();
        
    }
    public void Exit()
    {
    }
    //Loop to add child
    public IEnumerator AddChild()
    {
        foreach (Transform child in ParentToTakeFrom.transform)
        {
            pathParent.Add(child);
        }
        
        yield return null;
    }
    //Render waypoints
    void OnDrawGizmos()
    {
        for (var i = 0; i < ParentToTakeFrom.transform.childCount; i++)
        {
            if (ParentToTakeFrom.transform.GetChild(i) != null)
            {
                Gizmos.DrawWireSphere(ParentToTakeFrom.transform.GetChild(i).position, 0.5f);
            }
        }

    }

}
