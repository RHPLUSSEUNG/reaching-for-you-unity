using UnityEngine;
using UnityEngine.UI;

public class BarUI : UI_Scene
{
    enum barUI
    {
        Bar
    }
    Image bar;
    float ratio;

    public override void Init()
    {
        Bind<GameObject>(typeof(barUI));
        bar = GetObject((int)barUI.Bar).GetComponent<Image>();
    }

    public void SetPlayerStat(int stat, int maxStat)
    {
        ratio = stat / (float)maxStat;
        bar.fillAmount = ratio;
    }
}
