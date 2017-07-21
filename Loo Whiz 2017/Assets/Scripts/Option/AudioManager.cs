using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public enum AudioChannel
    {
        Master,
        Music,
        Sfx
    };

    public float masterVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }

    AudioSource sfx2DSource;
    AudioSource musicSources;
    int activeMusicSourceIndex;
    public static AudioManager instance;
    //Transform audioListener;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            GameObject newMusicSource = new GameObject("Music source");
            musicSources = newMusicSource.AddComponent<AudioSource>();
            musicSources.loop = true;
            newMusicSource.transform.parent = transform;


            GameObject newSfx2Dsource = new GameObject("2D sfx source");
            sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
            newSfx2Dsource.transform.parent = transform;

            //audioListener = FindObjectOfType<AudioListener>().transform;

            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
        }

        musicSources.volume = musicVolumePercent * masterVolumePercent;
        sfx2DSource.volume = sfxVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        musicSources.volume = musicVolumePercent * masterVolumePercent;
        musicSources.clip = clip;
        musicSources.Play();

        //StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            sfx2DSource.PlayOneShot(clip, sfxVolumePercent * masterVolumePercent);
        }
    }

    //IEnumerator AnimateMusicCrossfade(float duration)
    //{
    //    float percent = 0;
    //    while (percent < 1)
    //    {
    //        percent += Time.deltaTime * 5 / duration;
    //        musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
    //        musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
    //        yield return null;
    //    }
    //}
}

