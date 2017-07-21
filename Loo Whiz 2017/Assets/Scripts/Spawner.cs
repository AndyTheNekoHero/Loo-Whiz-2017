using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float countdown = 0f;
    public float SpawnTime = 1.0f;
    private bool spawnedSuccessfully;
    public int MaxTotalCustomer = 5;
    public ObjectPool Customer;
    private Animator Door;

    void Start()
    {
        countdown = SpawnTime;
        Door = GameObject.Find("Entrance_Door").GetComponent<Animator>();
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
<<<<<<< HEAD
        if (Door.GetCurrentAnimatorStateInfo(0).IsName("Door"))
        {
            Door.SetBool("Enter", false);
        }
        if (GlobalVar.Instance.Tut_Steps == 1)
        {
            MaxTotalCustomer = 2;
        }
        else if (GlobalVar.Instance.Tut_Steps == 6)
        {
            MaxTotalCustomer = 1;
        }
=======
>>>>>>> 0a630130d0d661133cb6f68e068c0726275a89e8
    }

    void Spawn()
    {
        spawnedSuccessfully = false;
        if (GlobalVar.Instance.CustomerCount < MaxTotalCustomer && (!EnviManager.Instance.SinkAllFull()
            || !EnviManager.Instance.BowlAllFull() || !EnviManager.Instance.UrinalAllFull()))
        {
            while (spawnedSuccessfully == false)
            {
                //Instantiate(Customer, transform.position, Quaternion.identity);
                Customer.GetObject();
                Customer.RandomNPC();
                Door.SetBool("Enter", true);
                GlobalVar.Instance.CustomerCount++;
                spawnedSuccessfully = true;
            }
        }
    }
}
