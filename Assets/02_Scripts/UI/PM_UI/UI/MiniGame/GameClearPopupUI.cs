using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameClearPopupUI : UI_Popup
{
    enum endUI
    {
        GameClearPanel,
        GameRankImage,
        StartButton,
        CloseButton
    }

    public MiniGameBase gameUI;

    RectTransform clearPanel;
    Image rankImg;

    Button startBtn;
    Button closeBtn;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(endUI));

        clearPanel = GetObject((int)endUI.GameClearPanel).GetComponent<RectTransform>();
        startBtn = GetObject((int)endUI.StartButton).GetComponent<Button>();
        closeBtn = GetObject((int)endUI.CloseButton).GetComponent<Button>();
        rankImg = GetObject((int)endUI.GameRankImage).GetComponent<Image>();

        BindEvent(startBtn.gameObject, StartButtonClick, Define.UIEvent.Click);
        BindEvent(closeBtn.gameObject, CloseButtonClick, Define.UIEvent.Click);

        StartCoroutine(AnimPopup(clearPanel));
    }

    public void SetRankImage(Sprite rank)
    {
        if(gameUI.GetStageLevel() >= 6)
        {
            startBtn.gameObject.SetActive(false);
        }
        rankImg.sprite = rank;      // TODO : 이미지 애니메이션?
    }
    public void StartButtonClick(PointerEventData data)
    {
        gameUI.NextLevel();
        StartCoroutine(ClosePopupUIAnim());
    }

    public void CloseButtonClick(PointerEventData data)
    {
        gameUI.GameEnd();
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(clearPanel));
        yield return new WaitForSeconds(animDuration);
        Managers.Prefab.Destroy(gameObject);
    }

    public Button GetStartBtn()
    {
        return startBtn;
    }
}
