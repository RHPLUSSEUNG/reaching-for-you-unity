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
        // ���� ĳ���Ͱ� �� ������ ����� �� �ִ� ���� or ���¹̳��� ������ �ִ��� üũ
        int needMp = saveSkill.GetComponent<Active>().mp;
        if (curMp < needMp)
        {
            Managers.UI.ShowUI(GetObject((int)magicButtonUI.Disabled));
            possible = false;
            return possible;
        }
        Managers.UI.HideUI(GetObject((int)magicButtonUI.Disabled));
        possible = true;
        return possible;
    }

    public void MagicButtonClick(PointerEventData data)
    {
        if (possible)
        {
            Managers.UI.uiState = UIManager.UIState.SkillSet;
            Managers.BattleUI.skill = saveSkill;
            Debug.Log("Button Click");
            Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
            Managers.UI.HideUI(Managers.BattleUI.actUI);
            Managers.UI.ShowUI(Managers.BattleUI.cancleBtn);

            cameraController.ChangeCameraMode(CameraMode.Static, true);
        }
        else
        {
            
            Debug.Log("��ų ��� �Ұ�");
        }
    }

    public void MagicButtonEnter(PointerEventData data)
    {
        Managers.UI.ShowUI(Managers.BattleUI.descriptPanel);
        DescriptUI descript = Managers.BattleUI.descriptPanel.GetComponent<DescriptUI>();
        descript.SetDescript(saveSkill, "������ ���� ����");
        descript.SetPosition();
    }

    public void MagicButtonExit(PointerEventData data)
    {
        Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
    }
}
