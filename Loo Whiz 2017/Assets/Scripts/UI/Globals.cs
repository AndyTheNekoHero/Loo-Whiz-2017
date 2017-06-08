using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    // 'pointer'
    public static Globals Instance;

    // Audio, common
    private AudioSource source;
    float sfx_vol = 1.0f;
    public AudioClip snd_btnpress;

    public static int selected_char = 1;

	// Use this for initialization
	void Start () {
		
	}
	
    void Awake ()
    {
        // Keep object persistent between scenes
        DontDestroyOnLoad(transform.gameObject);

        // 'Pointer' must be 'initialised' to enable 'linking' between objects
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Audio
        source = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {

	}
    // ================== Gameplay Commands ================== //
    public void set_char(int choice)
    {
        selected_char = choice;
        Debug.Log(selected_char);
    }
    // ================== Audio Commands ================== //
    public void plysnd_btnpress()
    {
        source.PlayOneShot(snd_btnpress, 1.0f);
    }

    public void set_sfx_vol(float val)
    {
        source.volume = sfx_vol = val;
    }

    public float get_sfx_vol()
    {
        return sfx_vol;
    }
}
