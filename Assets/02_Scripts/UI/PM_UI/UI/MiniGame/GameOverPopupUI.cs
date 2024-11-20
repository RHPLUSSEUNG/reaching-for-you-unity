using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverPopupUI : UI_Popup
{
    enum gameOverUI
    {
        GameOverPanel,
        GameRankImage,        
        CloseButton
    }

    public BasicHealthUI healthUI;

    RectTransform gameOverPanel;
    Image rankImg;

    Button startBtn;
    Button closeBtn;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(gameOverUI));

        gameOverPanel = GetObject((int)gameOverUI.GameOverPanel).GetComponent<RectTransform>();
        closeBtn = GetObject((int)gameOverUI.CloseButton).GetComponent<Button>();
        rankImg = GetObject((int)gameOverUI.GameRankImage).GetComponent<Image>();

        BindEvent(closeBtn.gameObject, CloseButtonClick, Define.UIEvent.Click);

        StartCoroutine(AnimPopup(gameOverPanel));
    }

    public void SetRankImage(Sprite rank)
    {
        rankImg.sprite = rank;      // TODO : �̹��� �ִϸ��̼�?
    }

    public void CloseButtonClick(PointerEventData data)
    {
        healthUI.GameEnd();
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(gameOverPanel));
        yield return new WaitForSeconds(animDuration);
        Managers.Prefab.Destroy(gameObject);
    }
}
