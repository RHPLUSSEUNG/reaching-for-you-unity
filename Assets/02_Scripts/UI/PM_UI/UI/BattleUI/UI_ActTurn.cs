using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_ActTurn : UI_Scene
{
    enum turnUI
    {
        ActTurnPanel,
        TurnOrderButton
    }

    // Temp
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite crabSprite;
    [SerializeField] Sprite lizardSprite;

    GameObject turnPanel;
    Button turnUIBtn;

    float moveDuration = 0.4f;
    float moveDistance = 300f;
    bool state = false;
    bool isMoving = false;

    RectTransform uiRect;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(turnUI));

        turnPanel = GetObject((int)turnUI.ActTurnPanel);
        turnUIBtn = GetObject((int)turnUI.TurnOrderButton).GetComponent<Button>();
        BindEvent(turnUIBtn.gameObject, ClickTurnOrderButton, Define.UIEvent.Click);

        uiRect = turnPanel.GetComponent<RectTransform>();

        Managers.BattleUI.turnUI = gameObject.GetComponent<UI_ActTurn>();
    }

    public void InstantiateTurnOrderUI()
    {
        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            Managers.Prefab.Instantiate("UI/SubItem/Turn", turnPanel.transform); // ActTurnPanel을 부모로 해서 생성
        }
        ShowTurnOrderUI();
    }

    public void UpdateTurnUI()
    {
        for(int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            Image turnImage = turnPanel.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
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

    public void ProceedTurnUI(int turnCnt)
    {
        int circleIdx = turnCnt;
        for (int i = 0; i < turnPanel.transform.childCount; i++)
        {
            Image turnImg = turnPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            Sprite charImage = Util.FindChild(Managers.Battle.ObjectList[circleIdx], "Character", true).GetComponent<SpriteRenderer>().sprite;

            turnImg.sprite = charImage;
            circleIdx++;
            if (circleIdx == Managers.Battle.ObjectList.Count)
            {
                circleIdx = 0;
            }
        }
        if(turnCnt > 0)
        {
            GameObject pastPanel = turnPanel.transform.GetChild(Managers.Battle.ObjectList.Count - turnCnt).GetChild(1).gameObject;
            pastPanel.SetActive(true);
        }
    }

    public void ShowTurnOrderUI()
    {
        if (isMoving) return;
        state = true;
        StartCoroutine(MoveUI(false));
    }

    public void HideTurnOrderUI()
    {
        if (isMoving) return;
        state = false;
        StartCoroutine(MoveUI(true));
    }

    IEnumerator MoveUI(bool flag)
    {
        isMoving = true;
        turnUIBtn.interactable = false;
        float distance = moveDistance;
        if(!flag)
        {
            distance = -moveDistance;
        }

        Vector3 startPos = uiRect.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(0, distance, 0);

        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            uiRect.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiRect.anchoredPosition = endPos;
        turnUIBtn.interactable = true;
        isMoving = false;
    }
    
    public void ResetPastPanel()
    {
        for(int i = 0; i < turnPanel.transform.childCount; i++)
        {
            GameObject pastPanel = turnPanel.transform.GetChild(i).GetChild(1).gameObject;
            pastPanel.SetActive(false);
        }
    }

    public bool GetState()
    {
        return state;
    }

    public void ClickTurnOrderButton(PointerEventData data)
    {
        if(state)
        {
            HideTurnOrderUI();
        }
        else
        {
            ShowTurnOrderUI();
        }
    }
}
