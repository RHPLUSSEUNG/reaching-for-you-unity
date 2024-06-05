using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    public enum HUDUI
    {
        ProfileImage,
        QuickLayout,
        BuffLayout,
        DeBuffLayout,
        Status_EffectLayout,
        HPBar,
        MPBar
    }

    public GameObject _effect;

    BarUI hpBar;
    BarUI mpBar;
    Image profileImage;
    GameObject buffLayout;
    GameObject debuffLayout;
    GameObject status_effectLayout;

    public Image tempImage;     // test

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(HUDUI));

        profileImage = GetObject((int)HUDUI.ProfileImage).GetComponent<Image>();
        hpBar = GetObject((int)HUDUI.HPBar).GetComponent<BarUI>();
        mpBar = GetObject((int)HUDUI.MPBar).GetComponent<BarUI>();
        buffLayout = GetObject((int)HUDUI.BuffLayout);
        debuffLayout = GetObject((int)HUDUI.DeBuffLayout);
        status_effectLayout = GetObject((int)HUDUI.Status_EffectLayout);
    }


    public void CreateStatus(HUDUI type, Image icon = null)
    {
        HUDEffectUI effectUI;
        switch(type)
        {
            case HUDUI.BuffLayout:
                effectUI = PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(buffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
                break;
            case HUDUI.DeBuffLayout:
                effectUI = PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(debuffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
                break;
            case HUDUI.Status_EffectLayout:
                effectUI = PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(status_effectLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
                break;
            default:
                Debug.Log("Incorrect Access");
                break;
        }
    }

    public void ChangeProfile(TestPlayerInfo curInfo)
    {
        hpBar.SetPlayerStat(curInfo.hp, curInfo.maxHp);
        mpBar.SetPlayerStat(curInfo.mp, curInfo.maxMp);
        profileImage.sprite = curInfo.iconImage;
        ChangeEffectUI(curInfo);
    }

    public void ChangeEffectUI(TestPlayerInfo curInfo)
    {
        HUDEffectUI effectUI;
        if (buffLayout.transform.childCount < curInfo.buffList.Count)
        {
            for(int i = buffLayout.transform.childCount; i < curInfo.buffList.Count; i++)
            {
                PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(buffLayout.transform, "Effect");
            }
        }
        for (int i = 0; i < curInfo.buffList.Count; i++)
        {
            effectUI = buffLayout.transform.GetChild(i).gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.buffList[i].GetComponent<Image>();       // Test
            changeIcon = tempImage;      // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effectUI.gameObject);
            }
        }
        if(buffLayout.transform.childCount > curInfo.buffList.Count)
        {
            for (int i = curInfo.buffList.Count; i < buffLayout.transform.childCount; i++)
            {
                PM_UI_Manager.Resource.Destroy(buffLayout.transform.GetChild(i).gameObject);
            }
        }

        if (debuffLayout.transform.childCount < curInfo.debuffList.Count)
        {
            for(int i = debuffLayout.transform.childCount; i < curInfo.debuffList.Count; i++)
            {
                PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(debuffLayout.transform, "Effect");
            }
        }
        for (int i = 0; i < curInfo.debuffList.Count;i++)
        {
            effectUI = debuffLayout.transform.GetChild(i).gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.debuffList[i].GetComponent<Image>();     // Test
            changeIcon = tempImage;     // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effectUI.gameObject);
            }
        }
        if (debuffLayout.transform.childCount > curInfo.debuffList.Count)
        {
            for (int i = curInfo.debuffList.Count; i < debuffLayout.transform.childCount; i++)
            {
                PM_UI_Manager.Resource.Destroy(debuffLayout.transform.GetChild(i).gameObject);
            }
        }

        if (status_effectLayout.transform.childCount < curInfo.seList.Count)
        {
            for (int i = status_effectLayout.transform.childCount; i < curInfo.seList.Count; i++)
            {
                PM_UI_Manager.UI.MakeSubItem<HUDEffectUI>(status_effectLayout.transform, "Effect");
            }
        }
        for (int i = 0; i < curInfo.seList.Count; i++)
        {
            effectUI = status_effectLayout.transform.GetChild(i).gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.seList[i].GetComponent<Image>();     // Test
            changeIcon = tempImage;     // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effectUI.gameObject);
            }
        }
        if (status_effectLayout.transform.childCount > curInfo.seList.Count)
        {
            for (int i = curInfo.seList.Count; i < status_effectLayout.transform.childCount; i++)
            {
                PM_UI_Manager.Resource.Destroy(status_effectLayout.transform.GetChild(i).gameObject);
            }
        }
    }
}