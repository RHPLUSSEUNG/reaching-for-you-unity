using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckConfirmUI : UI_Popup
{
    enum checkConfirmUI
    {
        WarningPanel,
        TitleText,
        CheckConfirmText,
        ConfirmButton,
        CancleButton
    }

    TextMeshProUGUI checkConfirmText;
    Button confirmButton;
    Button cancleButton;
    RectTransform panelRect;

    private Action confirmAction;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(checkConfirmUI));

        checkConfirmText = GetObject((int)checkConfirmUI.CheckConfirmText).GetComponent<TextMeshProUGUI>();
        confirmButton = GetObject((int)checkConfirmUI.ConfirmButton).GetComponent<Button>();
        cancleButton = GetObject((int)checkConfirmUI.CancleButton).GetComponent<Button>();

        BindEvent(confirmButton.gameObject, ConfirmButtonClick, Define.UIEvent.Click);
        BindEvent(cancleButton.gameObject, CancleButtonClick, Define.UIEvent.Click);

        panelRect = GetObject((int)checkConfirmUI.WarningPanel).GetComponent<RectTransform>();
        StartCoroutine(AnimPopup(panelRect));
    }

    public void SetConfirmAction(Action action)
    {
        confirmAction = action;
    }

    public void ChangeConfirmText(string text)
    {
        checkConfirmText.text = text;
    }

    public void ConfirmButtonClick(PointerEventData data)
    {
        if (confirmAction != null)
        {
            confirmAction.Invoke();
        }
        else
        {
            Debug.Log("Action is Null");
        }
        StartCoroutine(ClosePopupUIAnim());
    }

    public void CancleButtonClick(PointerEventData data)
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
