using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelector1 : MonoBehaviour
{
    public void MaleSelected()
    {
        GlobalVar.Instance.Gender = 1;
    }

    public void FemaleSelected()
    {
        GlobalVar.Instance.Gender = 0;
    }
}
