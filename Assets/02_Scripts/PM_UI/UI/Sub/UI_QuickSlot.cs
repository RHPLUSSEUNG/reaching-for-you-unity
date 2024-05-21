using UnityEngine;
using UnityEngine.EventSystems;

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
}
