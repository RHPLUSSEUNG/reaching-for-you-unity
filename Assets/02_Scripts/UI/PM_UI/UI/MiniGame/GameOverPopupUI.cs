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
        CloseButton
    }

    public BasicHealthUI healthUI;

    RectTransform gameOverPanel;
    Image rankImg;

    Button closeBtn;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(gameOverUI));

        gameOverPanel = GetObject((int)gameOverUI.GameOverPanel).GetComponent<RectTransform>();
        closeBtn = GetObject((int)gameOverUI.CloseButton).GetComponent<Button>();

        BindEvent(closeBtn.gameObject, CloseButtonClick, Define.UIEvent.Click);

        StartCoroutine(AnimPopup(gameOverPanel));
    }

    public void SetRankImage()
    {
        
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
