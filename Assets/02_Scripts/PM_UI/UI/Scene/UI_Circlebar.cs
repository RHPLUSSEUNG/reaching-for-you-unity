using UnityEngine.UI;

public class UI_CircleBar : UI_Scene
{
    Image bar;  
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
        bar.fillAmount = 0.5f;
        // bar.fillAmount = stat;
    }

    public void SetPlayerStat()
    {
        // stat.hp or mp
    }
}
