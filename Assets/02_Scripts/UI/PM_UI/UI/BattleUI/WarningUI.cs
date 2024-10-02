using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : UI_Popup
{
    TextMeshProUGUI warningText;
    float displayTime = 2.0f;
    public override void Init()
    {
        base.Init();

        warningText = gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public TextMeshProUGUI GetText()
    {
        return warningText;
    }

    public void SetText(string text)
    {
        warningText.text = text;
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
