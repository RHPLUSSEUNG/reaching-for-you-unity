using UnityEngine;
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
        MPBar
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

        player = GameObject.Find("Player_Girl");            // 남캐일 때 문제 발생 + Player 교체 코드 필요

        playerStat = player.GetComponent<PlayerStat>();
        playerState = player.GetComponent<CharacterState>();

        #region Test Setting
        IncreaseAtk testBuff_1 = new IncreaseAtk();
        testBuff_1.SetBuff(5, player, 30);

        IncreaseDefence testBuff_2 = new IncreaseDefence();
        testBuff_2.SetBuff(3, player, 30);

        BarrierBuff testBuff_3 = new BarrierBuff();
        testBuff_3.SetBuff(1, player, 30);

        IncreaseSpeed testBuff_4 = new IncreaseSpeed();
        testBuff_4.SetBuff(2, player, 30);

        Freeze testDebuff_1 = new Freeze();
        testDebuff_1.SetDebuff(5, player, 30);

        Cold testDebuff_2 = new Cold();
        testDebuff_2.SetDebuff(3, player, 30);

        Poision testDebuff_3 = new Poision();
        testDebuff_3.SetDebuff(2, player);

        Slow testDebuff_4 = new Slow();
        testDebuff_4.SetDebuff(1, player);

        Burn testDebuff_5 = new Burn();
        testDebuff_5.SetDebuff(2, player, 30);
        #endregion

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
        // Test : KeyCode를 통해 인게임하면서 State 생성, 삭제와 같이 갱신 잘 되는지 확인
        if (Input.GetKeyDown(KeyCode.K))
        {
            GhostBuff ghost_Buff = new GhostBuff();
            ghost_Buff.SetBuff(2, player);
            ChangeProfile(playerStat, playerState);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            playerState.buffs.RemoveAt(0);
            ChangeProfile(playerStat, playerState);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            Freeze freeze = new Freeze();
            freeze.SetDebuff(2, player);
            ChangeProfile(playerStat, playerState);
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            playerState.debuffs.RemoveAt(0);
            ChangeProfile(playerStat, playerState);
        }
    }

    public void CreateStatus(HUDUI type, Sprite icon = null)
    {
        HUDEffectUI effectUI;
        switch(type)
        {
            case HUDUI.BuffLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(buffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
                break;
            case HUDUI.DeBuffLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(debuffLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
                break;
            case HUDUI.Status_EffectLayout:
                effectUI = Managers.UI.MakeSubItem<HUDEffectUI>(status_effectLayout.transform, "Effect");
                effectUI.SetStatusImage(icon);
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
        Sprite playerSprite = Util.FindChild(player, "Character", true).GetComponent<SpriteRenderer>().sprite;
        profileImage.sprite = playerSprite;
        ChangeEffectUI(state);
    }

    public void ChangeEffectUI(CharacterState state)
    {
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
            effectUI.SetStatusImage(buff_Image);
            if (i > effectUI.Max_Display_Child - 1)
            {
                Managers.UI.HideUI(effectUI.gameObject);
            }
        }
        if(buffLayout.transform.childCount > state.buffs.Count)
        {
            for (int i = state.buffs.Count; i < buffLayout.transform.childCount; i++)
            {
                Managers.Prefab.Destroy(buffLayout.transform.GetChild(i).gameObject);
            }
        }
        #endregion

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
            // Image changeIcon = state.debuffs[i].GetComponent<Image>();     // Test
            effectUI.SetStatusImage(Debuff_Image);
            if (i > effectUI.Max_Display_Child - 1)
            {
                Managers.UI.HideUI(effectUI.gameObject);
            }
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
}