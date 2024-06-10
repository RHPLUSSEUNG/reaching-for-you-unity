using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharInfo : UI_Base
{
    [SerializeField] UI_Repair repairUI;
    enum CharInfoUI
    {
        CharacterIcon
    }

    public GameObject character;

    public override void Init()
    {
        Bind<GameObject>(typeof(CharInfoUI));

        BindEvent(gameObject, OnClickIcon, Define.UIEvent.Click);
    }

    public void OnClickIcon(PointerEventData data)
    {
        Debug.Log("Character Icon Click");
        Managers.InvenUI.player = character;
        repairUI.UpdatePlayerInfo();
    }
}
