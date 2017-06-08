using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelector1 : MonoBehaviour {
    public void CharSelect(int choice)
    {
        Globals.Instance.set_char(choice);
    }
}
