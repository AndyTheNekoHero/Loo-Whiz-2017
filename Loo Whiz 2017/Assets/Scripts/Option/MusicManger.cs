using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManger : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    private string sceneName;

    void Update()
    {
        LevelStart(0);
    }

    void LevelStart(int sceneIndex)
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != sceneName)
        {
            sceneName = newSceneName;
            Invoke("PlayMusic", .2f);
        }
    }

    void PlayMusic()
    {
        AudioClip clipToPlay = null;

        if (sceneName == "MainMenu")
        {
            clipToPlay = menuTheme;
        }
        else if (sceneName == "Tutorial")
        {
            clipToPlay = mainTheme;
        }

        if (clipToPlay != null)
        {
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }

}
