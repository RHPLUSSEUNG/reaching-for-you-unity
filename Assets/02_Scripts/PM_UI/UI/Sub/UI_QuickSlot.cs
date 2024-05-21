using UnityEngine;
using UnityEngine.EventSystems;

public class UI_QuickSlot : UI_Base
{
    enum GameObjects
    {
        MagicIcon,

    }
    GameObject UI_Magic;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GameObject magicIcon = Get<GameObject>((int)GameObjects.MagicIcon).gameObject;
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
        // sprite 변경
        // 마법에 들어가는 스크립트 변경
    }
}
