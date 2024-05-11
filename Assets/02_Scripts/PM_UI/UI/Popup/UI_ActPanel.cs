using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ActPanel : UI_Popup
{
    enum ActButton
    {
        MagicButton,
        ItemButton,
        DefenseButton
    }

    [SerializeField] GameObject magicPanel;
    [SerializeField] GameObject itemPanel;

    PlayerSpec playerInfo;
    RectTransform pos;
    Vector2 mousePos;
 
    public override void Init()
    {
        Bind<Button>(typeof(ActButton));

        Button magicBtn = GetButton((int)ActButton.MagicButton);
        Button itemBtn = GetButton((int)ActButton.ItemButton);
        Button defenseBtn = GetButton((int)ActButton.DefenseButton);

        BindEvent(magicBtn.gameObject, MagicButtonClick, Define.UIEvent.Click);
        BindEvent(itemBtn.gameObject, ItemButtonClick, Define.UIEvent.Click);
        BindEvent(defenseBtn.gameObject, DefenseButtonClick, Define.UIEvent.Click);

        pos = GetComponent<RectTransform>();
    }

    // temp
    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        mousePos = Input.mousePosition;
    //        pos.position = mousePos;
    //        Debug.Log(mousePos);
    //    }
    //}

    public void ShowActPanel(PlayerSpec info)
    {
        gameObject.SetActive(true);
        playerInfo = info;
        mousePos = Input.mousePosition;
        pos.position = mousePos;
        Debug.Log(mousePos);
    }

    public void MagicButtonClick(PointerEventData data)
    {
        magicPanel.SetActive(true);
    }

    public void ItemButtonClick(PointerEventData data)
    {
        itemPanel.SetActive(true);
    }

    public void DefenseButtonClick(PointerEventData data)
    {

    }
}
