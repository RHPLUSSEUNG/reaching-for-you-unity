using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn,
        BattleStart,
        NoticeText,
        MoveCameraButton
    }

    Text noticeText;
    SlideEffect slideEffect;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleUI));

        Managers.UI.HideUI(GetObject((int)battleUI.ActTurn));

        noticeText = GetObject((int)battleUI.NoticeText).GetComponent<Text>();
        GameObject noticeUI = GetObject((int)battleUI.BattleStart);
        slideEffect = noticeUI.GetComponent<SlideEffect>();
        GameObject cameraBtn = GetObject((int)battleUI.MoveCameraButton);
        BindEvent(cameraBtn.gameObject, OnCameraButton, Define.UIEvent.Click);

        Managers.BattleUI.battleUI = gameObject.GetComponent<BattleUI>();

        StartCoroutine(StartSlideCoroutine("전투 시작!"));
    }

    IEnumerator StartSlideCoroutine(string text)
    {
        yield return StartCoroutine(StartSlide(text));
    }

    public IEnumerator StartSlide(string text)
    {
        noticeText.text = text;
        yield return StartCoroutine(slideEffect.SetSlideElement());
    }

    public void OnCameraButton(PointerEventData data)
    {

    }
}
