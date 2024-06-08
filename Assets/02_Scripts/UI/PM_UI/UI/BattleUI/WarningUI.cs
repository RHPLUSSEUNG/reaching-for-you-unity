using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : UI_Popup
{
    Text waringText;
    float displayTime = 2.0f;
    public override void Init()
    {
        base.Init();

        waringText = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }

    public Text GetText()
    {
        return waringText;
    }

    public void SetText(string text)
    {
        waringText.text = text;
    }

    public void ShowWarningUI()
    {
        Managers.UI.ShowUI(gameObject);
        StartCoroutine(ShowWarningCoroutine());
    }

    private IEnumerator ShowWarningCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        Managers.UI.HideUI(gameObject);
    }
}
