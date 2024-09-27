using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public Button turnUIBtn;

    float moveDuration = 0.4f;
    float moveDistance = 300f;
    float animDuration = 0.5f;
    float animDistance = 100f;

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
        turnUIBtn.gameObject.SetActive(false);
    }

    public void InstantiateTurnOrderUI()
    {
        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            GameObject newTurnUI = Managers.Prefab.Instantiate("UI/SubItem/Turn", turnPanel.transform);
            newTurnUI.AddComponent<CanvasGroup>();
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

    // TODO : Turn 생성 및 삭제
    public void MakeTurnUI(Sprite newObj, int turnCnt)
    {
        Image newTurn = Managers.Prefab.Instantiate("UI/SubItem/Turn", gameObject.transform).GetComponent<Image>();
        newTurn.sprite = newObj;
        UpdateTurnUI();
    }

    public void DestroyTurnUI(int turnCnt)
    {
        Destroy(transform.GetChild(0).gameObject);
        UpdateTurnUI();
    }

    public void ProceedTurnUI(int turnCnt)
    {
        if (turnCnt == 0)
        {
            return;
        }
        StartCoroutine(MoveTurnUIAnim());
        
    }

    IEnumerator MoveTurnUIAnim()
    {
        isMoving = true;
        turnUIBtn.interactable = false;
        CanvasGroup firstChildCanvasGroup = turnPanel.transform.GetChild(0).GetComponent<CanvasGroup>();

        Vector3 startPos = uiRect.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(-animDistance, 0, 0);

        float elapsedTime = 0;

        StartCoroutine(FadeOut(firstChildCanvasGroup, animDuration));

        while (elapsedTime < animDuration)
        {
            uiRect.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiRect.anchoredPosition = endPos;

        turnPanel.transform.GetChild(0).SetSiblingIndex(turnPanel.transform.childCount - 1);
        GameObject pastPanel = turnPanel.transform.GetChild(Managers.Battle.ObjectList.Count - 1).GetChild(1).gameObject;
        pastPanel.SetActive(true);

        uiRect.anchoredPosition = startPos;

        StartCoroutine(FadeIn(firstChildCanvasGroup, animDuration));
        turnUIBtn.interactable = true;
        isMoving = false;
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public void ShowTurnOrderUI()
    {
        if (isMoving) return;
        state = true;
        StartCoroutine(VisibleToggleUI(false));
    }

    public void HideTurnOrderUI()
    {
        if (isMoving) return;
        state = false;
        StartCoroutine(VisibleToggleUI(true));
    }

    IEnumerator VisibleToggleUI(bool flag)
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
        turnPanel.transform.GetChild(0).SetSiblingIndex(turnPanel.transform.childCount - 1);
        GameObject pastPanel = turnPanel.transform.GetChild(Managers.Battle.ObjectList.Count - 1).GetChild(1).gameObject;
        pastPanel.SetActive(true);
        for (int i = 0; i < turnPanel.transform.childCount; i++)
        {
            pastPanel = turnPanel.transform.GetChild(i).GetChild(1).gameObject;
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
