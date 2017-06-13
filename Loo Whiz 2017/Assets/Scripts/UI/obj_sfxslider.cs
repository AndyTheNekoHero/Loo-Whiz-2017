using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obj_sfxslider : MonoBehaviour {
    public Slider selfslider;

    void Start()
    {
        selfslider.value = Globals.Instance.get_sfx_vol();
    }

    public void do_setsfxvol(float val)
    {
        Globals.Instance.set_sfx_vol(val);
    }
}
