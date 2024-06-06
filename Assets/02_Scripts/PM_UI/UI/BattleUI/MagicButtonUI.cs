using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicButtonUI : UI_Base
{
    enum magicButtonUI
    {
        MagicIcon,
        MagicName,
        ElementIcon,
        ManaText,
        AttackText,
        Disabled
    }

    [SerializeField]
    GameObject saveSkill = null;

    GameObject mainCamera;
    CameraController cameraController;
    bool possible = true;

    public GameObject SaveSkill { get { return saveSkill; } }

    public override void Init()
    {
        Bind<GameObject>(typeof(magicButtonUI));
        BindEvent(gameObject, MagicButtonClick, Define.UIEvent.Click);
        BindEvent(gameObject, MagicButtonEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, MagicButtonExit, Define.UIEvent.Exit);

        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    public void SetSkill(GameObject skill)
    {
        Text magicName = GetObject((int)magicButtonUI.MagicName).GetComponent<Text>();
        // magicName.text;
        Image elementIcon = GetObject((int)magicButtonUI.ElementIcon).GetComponent<Image>();
        Text manaText = GetObject((int)magicButtonUI.ManaText).GetComponent<Text>();

        saveSkill = skill;
    }

    public bool CheckEnableMagic(int curMp)
    {
        // 현재 캐릭터가 이 마나를 사용할 수 있는 마나 or 스태미나를 가지고 있는지 체크
        int needMp = saveSkill.GetComponent<Active>().mp;
        if (curMp < needMp)
        {
            PM_UI_Manager.UI.ShowUI(GetObject((int)magicButtonUI.Disabled));
            possible = false;
            return possible;
        }
        PM_UI_Manager.UI.HideUI(GetObject((int)magicButtonUI.Disabled));
        possible = true;
        return possible;
    }

    public void MagicButtonClick(PointerEventData data)
    {
        if (possible)
        {
            PM_UI_Manager.UI.uiState = UIManager.UIState.SkillSet;
            PM_UI_Manager.BattleUI.skill = saveSkill;
            Debug.Log("Button Click");
            PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.descriptPanel);
            PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.actUI);
            PM_UI_Manager.UI.ShowUI(PM_UI_Manager.BattleUI.cancleBtn);

            cameraController.ChangeCameraMode(CameraMode.Static, true);
        }
        else
        {
            
            Debug.Log("스킬 사용 불가");
        }
    }

    public void MagicButtonEnter(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(PM_UI_Manager.BattleUI.descriptPanel);
        DescriptUI descript = PM_UI_Manager.BattleUI.descriptPanel.GetComponent<DescriptUI>();
        descript.SetDescript(saveSkill, "마법에 대한 설명");
        descript.SetPosition();
    }

    public void MagicButtonExit(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.descriptPanel);
    }
}
