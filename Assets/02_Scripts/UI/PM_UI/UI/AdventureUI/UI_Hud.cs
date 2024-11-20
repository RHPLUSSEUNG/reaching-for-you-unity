using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    public enum HUDUI
    {
        ProfileImage,
        QuickLayout,
        BuffLayout,
        DeBuffLayout,
        Status_EffectLayout,
        HPBar,
        MPBar,
        AcquireItemUI,
        SearchCountText,
        EncounterText
    }

    [SerializeField]
    GameObject _effect;

    BarUI hpBar;
    BarUI mpBar;
    Image profileImage;
    GameObject buffLayout;
    GameObject debuffLayout;
    GameObject status_effectLayout;

    GameObject player;
    PlayerStat playerStat;
    CharacterState playerState;

    // test
    public Sprite buff_Image;
    public Sprite Debuff_Image;

    public int Max_Display_Status = 3;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(HUDUI));

        profileImage = GetObject((int)HUDUI.ProfileImage).GetComponent<Image>();
        hpBar = GetObject((int)HUDUI.HPBar).GetComponent<BarUI>();
        mpBar = GetObject((int)HUDUI.MPBar).GetComponent<BarUI>();
        buffLayout = GetObject((int)HUDUI.BuffLayout);
        debuffLayout = GetObject((int)HUDUI.DeBuffLayout);
        status_effectLayout = GetObject((int)HUDUI.Status_EffectLayout);
        GameObject acqItem = GetObject((int)HUDUI.AcquireItemUI);
        BindEvent(acqItem, ClickAcqItemUI, Define.UIEvent.Click);

        player = GameObject.Find("Player_Girl");            // 남캐일 때 문제 발생 + Player 교체 코드 필요

        playerStat = player.GetComponent<PlayerStat>();
        playerState = player.GetComponent<CharacterState>();

        Managers.BattleUI.hudUI = GetComponent<UI_Hud>();
    }

    private void Start()
    {
        ChangeProfile(playerStat, playerState);         // 함수 호출 순서에 따른 에러 발생 가능성
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeProfile(playerStat, playerState);
        }
    }

    public void CreateStatus(HUDUI type, Sprite icon = null, int value = 1)
    {
        HUDEffectUI effectUI;
        switch(type)
        {
            case HUDUI.BuffLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(buffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon, value);
                break;
            case HUDUI.DeBuffLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(debuffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon, value);
                break;
            case HUDUI.Status_EffectLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(status_effectLayout.transform, "Effect");
                effectUI.SetStatusImage(icon, value);
                break;
            default:
                Debug.Log("Incorrect Access");
                break;
        }
    }

    public void ChangeProfile(PlayerStat stat, CharacterState state)
    {
        hpBar.SetPlayerStat(stat.Hp, stat.MaxHp);
        mpBar.SetPlayerStat(stat.Mp, stat.MaxMp);
        ChangeEffectUI(state);
    }

    public void ChangeEffectUI(CharacterState state)
    {
        // TODO : Refactoring
        int displayCnt = 0;
        #region HUD Buff Setting
        HUDEffectUI effectUI;
        if (buffLayout.transform.childCount < state.buffs.Count)
        {
            for(int i = buffLayout.transform.childCount; i < state.buffs.Count; i++)
            {
                Managers.UI.MakeSubItem<HUDEffectUI>(buffLayout.transform, "Effect");
            }
        }
        for (int i = 0; i < state.buffs.Count; i++)
        {
            effectUI = buffLayout.transform.GetChild(i).gameObject.GetComponent<HUDEffectUI>();
            // Image changeIcon = state.buffs[i].GetComponent<Image>();       // Buff Icon
            int value = state.buffs[i].remainTurn;
            effectUI.SetStatusImage(buff_Image, value);
            
            if (displayCnt >= Max_Display_Status || value == 0)
            {
                Managers.UI.HideUI(effectUI.gameObject);
                continue;
            }
            displayCnt++;
        }
        if(buffLayout.transform.childCount > state.buffs.Count)
        {
            for (int i = state.buffs.Count; i < buffLayout.transform.childCount; i++)
            {
                Managers.Prefab.Destroy(buffLayout.transform.GetChild(i).gameObject);
            }
        }
        #endregion

        displayCnt = 0;
        #region HUD Debuff Setting
        if (debuffLayout.transform.childCount < state.debuffs.Count)
        {
            for(int i = debuffLayout.transform.childCount; i < state.debuffs.Count; i++)
            {
                Managers.UI.MakeSubItem<HUDEffectUI>(debuffLayout.transform, "Effect");
            }
        }
        for (int i = 0; i < state.debuffs.Count;i++)
        {
            effectUI = debuffLayout.transform.GetChild(i).gameObject.GetComponent<HUDEffectUI>();
            // Image changeIcon = state.debuffs[i].GetComponent<Image>();     // Debuff Icon
            int value = state.debuffs[i].remainTurn;
            effectUI.SetStatusImage(Debuff_Image, value);

            if (displayCnt >= Max_Display_Status || value == 0)
            {
                Managers.UI.HideUI(effectUI.gameObject);
                continue;
            }
            displayCnt++;
        }
        if (debuffLayout.transform.childCount > state.debuffs.Count)
        {
            for (int i = state.debuffs.Count; i < debuffLayout.transform.childCount; i++)
            {
                Managers.Prefab.Destroy(debuffLayout.transform.GetChild(i).gameObject);
            }
        }
        #endregion

        // TODO : 상태이상 HUD
    }

    public void UpdateSearchCountTextUI(int count)
    {
        TextMeshProUGUI countText = GetObject((int)HUDUI.SearchCountText).GetComponent<TextMeshProUGUI>();

        countText.text = $"탐색 횟수 : {count.ToString()}번";
    }

    public void UpdateEncounterTextUI(float chance)
    {
        TextMeshProUGUI chanceText = GetObject((int)HUDUI.EncounterText).GetComponent<TextMeshProUGUI>();

        chanceText.text = $"조우 확률 : {chance} %";
    }

    public void ClickAcqItemUI(PointerEventData data)
    {
        Managers.UI.CreatePopupUI<AcquiredItemUI>("AcqItemUI");
    }
}