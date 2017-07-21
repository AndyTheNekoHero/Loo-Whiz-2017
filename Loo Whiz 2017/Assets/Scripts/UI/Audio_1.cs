using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_1 : MonoBehaviour
{
    public AudioClip Click;

    public void do_plysnd_btnpress()
    {
        AudioManager.instance.PlaySound(Click, transform.position);
    }
}
