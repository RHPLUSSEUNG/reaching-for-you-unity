using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn,
        BattleStart,
        NoticeText
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
}
