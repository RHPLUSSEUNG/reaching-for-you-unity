using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUI : UI_Popup
{
    float displayTime = 2.0f;
    public override void Init()
    {
        base.Init();
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
