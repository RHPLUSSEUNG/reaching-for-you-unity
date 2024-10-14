using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapInfoUI : BattleInfoUI
{
    enum mapInfoUI
    {
        Blocker,
        InfoPanel,
        TileName,
        TileIcon,
        TileDescription
    }

    RectTransform panelRect;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(mapInfoUI));

        Managers.BattleUI.battleInfoUI = gameObject.GetComponent<BattleInfoUI>();

        GameObject blocker = GetObject((int)mapInfoUI.Blocker);
        BindEvent(blocker, ClickBlocker, Define.UIEvent.Click);

        panelRect = GetObject((int)mapInfoUI.InfoPanel).GetComponent<RectTransform>();

        StartCoroutine(AnimPopup(panelRect));
    }

    public override void SetInfo(GameObject tile)
    {
        GimmickUI gimmickUI = tile.GetComponent<GimmickUI>();

        Image icon = GetObject((int)mapInfoUI.TileIcon).GetComponent<Image>();
        TextMeshProUGUI tileName = GetObject((int)mapInfoUI.TileName).GetComponent<TextMeshProUGUI>();
        tileName.text = gimmickUI.GimmickInfo.name;
        TextMeshProUGUI tile_description = GetObject((int)mapInfoUI.TileDescription).GetComponent<TextMeshProUGUI>();
        tile_description.text = gimmickUI.GimmickInfo.description; 
    }

    public override void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float uiWidth = panelRect.rect.width;
        float uiHeight = panelRect.rect.height;

        Vector3 uiPos = mousePos;
        if (mousePos.x + uiWidth > screenWidth)
        {
            uiPos.x = mousePos.x - (uiWidth / 2);
        }
        else
        {
            uiPos.x = mousePos.x + (uiWidth / 2);
        }
        if (mousePos.y - uiHeight < 0)
        {
            uiPos.y = mousePos.y + (uiHeight / 2);
        }
        else
        {
            uiPos.y = mousePos.y;
        }

        panelRect.position = uiPos;
    }

    public void ClickBlocker(PointerEventData data)
    {
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(panelRect));
        yield return new WaitForSeconds(animDuration);
        Managers.Prefab.Destroy(gameObject);
    }
}
