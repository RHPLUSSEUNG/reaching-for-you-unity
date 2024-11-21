using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameStartPopupUI : UI_Popup
{
    enum miniGameStartUI
    {
        GamePanel,
        GameNameText,
        GameExImage,
        MiniGameDescription,
        StartButton,
        CloseButton
    }

    RectTransform gamePanel;

    Button startBtn;
    Button closeBtn;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(miniGameStartUI));

        gamePanel = GetObject((int)miniGameStartUI.GamePanel).GetComponent<RectTransform>();
        startBtn = GetObject((int)miniGameStartUI.StartButton).GetComponent<Button>();
        closeBtn = GetObject((int)miniGameStartUI.CloseButton).GetComponent<Button>();

        BindEvent(startBtn.gameObject, StartButtonClick, Define.UIEvent.Click);
        BindEvent(closeBtn.gameObject, CloseButtonClick, Define.UIEvent.Click);

        StartCoroutine(AnimPopup(gamePanel));
    }

    public void StartButtonClick(PointerEventData data)
    {
        Managers.UI.CreatePopupUI<BasicHealthUI>("BasicHealthUI");
        StartCoroutine(ClosePopupUIAnim());
    }

    public void CloseButtonClick(PointerEventData data)
    {
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(gamePanel));
        yield return new WaitForSeconds(animDuration);
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(true);
        Managers.Prefab.Destroy(gameObject);
    }
}
