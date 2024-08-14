using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultCharacterUI : UI_Base
{
    enum CharacterUI
    {
        CharacterImage,
        CharacterName,
        CharacterState,
        Level,
        Exp,
        ExpText
    }
    Image charcterImage;
    TextMeshProUGUI level;
    GameObject exp;
    TextMeshProUGUI expText;

    float fillDuration = 2.0f;

    public override void Init()
    {
        Bind<GameObject>(typeof(CharacterUI));

        GameObject characterState = GetObject((int)CharacterUI.CharacterState);
        charcterImage = GetObject((int)CharacterUI.CharacterImage).GetComponent<Image>();
        level = GetObject((int)CharacterUI.Level).GetComponent<TextMeshProUGUI>();
        exp = GetObject((int)CharacterUI.Exp);
        expText = GetObject((int)CharacterUI.ExpText).GetComponent<TextMeshProUGUI>();

        BindEvent(characterState, OnEnterCharacterState, Define.UIEvent.Enter);
        BindEvent(characterState, OnExitCharacterState, Define.UIEvent.Exit);

        UpdateCharacterExp(50, 132, 100);
    }

    public void CharacterLevelUp()
    {
        int curLevel = 0/*���� ĳ�� ���� �ڸ�*/ + 1;
        level.text = curLevel.ToString();
    }

    public void UpdateCharacterExp(int curExp, int acpExp , int maxExp)
    {
        Slider expSlider = exp.GetComponent<Slider>();
        expSlider.value = curExp / (float)maxExp;
        // ����ġ�� ������� �� �ִ� ��� ��� == �ʱ� ����ġ �� �ʿ� => ĳ����Stat �ʿ� => ĳ���� �ʿ�
        StartCoroutine(FillSlider(curExp, acpExp, maxExp));
    }

    IEnumerator FillSlider(int curExp, int acpExp, int maxExp)
    {
        float elapsedTime = 0.0f;
        Slider expSlider = exp.GetComponent<Slider>();
        float startValue = expSlider.value;
        float targetValue = (curExp + acpExp) / (float)maxExp;
        int startExp = curExp;
        int targetExp = curExp + acpExp;

        while (elapsedTime < fillDuration)
        {
            expSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / fillDuration);
            float expValue = Mathf.Lerp(startExp, targetExp, elapsedTime / fillDuration);
            curExp = Mathf.RoundToInt(expValue);
            expText.text = $"{curExp} / {maxExp}";

            // ������ �� maxExp�� ���� ���
            if (expSlider.value >= 1.0f)
            {
                // ������ �� maxExp�� ������ ���� value ���� �̻������� ���� �߻� ����
                // CharacterLevelUp();
                expSlider.value = 0.0f;
                startValue = 0.0f;
                targetValue = targetValue - 1.0f;

                startExp = 0;
                targetExp = targetExp - maxExp;
                // maxExp ���� ���                
                elapsedTime = 0.0f;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        expSlider.value = targetValue;
    }

    public void OnEnterCharacterState(PointerEventData eventData)
    { 

    }

    public void OnExitCharacterState(PointerEventData eventData)
    {

    }
}
