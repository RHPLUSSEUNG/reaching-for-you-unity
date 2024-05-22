using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    public enum HUD_UI
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

        Bind<GameObject>(typeof(HUD_UI));

        profileImage = GetObject((int)HUD_UI.ProfileImage).GetComponent<Image>();
        hpBar = GetObject((int)HUD_UI.HPBar).GetComponent<BarUI>();
        mpBar = GetObject((int)HUD_UI.MPBar).GetComponent<BarUI>();
        buffLayout = GetObject((int)HUD_UI.BuffLayout);
        debuffLayout = GetObject((int)HUD_UI.DeBuffLayout);
        status_effectLayout = GetObject((int)HUD_UI.Status_EffectLayout);
    }


    public void CreateStatus(Image icon, HUD_UI type)
    {
        GameObject effect;
        HUDEffectUI effectUI;
        switch(type)
        {
            case HUD_UI.BuffLayout:
                effect = Instantiate(_effect, buffLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            case HUD_UI.DeBuffLayout:
                effect = Instantiate(_effect, debuffLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            case HUD_UI.Status_EffectLayout:
                effect = Instantiate(_effect, status_effectLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            default:
                Debug.Log("Incorrect Access");
                break;
        }
    }

    public void ChangeProfile(TestPlayerInfo curInfo)
    {
        hpBar.SetPlayerStat(curInfo.hp);
        mpBar.SetPlayerStat(curInfo.mp);
        profileImage.sprite = curInfo.iconImage;
        ChangeEffectUI(curInfo);
    }

    public void ChangeEffectUI(TestPlayerInfo curInfo)
    {
        if (buffLayout.transform.childCount < curInfo.buffList.Count)
        {
            for(int i = buffLayout.transform.childCount; i < curInfo.buffList.Count; i++)
            {
                Instantiate(_effect, buffLayout.transform);
            }
        }
        for (int i = 0; i < curInfo.buffList.Count; i++)
        {
            Transform effect = buffLayout.transform.GetChild(i);
            HUDEffectUI effectUI = effect.gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.buffList[i].GetComponent<Image>();       // Test
            changeIcon = tempImage;      // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effect.gameObject);
            }
        }
        if(buffLayout.transform.childCount > curInfo.buffList.Count)
        {
            for (int i = curInfo.buffList.Count; i < buffLayout.transform.childCount; i++)
            {
                GameObject effect = buffLayout.transform.GetChild(i).gameObject;
                Destroy(effect);
            }
        }

        if (debuffLayout.transform.childCount < curInfo.debuffList.Count)
        {
            for(int i = debuffLayout.transform.childCount; i < curInfo.debuffList.Count; i++)
            {
                Instantiate(_effect, debuffLayout.transform);
            }
        }
        for (int i = 0; i < curInfo.debuffList.Count;i++)
        {
            Transform effect = debuffLayout.transform.GetChild(i);
            HUDEffectUI effectUI = effect.gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.debuffList[i].GetComponent<Image>();
            changeIcon = tempImage;     // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effect.gameObject);
            }
        }
        if (debuffLayout.transform.childCount > curInfo.debuffList.Count)
        {
            for (int i = curInfo.debuffList.Count; i < debuffLayout.transform.childCount; i++)
            {
                GameObject effect = debuffLayout.transform.GetChild(i).gameObject;
                Destroy(effect);
            }
        }

        if (status_effectLayout.transform.childCount < curInfo.seList.Count)
        {
            for (int i = status_effectLayout.transform.childCount; i < curInfo.seList.Count; i++)
            {
                Instantiate(_effect, status_effectLayout.transform);
            }
        }
        for (int i = 0; i < curInfo.seList.Count; i++)
        {
            Transform effect = status_effectLayout.transform.GetChild(i);
            HUDEffectUI effectUI = effect.gameObject.GetComponent<HUDEffectUI>();
            Image changeIcon = curInfo.seList[i].GetComponent<Image>();
            changeIcon = tempImage;     // Test
            effectUI.SetStatusImage(changeIcon);
            if (i > effectUI.Max_Display_Child - 1)
            {
                PM_UI_Manager.UI.HideUI(effect.gameObject);
            }
        }
        if (status_effectLayout.transform.childCount > curInfo.seList.Count)
        {
            for (int i = curInfo.seList.Count; i < status_effectLayout.transform.childCount; i++)
            {
                GameObject effect = status_effectLayout.transform.GetChild(i).gameObject;
                Destroy(effect);
            }
        }
    }
}