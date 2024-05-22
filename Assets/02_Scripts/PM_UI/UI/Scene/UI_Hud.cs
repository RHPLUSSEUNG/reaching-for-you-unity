using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    public enum HUD_UI
    {
        Profile,
        QuickLayout,
        BuffLayout,
        DeBuffLayout,
        Status_EffectLayout,
        HPBar,
        MPBar
    }

    public GameObject _status;

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

        profileImage = GetObject((int)HUD_UI.Profile).GetComponent<Image>();
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
                effect = Instantiate(_status, buffLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            case HUD_UI.DeBuffLayout:
                effect = Instantiate(_status, debuffLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            case HUD_UI.Status_EffectLayout:
                effect = Instantiate(_status, status_effectLayout.transform);
                effectUI = effect.GetOrAddComponent<HUDEffectUI>();
                effectUI.SetStatusImage(icon);
                break;
            default:
                Debug.Log("Incorrect Access");
                break;
        }
    }

    public void ChangeProfile(PlayerSpec curInfo, Image changeProfile)
    {
        hpBar.SetPlayerStat(curInfo.hp);
        mpBar.SetPlayerStat(curInfo.mp);
        profileImage.sprite = changeProfile.sprite;
        //SetStatusLayout(curInfo, buffLayout, HUD_UI.BuffLayout);
        //SetStatusLayout(curInfo, debuffLayout, HUD_UI.DeBuffLayout);
        //SetStatusLayout(curInfo, status_effectLayout, HUD_UI.Status_EffectLayout);
        // ChangeEffectUI(curInfo);
    }

    public void ChangeEffectUI(PlayerSpec curInfo)
    {
        if (buffLayout.transform.childCount < curInfo.buffs.Count)
        {
            for(int i = buffLayout.transform.childCount; i < curInfo.buffs.Count; i++)
            {
                Instantiate(_status, buffLayout.transform);
            }
        }
        for (int i = 0; i < curInfo.buffs.Count; i++)
        {
            Transform status = buffLayout.transform.GetChild(i);
            HUDEffectUI uI_Status = status.gameObject.GetComponent<HUDEffectUI>();
            // Image changeIcon = curInfo.buffs[i].gameObject.GetComponent<Image>();
            // changeIcon = tempImage;      // Test
            // uI_Status.SetStatusImage(changeIcon);
            //if(i > uI_Status.Max_Display_Child - 1)
            //{
            //    status.gameObject.SetActive(false);
            //}
        }
        if(buffLayout.transform.childCount > curInfo.buffs.Count)
        {
            for (int i = curInfo.buffs.Count; i < buffLayout.transform.childCount; i++)
            {
                GameObject status = buffLayout.transform.GetChild(i).gameObject;
                Destroy(status);
            }
        }
        // Debuff, Status_Effect도 동일하게
    }

    void SetStatusLayout(PlayerSpec curInfo, GameObject layout, HUD_UI type)
    {
        int count, curCount;
        switch(type)
        {
            case HUD_UI.BuffLayout:
                count = layout.transform.childCount;
                curCount = curInfo.buffs.Count;
                if(count < curCount)
                {
                    for (int i = count; i < curCount; i++)
                    {
                        Instantiate(_status, layout.transform);
                    }
                }
                for (int i = 0; i < curCount; i++)
                {
                    Transform status = layout.transform.GetChild(i);
                    HUDEffectUI ui_status = status.gameObject.GetComponent<HUDEffectUI>();
                    // Image changeIcon = curInfo.buffs[i].gameObject.GetComponent<Image>();
                    // changeIcon = tempImage; // test
                    // ui_status.SetStatusImage(changeIcon);
                }
                if (count > curCount)
                {
                    for (int i = curCount; i < count; i++)
                    {
                        Transform status = layout.transform.GetChild(i);
                        Destroy(status.gameObject);
                    }
                }
                break;
            case HUD_UI.DeBuffLayout:
                //count = layout.transform.childCount;
                //curCount = curInfo.debuffs.Count;
                //for (int i = 0; i < curCount; i++)
                //{
                //    Transform status = layout.transform.GetChild(i);
                //    if (status == null)
                //    {
                //        status = Instantiate(_status, layout.transform).transform;
                //    }
                //    UI_Status ui_status = status.gameObject.GetComponent<UI_Status>();
                //    Image changeIcon = curInfo.debuffs[i].gameObject.GetComponent<Image>();
                //    ui_status.SetStatusImage(changeIcon);
                //}
                //if (count > curCount)
                //{
                //    for (int i = curCount; i < count; i++)
                //    {
                //        Transform status = layout.transform.GetChild(i);
                //        Destroy(status.gameObject);
                //    }
                //}
                break;
            case HUD_UI.Status_EffectLayout:
                //count = layout.transform.childCount;
                //curCount = curInfo.status_effect.Count;
                //for (int i = 0; i < curCount; i++)
                //{
                //    Transform status = layout.transform.GetChild(i);
                //    if (status == null)
                //    {
                //        status = Instantiate(_status, layout.transform).transform;
                //    }
                //    UI_Status ui_status = status.gameObject.GetComponent<UI_Status>();
                //    Image changeIcon = curInfo.status_effectLayout[i].gameObject.GetComponent<Image>();
                //    ui_status.SetStatusImage(changeIcon);
                //}
                //if (count > curCount)
                //{
                //    for (int i = curCount; i < count; i++)
                //    {
                //        Transform status = layout.transform.GetChild(i);
                //        Destroy(status.gameObject);
                //    }
                //}
                break;
        }
        
    }
}