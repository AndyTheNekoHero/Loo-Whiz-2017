using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float countdown = 0f;
    public float SpawnTime = 1.0f;
    private bool spawnedSuccessfully;
    public int MaxTotalCustomer = 5;
    public ObjectPool Cust;

    void Start()
    {
        countdown = SpawnTime;
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
        if (GlobalVar.Instance.CustomerCount < MaxTotalCustomer)
        {
            while (spawnedSuccessfully == false)
            {
                //Instantiate(Customer, transform.position, Quaternion.identity);
                Cust.GetObject();
                GlobalVar.Instance.CustomerCount++;
                spawnedSuccessfully = true;
            }
        }
    }
}
