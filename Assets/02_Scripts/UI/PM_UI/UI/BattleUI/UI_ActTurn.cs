using UnityEngine;
using UnityEngine.UI;

public class UI_ActTurn : UI_Scene
{
    enum turnUI
    {
        ActTurnPanel
    }

    // Temp
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite crabSprite;
    [SerializeField] Sprite lizardSprite;

    Color actedColor = Color.gray;
    GameObject turnPanel;
    // ���� �ʿ�
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(turnUI));

        turnPanel = GetObject((int)turnUI.ActTurnPanel);
        for(int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            Managers.Prefab.Instantiate("UI/SubItem/Turn", turnPanel.transform); // ActTurnPanel�� �θ�� �ؼ� ����
        }
        Managers.BattleUI.turnUI = gameObject.GetComponent<UI_ActTurn>();
    }

    public void UpdateTurnUI()
    {
        for(int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            Image turnImage = turnPanel.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
            Sprite charImage = Managers.Battle.ObjectList[i].GetComponent<Sprite>();            // ĳ���� ��������Ʈ�� �����´�

            Debug.Log($"Object : {Managers.Battle.ObjectList[i]}");

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
        }
    }

    public void MakeTurnUI(Sprite newObj)
    {
        Image newTurn = Managers.Prefab.Instantiate("UI/SubItem/Turn", gameObject.transform).GetComponent<Image>();
        newTurn.sprite = newObj;
        UpdateTurnUI();
    }

    public void DestroyTurnUI()
    {
        Destroy(transform.GetChild(0).gameObject);
        UpdateTurnUI();
    }

    public void ProceedTurnUI()
    {
        Image firstChar = turnPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        for (int i = 1; i < turnPanel.transform.childCount; i++)
        {
            Image updateImg = turnPanel.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>();
            Image moveImg = turnPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>();

            updateImg.sprite = moveImg.sprite;
        }

        Image lastChar = turnPanel.transform.GetChild(turnPanel.transform.childCount - 1).GetChild(0).GetComponent<Image>();
        lastChar.sprite = firstChar.sprite;
    }

    public void TurnUpdate()
    {
        // �ݺ��� childCount ���� �缳��
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
        // TODO : Color ����. ��� ĳ���Ͱ� �� ���� �ൿ���� �� �� �ʱ�ȭ
    }

    public void TempTurnUpdate()
    {
        Image lastImg = transform.GetChild(0).GetComponent<Image>();
        lastImg.color = actedColor;
    }
}
