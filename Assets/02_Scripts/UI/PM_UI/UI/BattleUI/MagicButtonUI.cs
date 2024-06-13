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
    [SerializeField]
    skillType skillType;

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
        GameObject disabled = GetObject((int)magicButtonUI.Disabled);
        Managers.UI.HideUI(disabled);
    }

    public void SetSkill(GameObject skill, int skill_ID)
    {
        SkillData skill_Data = Managers.Data.GetPlayerSkillData(skill_ID);

        if(skill_Data.SkillType == skillType.Passive)
        {
            Managers.UI.ShowUI(GetObject((int)magicButtonUI.Disabled));
            Text passiveText = GetObject((int)magicButtonUI.Disabled).transform.GetChild(0).GetComponent<Text>();
            passiveText.text = "P  a  s  s  i  v  e";
            possible = false;
        }

        Image magicIcon = GetObject((int)magicButtonUI.MagicIcon).GetComponent<Image>();
        Text magicName = GetObject((int)magicButtonUI.MagicName).GetComponent<Text>();
        magicName.text = skill_Data.SkillName;
        Image elementIcon = GetObject((int)magicButtonUI.ElementIcon).GetComponent<Image>();
        Text manaText = GetObject((int)magicButtonUI.ManaText).GetComponent<Text>();
        manaText.text = skill_Data.mp.ToString();
        Text attackText = GetObject((int)magicButtonUI.AttackText).GetComponent<Text>();

        saveSkill = skill;
        skillType = skill_Data.SkillType;
    }

    public bool CheckEnableMagic(int curMp)
    {
        if(skillType == skillType.Passive)
        {
            return false;
        }
        // 현재 캐릭터가 이 마나를 사용할 수 있는 마나 or 스태미나를 가지고 있는지 체크
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
            Managers.UI.uiState = UIState.SkillSet;
            Managers.BattleUI.skill = saveSkill;
            Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
            Managers.UI.HideUI(Managers.BattleUI.actPanel);
            Managers.UI.HideUI(Managers.BattleUI.moveBtn);
            Managers.UI.ShowUI(Managers.BattleUI.cancleBtn);

            cameraController.ChangeCameraMode(CameraMode.Static, true, true);
            Managers.BattleUI.cameraMode = CameraMode.Static;
        }
        else
        {
            
            Debug.Log("스킬 사용 불가");
        }
    }

    public void MagicButtonEnter(PointerEventData data)
    {
        Managers.UI.ShowUI(Managers.BattleUI.descriptPanel);
        DescriptUI descript = Managers.BattleUI.descriptPanel.GetComponent<DescriptUI>();
        descript.SetDescript(saveSkill, "마법에 대한 설명");
        descript.SetPosition();
    }

    public void MagicButtonExit(PointerEventData data)
    {
        Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
    }
}
