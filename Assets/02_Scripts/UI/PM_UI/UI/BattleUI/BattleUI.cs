using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn,
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
        slideEffect = GetComponent<SlideEffect>();

        Managers.BattleUI.battleUI = gameObject.GetComponent<BattleUI>();

        StartCoroutine(StartBattleSlide("전투 시작"));
    }

    public IEnumerator StartBattleSlide(string text)
    {
        yield return StartCoroutine(StartSlide(text));
        Managers.Battle.BattleStart();
    }

    public IEnumerator StartSlide(string text)
    {
        noticeText.text = text;
        slideEffect.SetSlideElement();
        yield return new WaitForSeconds(slideEffect.pauseDuration + slideEffect.duration);
    }
}
