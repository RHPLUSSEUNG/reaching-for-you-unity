using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    public enum HUD_UI
    {
        QuickLayout,
        BuffLayout,
        DeBuffLayout,
        Status_EffectLayout,
        HPBar,
        MPBar
    }

    public GameObject _status;

    Image profileImage;
    UI_Bar hpBar;
    UI_Bar mpBar;
    GameObject buffLayout;
    GameObject debuffLayout;
    GameObject status_effectLayout;

    public Image tempImage;     // test

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(HUD_UI));

        GameObject quickLayout = GetObject((int)HUD_UI.QuickLayout);
        GameObject hp = GetObject((int)HUD_UI.HPBar);
        GameObject mp = GetObject((int)HUD_UI.MPBar);
        buffLayout = Get<GameObject>((int)HUD_UI.BuffLayout);
        hpBar = hp.GetComponent<UI_Bar>();
        mpBar = mp.GetComponent<UI_Bar>();
        profileImage = Util.FindChild<Image>(gameObject, "CharacterImage", true);

    }


    public void CreateStatus(Image icon, HUD_UI type)
    {
        GameObject status;
        UI_Status ui_status;
        switch(type)
        {
            case HUD_UI.BuffLayout:
                status = Instantiate(_status, buffLayout.transform);
                ui_status = status.GetOrAddComponent<UI_Status>();
                ui_status.SetStatusImage(icon);
                break;
            case HUD_UI.DeBuffLayout:
                status = Instantiate(_status, debuffLayout.transform);
                ui_status = status.GetOrAddComponent<UI_Status>();
                ui_status.SetStatusImage(icon);
                break;
            case HUD_UI.Status_EffectLayout:
                status = Instantiate(_status, status_effectLayout.transform);
                ui_status = status.GetOrAddComponent<UI_Status>();
                ui_status.SetStatusImage(icon);
                break;
            default:
                Debug.Log("Incorrect Access");
                break;
        }
    }

    public void ChangeProfile(PlayerSpec curInfo, Image changeProfile)
    {
        hpBar.SetPlayerStat(curInfo.hp);            // Max HP ÇÊ¿ä    
        mpBar.SetPlayerStat(curInfo.mp);
        profileImage.sprite = changeProfile.sprite;
        SetStatusLayout(curInfo, buffLayout, HUD_UI.BuffLayout);
        SetStatusLayout(curInfo, debuffLayout, HUD_UI.DeBuffLayout);
        SetStatusLayout(curInfo, status_effectLayout, HUD_UI.Status_EffectLayout);
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
                    UI_Status ui_status = status.gameObject.GetComponent<UI_Status>();
                    Image changeIcon = curInfo.buffs[i].gameObject.GetComponent<Image>();
                    changeIcon = tempImage; // test
                    ui_status.SetStatusImage(changeIcon);
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