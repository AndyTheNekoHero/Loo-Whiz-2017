using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {
    public void LoadSceneNum(int num)
    {
        Debug.LogWarning("Can't load scene number: " + num);

        LoadingScreenManager.LoadScene(num);
    }	
}
