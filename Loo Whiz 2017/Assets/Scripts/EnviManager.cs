using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviManager : MonoBehaviour {
    public static EnviManager Instance;

    Urinal urinal;
    ToiletBowl toiletbowl;
    Sink sink;

    public List<Transform> UrinalList = new List<Transform>();
    public List<Transform> BowlList = new List<Transform>();
    public List<Transform> SinkList = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Urinal>())
            {
                UrinalList.Add(child);
            }
            if (child.GetComponent<ToiletBowl>())
            {
                BowlList.Add(child);
            }
            if (child.GetComponent<Sink>())
            {
                SinkList.Add(child);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
