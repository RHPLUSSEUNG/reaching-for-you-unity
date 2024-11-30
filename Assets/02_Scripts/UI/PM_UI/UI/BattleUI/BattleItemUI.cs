using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleItemUI : UI_Base
{
    enum battleItemUI
    {
        ItemIcon,
        ItemName,
        ItemCount
    }

    [SerializeField]
    GameObject saveItem;
    [SerializeField]
    int saveItemCount;
    int saveItem_ID;

    GameObject mainCamera;
    CameraController cameraController;

    public GameObject SaveItem { get { return saveItem; } }

    public override void Init()
    {
        Bind<GameObject>(typeof(battleItemUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
        BindEvent(gameObject, ItemButtonEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, ItemButtonExit, Define.UIEvent.Exit);

        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    public void SetItem(int item_ID, int itemCount)
    {
        ConsumeData consume_Data = Managers.Data.ParsingData(item_ID) as ConsumeData;

        Image itemIcon = GetObject((int)battleItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = consume_Data.itemSprite;
        Text itemName = GetObject((int)battleItemUI.ItemName).GetComponent<Text>();
        itemName.text = consume_Data.ItemName;
        Text count = GetObject((int)battleItemUI.ItemCount).GetComponent<Text>();
        count.text = itemCount.ToString();

        // saveItem = item;
        saveItemCount = itemCount;
        saveItem_ID = item_ID;
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIState.ItemSet;
        Managers.BattleUI.item = saveItem;
        Managers.BattleUI.item_ID = saveItem_ID;

        Managers.UI.HideUI(Managers.BattleUI.itemPanel);
        Managers.UI.HideUI(Managers.BattleUI.actUI.gameObject);

        cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.BattleUI.cameraMode = CameraMode.Follow;
    }

    public void ItemButtonEnter(PointerEventData data)
    {

    }

    public void ItemButtonExit(PointerEventData data)
    {
        
    }
}
