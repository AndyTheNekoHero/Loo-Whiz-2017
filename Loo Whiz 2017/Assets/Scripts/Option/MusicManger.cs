using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManger : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    //private string sceneName;

    void Start()
    {
        PlayMusic();
    }

    //void LevelStart(int sceneIndex)
    //{
    //    string newSceneName = SceneManager.GetActiveScene().name;
    //    if (newSceneName != sceneName)
    //    {
    //        sceneName = newSceneName;
    //        Invoke("PlayMusic", .2f);
    //    }
    //}

    void PlayMusic()
    {
        AudioClip clipToPlay = null;
        clipToPlay = mainTheme;
        if (clipToPlay != null)
        {
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }

}
