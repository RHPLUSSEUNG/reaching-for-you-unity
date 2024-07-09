using UnityEngine;
using UnityEngine.UI;

public class UI_ActTurn : UI_Scene
{
    int objectCount;

    // Temp
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite crabSprite;
    [SerializeField] Sprite lizardSprite;

    Color actedColor = Color.gray;


    // 보완 필요

    public override void Init()
    {
        objectCount = Managers.Battle.ObjectList.Count;
        
        for(int i = 0; i < objectCount; i++)
        {
            Managers.Prefab.Instantiate("UI/SubItem/Turn", gameObject.transform);
        }
        Managers.BattleUI.turnUI = gameObject.GetComponent<UI_ActTurn>();
    }

    public void SetTurnUI()
    {
        for(int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            // GetChild 두 번 수정?
            Image turnImage = transform.GetChild((Managers.Battle.ObjectList.Count-1)-i).GetChild(0).gameObject.GetComponent<Image>();
            Sprite charImage = Managers.Battle.ObjectList[i].GetComponent<Sprite>();            // 캐릭터 스프라이트를 가져온다

            // Temp
            if (Managers.Battle.ObjectList[i].CompareTag("Player"))
            {
                turnImage.sprite = playerSprite;
            }
            else if (Managers.Battle.ObjectList[i].CompareTag("Monster"))
            {
                if (Managers.Battle.ObjectList[i].name == "Enemy_Crab(Clone)")
                {
                    turnImage.sprite = crabSprite;
                }
                else if (Managers.Battle.ObjectList[i].name == "Enemy_Lizard(Clone)")
                {
                    turnImage.sprite = lizardSprite;
                }
                
            }

            // turnImage.sprite = charImage;
        }
    }

    public void MakeTurnUI(Sprite newObj)
    {
        Image newTurn = Managers.Prefab.Instantiate("UI/SubItem/Turn", gameObject.transform).GetComponent<Image>();
        newTurn.sprite = newObj;
        SetTurnUI();
    }

    public void DestroyTurnUI()
    {
        Destroy(transform.GetChild(0).gameObject);
        SetTurnUI();
    }

    public void TurnUpdate()
    {
        // 반복문 childCount 범위 재설정
        Image curChar = transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>();
        Sprite curSprite = curChar.sprite;

        for (int i = transform.childCount - 2; i >= 0; i--)
        {
            Image turnImg = transform.GetChild(i + 1).GetChild(0).GetComponent<Image>();
            Image moveImg = transform.GetChild(i).GetChild(0).GetComponent<Image>();

            turnImg.sprite = moveImg.sprite;
        }
        Image lastImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        lastImg.sprite = curSprite;
        // TODO : Color 변경. 모든 캐릭터가 한 번씩 행동했을 시 색 초기화
    }

    public void TempTurnUpdate()
    {
        Image lastImg = transform.GetChild(0).GetComponent<Image>();
        lastImg.color = actedColor;
    }
}
