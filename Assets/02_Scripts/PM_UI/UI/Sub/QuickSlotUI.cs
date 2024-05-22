using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotUI : UI_Base
{
    enum QuickUI
    {
        MagicIcon
    }

    public override void Init()
    {
        Bind<Image>(typeof(QuickUI));
        BindEvent(gameObject, ClickButton, Define.UIEvent.Click);
    }

    public void SetImage(Image icon)
    {
        Image quickIcon = GetImage((int)QuickUI.MagicIcon);
        quickIcon.sprite = icon.sprite;
    }

    public void ClickButton(PointerEventData data)
    {
        Debug.Log("Magic »ç¿ë");

        // Test
        //Magic current_Magic = GetComponent<Magic>();
        //if (current_Magic == null)
        //{
        //    Debug.Log("Magic Error");
        //    return;
        //}

        //current_Magic.UseMagic();
    }
}
