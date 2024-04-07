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
        QuickLayout
    }

    Image Buff;
    Image Debuff;
    Image Status_Effect;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Define.Images));

        Buff = GetImage((int)Define.Images.Buff);
        Debuff = GetImage((int)Define.Images.Debuff);
        Status_Effect = GetImage((int)Define.Images.Status_Effect);
        Image status = Buff;
        BindEvent(status.gameObject, OnStatusEnter, Define.UIEvent.Enter);
        BindEvent(status.gameObject, OnStatusExit, Define.UIEvent.Exit);
        status = Debuff;
        BindEvent(status.gameObject, OnStatusEnter, Define.UIEvent.Enter);
        BindEvent(status.gameObject, OnStatusExit, Define.UIEvent.Exit);
        status = Status_Effect;
        BindEvent(status.gameObject, OnStatusEnter, Define.UIEvent.Enter);
        BindEvent(status.gameObject, OnStatusExit, Define.UIEvent.Exit);
        GameObject quickLayout = Get<GameObject>((int)GameObjects.QuickLayout);
        foreach (Transform slot in quickLayout.transform)
        {
            Managers.Resource.Destroy(slot.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject slot = Managers.UI.MakeSubItem<UI_QuickSlot>(quickLayout.transform, "QuickSlot").gameObject;
            UI_QuickSlot quickSlot = slot.GetOrAddComponent<UI_QuickSlot>();
        }


    }

    public void OnStatusEnter(PointerEventData data)
    {
        foreach (Transform child in Buff.gameObject.transform)
        {
            if (child == null)
            {
                break;
            }
            child.gameObject.SetActive(true);
        }
    }

    public void OnStatusExit(PointerEventData data)
    {
        foreach (Transform child in Buff.gameObject.transform)
        {
            if (child == null)
            {
                break;
            }
            child.gameObject.SetActive(false);
        }
    }
}