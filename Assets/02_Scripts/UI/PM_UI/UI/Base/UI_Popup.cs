using System.Collections;
using UnityEngine;

public class UI_Popup : UI_Base
{
    float animDuration = 0.1f;
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopUI()
    {
        Managers.UI.ClosePopupUI(this);
    }

    protected IEnumerator AnimPopup(RectTransform rect)
    {
        rect.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        float elapsedTime = 0f;
        while (elapsedTime < animDuration)
        {
            rect.localScale = Vector3.Lerp(new Vector3(0.1f, 0.1f, 0.1f), targetScale, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rect.localScale = targetScale;
    }
}
