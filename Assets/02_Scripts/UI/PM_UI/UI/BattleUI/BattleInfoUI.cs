using UnityEngine;
using UnityEngine.UI;

public class BattleInfoUI : UI_Popup
{
    enum battleInfoUI
    {
        CharacterIcon,
        HpText,
        MpText,
        // TODO : CharacterState �߰�
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleInfoUI));

        Managers.BattleUI.battleInfoUI = gameObject.GetComponent<BattleInfoUI>();
    }

    public void SetInfo(GameObject character)
    {
        // ���� character�� collider�� ��������
        EntityStat stat = character.GetComponent<EntityStat>();
        CharacterState state = character.GetComponent<CharacterState>();

        Image icon = GetObject((int)battleInfoUI.CharacterIcon).GetComponent<Image>();
        // TODO : Character Icon �ݿ�

        string hpText;
        string mpText;
        hpText = $"{stat.MaxHp}/{stat.Hp}";
        GetObject((int)battleInfoUI.HpText).GetComponent<Text>().text = hpText;
        mpText = $"{stat.MaxMp}/{stat.Mp}";
        GetObject((int)battleInfoUI.MpText).GetComponent<Text>().text = mpText;

        // TODO : State �ݿ�
    }
}
