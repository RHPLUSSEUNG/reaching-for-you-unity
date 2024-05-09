using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_QuickSlot : UI_Base
{
    GameObject UI_Magic;

    public override void Init()
    {
        BindEvent(gameObject, ClickButton, Define.UIEvent.Click);
        UI_Magic = Util.FindChild(gameObject, "UI_Magic");
    }

    public void ClickButton(PointerEventData data)
    {
        Magic current_Magic = UI_Magic.GetComponent<Magic>();
        if (current_Magic == null)
        {
            Debug.Log("Magic Error");
            return;
        }

        current_Magic.UseMagic();
    }

    public void ChangeMagic()
    {
        Image magicIcon = UI_Magic.GetComponent<Image>();
        // sprite 변경 : magicIcon.sprite;
        // 마법에 들어가는 스크립트 변경
        Magic curMagic = UI_Magic.GetComponent<Magic>();
        TempMagic changeMagic = UI_Magic.AddComponent<TempMagic>();
        Destroy(curMagic);
    }
}
