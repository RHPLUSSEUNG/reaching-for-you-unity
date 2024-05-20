using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : UI_Scene
{
    Image bar;
    int stat;
    float ratio;

    int tempMax = 100;
    public override void Init()
    {
        bar = Util.FindChild<Image>(gameObject, "Bar");
    }

    public void SetPlayerStat(int changeStat)
    {
        stat = changeStat;
        ratio = stat / (float)tempMax;
        bar.fillAmount = ratio;
    }
}
