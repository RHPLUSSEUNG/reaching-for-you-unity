using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    enum GameObjects
    {
        QuickLayout,
        BuffLayout,
        DeBuffLayout,
        Status_EffectLayout,
        HP,
        MP
    }

    Image profileImage;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject quickLayout = GetObject((int)GameObjects.QuickLayout);
        GameObject hp = GetObject((int)GameObjects.HP);
        GameObject mp = GetObject((int)GameObjects.MP);
        hp.GetOrAddComponent<UI_CircleBar>();
        mp.GetOrAddComponent<UI_CircleBar>();
        //foreach (Transform slot in quickLayout.transform)
        //{
        //    PM_UI_Manager.Resource.Destroy(slot.gameObject);
        //}

        for (int i = 0; i < 5; i++)
        {
            //GameObject slot = PM_UI_Manager.UI.MakeSubItem<UI_QuickSlot>(quickLayout.transform, "QuickSlot").gameObject;
            //UI_QuickSlot quickSlot = slot.GetOrAddComponent<UI_QuickSlot>();
        }

        // TEMP
        for (int i = 0; i < 5; i++)
        {
            CreateBuff();
            CreateDebuff();
            CreateStatusEffect();
        }

        profileImage = Util.FindChild<Image>(gameObject, "Profile", true);
    }

    public void CreateBuff()
    {
        GameObject buffLayout = Get<GameObject>((int)GameObjects.BuffLayout);

        // GameObject buff = PM_UI_Manager.UI.MakeSubItem<UI_Status>(buffLayout.transform, "Status").gameObject;
        // UI_Status status = buff.GetOrAddComponent<UI_Status>();
        // status.SetInfo();
    }

    public void CreateDebuff()
    {
        GameObject DebuffLayout = Get<GameObject>((int)GameObjects.DeBuffLayout);

        // GameObject debuff = PM_UI_Manager.UI.MakeSubItem<UI_Status>(DebuffLayout.transform, "Status").gameObject;
        // UI_Status status = debuff.GetOrAddComponent<UI_Status>();
    }

    public void CreateStatusEffect()
    {
        GameObject Status_effect_Layout = Get<GameObject>((int)GameObjects.Status_EffectLayout);

        //GameObject status_effect = PM_UI_Manager.UI.MakeSubItem<UI_Status>(Status_effect_Layout.transform, "Status").gameObject;
        //UI_Status status = status_effect.GetOrAddComponent<UI_Status>();
    }

    public void ChangeProfile()
    {
        // Need : PlayerInfo(동료 이미지, HP, MP, 가지고 있는 상태 리스트)
        // profileImage.sprite = changeProfile.sprite;
        // hp.SetPlayerStat(int _hp);
        // status.SetInfo();
        // PlayerStat 설정 (HP, MP Circle Bar) 잡는거 생각
    }
}