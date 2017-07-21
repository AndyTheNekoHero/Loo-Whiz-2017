using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_spawner : MonoBehaviour {

    public float countdown = 0f;
    public float SpawnTime = 1.0f;
    private bool spawnedSuccessfully;
    public int MaxTotalCustomer = 5;
    public GameObject Customer;
    void Start()
    {
        countdown = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;
        if (countdown >= SpawnTime)
        {
            Spawn();
            countdown = 0f;
        }
        if (GlobalVar.Instance.Tut_Steps == 1)
        {
            MaxTotalCustomer = 2;
        }
        else if (GlobalVar.Instance.Tut_Steps == 7)
        {
            MaxTotalCustomer = 1;
        }
    }

    void Spawn()
    {
        spawnedSuccessfully = false;
        if (GlobalVar.Instance.CustomerCount < MaxTotalCustomer && (!EnviManager.Instance.SinkAllFull()
            || !EnviManager.Instance.BowlAllFull() || !EnviManager.Instance.UrinalAllFull()))
        {
            while (spawnedSuccessfully == false)
            {
                Instantiate(Customer, transform.position, Quaternion.identity);

                GlobalVar.Instance.CustomerCount++;
                spawnedSuccessfully = true;
            }
        }
    }
}
