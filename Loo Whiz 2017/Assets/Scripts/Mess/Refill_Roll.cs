using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refill_Roll : MonoBehaviour
{
    private Transform Player;
    private bool InRange = false;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector2.Distance(Player.transform.position, transform.position);
        if (dis < 0.5f)
            InRange = true;
        else
            InRange = false;

        if (InRange && GlobalVar.Instance.Roll_Selected)
        {
            Destroy(gameObject);
        }
    }
}
