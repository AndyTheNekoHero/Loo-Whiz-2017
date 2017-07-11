using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneNum(int num)
    {
        LoadingScreenManager.LoadScene(num);
    }

    public void Reset()
    {
        GlobalVar.Instance.Win = false;
        GlobalVar.Instance.Lose = false;
    }
}
