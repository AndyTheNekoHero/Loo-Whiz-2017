using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obj_bgmslider : MonoBehaviour {
    public Slider selfslider;

    void Start()
    {
        selfslider.value = Globals.Instance.get_bgm_vol();
    }

    public void do_setbgmvol(float val)
    {
        Globals.Instance.set_bgm_vol(val);
    }
}
