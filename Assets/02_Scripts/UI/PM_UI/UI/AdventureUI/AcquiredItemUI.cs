using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcquiredItemUI : UI_Popup
{
    enum AcqItemUI
    {
        Blocker,
        AcqItemPanel,
        ItemLayout
    }

    List<int> acqItemList; // ItemManager로 옮기는것 고려

    RectTransform panelRect;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(AcqItemUI));

        panelRect = GetObject((int)AcqItemUI.AcqItemPanel).GetComponent<RectTransform>();
        GameObject blocker = GetObject((int)AcqItemUI.Blocker);
        BindEvent(blocker, ClickBlocker, Define.UIEvent.Click);

        StartCoroutine(AnimPopup(panelRect));
    }

    public void AcqItemUISetting()
    {
        for(int i = 0; i < acqItemList.Count; i++)
        {
            // Sub Item 생성
        }
    }

    public void AddAcqItemList(int itemID)
    {
        acqItemList.Add(itemID);
    }

    public void ClearAcqItemList()
    {
        acqItemList.Clear();
    }

    public void ClickBlocker(PointerEventData data)
    {
        StartCoroutine(CloseAcqPanelUI());
    }

    IEnumerator CloseAcqPanelUI()
    {
        StartCoroutine(CloseAnimPopup(panelRect));
        yield return new WaitForSeconds(animDuration);
        Managers.Prefab.Destroy(gameObject);
    }
}
