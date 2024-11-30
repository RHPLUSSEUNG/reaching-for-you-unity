using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeCountUI : UI_Popup
{
    enum consumeCntUI
    {
        CountText,
        DecreaseButton,
        ValueSlider,
        IncreaseButton,
        DecisionButton
    }

    TextMeshProUGUI countText;
    Slider valueSlider;
    int count;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(consumeCntUI));

        countText = GetObject((int)consumeCntUI.CountText).GetComponent<TextMeshProUGUI>();
        valueSlider = GetObject((int)consumeCntUI.ValueSlider).GetComponent<Slider>();
        valueSlider.value = 0;
        count = 0;

        GameObject decreaseBtn = GetObject((int)consumeCntUI.DecreaseButton);
        GameObject increaseBtn = GetObject((int)consumeCntUI.IncreaseButton);
        GameObject decisionBtn = GetObject((int)consumeCntUI.DecisionButton);
        BindEvent(decreaseBtn, ClickDecreaseButton, Define.UIEvent.Click);
        BindEvent(increaseBtn, ClickIncreaseButton, Define.UIEvent.Click);
        BindEvent(decisionBtn, ClickDecisionButton, Define.UIEvent.Click);
    }

    void ChangeCountText(bool changeState)
    {
        if(changeState)
        {
            count++;
            if(count >= Managers.Item.consumeInven[Managers.InvenUI.focusItemID])
            {
                count = Managers.Item.consumeInven[Managers.InvenUI.focusItemID];
            }
        }
        else
        {
            count--;
            if(count <= 0)
            {
                count = 0;
            }
        }
        string changeText = $"ÀåÂø ¼ö·® : {count}";
        countText.text = changeText;
    }

    void ChangeCountValue(bool changeState)
    {
        float ratio;

        ratio = (float)count / Managers.Item.consumeInven[Managers.InvenUI.focusItemID];
        valueSlider.value = ratio;
    }

    public void ClickDecreaseButton(PointerEventData data)
    {
        ChangeCountText(false);
        ChangeCountValue(false);
    }

    public void ClickIncreaseButton(PointerEventData data)
    {
        ChangeCountText(true);
        ChangeCountValue(true);
    }

    public void ClickDecisionButton(PointerEventData data)
    {
        Managers.InvenUI.UpdateInvenUI(count);
        Managers.Prefab.Destroy(gameObject);
    }
}
