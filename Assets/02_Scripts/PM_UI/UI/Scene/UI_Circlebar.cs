using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CircleBar : UI_Scene
{
    Image bar;
    int stat;
    // PlayerStat stat;
    public override void Init()
    {
        bar = Util.FindChild<Image>(gameObject, "Bar");
    }

    private void Update()
    {
        SetRadio();
    }

    public void SetRadio()
    {
        bar.fillAmount = stat;
    }

    public void SetPlayerStat(int changeStat)
    {
        stat = changeStat;
    }
}
