﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float countdown = 0f;
    public float SpawnTime = 1.0f;
    private bool spawnedSuccessfully;
    private int currentCustomer = 0;
    public int MaxTotalCustomer = 5;
    public ObjectPool Cust;

    void Start()
    {
    }
    
    // Update is called once per frame
	void Update ()
    {
        countdown += Time.deltaTime;
        if (countdown >= SpawnTime)
        {
            Spawn();
            countdown = 0f;
        }
    }

    void Spawn()
    {
        spawnedSuccessfully = false;
        if (currentCustomer < MaxTotalCustomer)
        {
            while (spawnedSuccessfully == false)
            {
                //Instantiate(Customer, transform.position, Quaternion.identity);
                Cust.GetObject();
                currentCustomer++;
                spawnedSuccessfully = true;
            }
        }
    }
}