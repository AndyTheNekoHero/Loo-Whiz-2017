using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class module_objpersistence : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
