using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess_Pee : MonoBehaviour
{
    private Transform Player;
    private bool InRange = false;
    private float Cleaning = 0.0f;
    public float CleaningTime = 5.0f;

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

        if (InRange && GlobalVar.Instance.Mop_Selected)
        {
            GlobalVar.Instance.IsEnableInput = false;
            Cleaning += Time.deltaTime;
        }

        if (Cleaning >= CleaningTime)
        {
            GlobalVar.Instance.IsEnableInput = true;
            Destroy(gameObject);
        }
    }
}
