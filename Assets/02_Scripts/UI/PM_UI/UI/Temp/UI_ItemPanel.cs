using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemPanel : UI_Popup
{
    enum Buttons
    {
        ItemButton
    }

    [SerializeField] GameObject actPanel;
    [SerializeField] GameObject itemDescript;
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        Button itemBtn = GetButton((int)Buttons.ItemButton);
        BindEvent(itemBtn.gameObject, ItemButtonClick, Define.UIEvent.Click);
        BindEvent(itemBtn.gameObject, ItemButtonEnter, Define.UIEvent.Enter);
        BindEvent(itemBtn.gameObject, ItemButtonExit, Define.UIEvent.Exit);
    }

    public void SetItemList()
    {

    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("아이템 사용");
        actPanel.SetActive(false);
        gameObject.SetActive(false);
        itemDescript.SetActive(false);
    }

    public void ItemButtonEnter(PointerEventData data)
    {
        itemDescript.SetActive(true);
    }

    public void ItemButtonExit(PointerEventData data)
    {
        itemDescript.SetActive(false);
    }
}
