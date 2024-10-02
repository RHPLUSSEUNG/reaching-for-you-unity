using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ActTurn : UI_Scene
{
    enum turnUI
    {
        TurnUIPanel,
        ActTurnPanel,
        TurnOrderButton
    }

    GameObject uiPanel;
    GameObject turnPanel;
    public Button turnUIBtn;

    int turnCnt;
    float moveDuration = 0.4f;
    float moveDistance = 300f;
    float animDuration = 0.5f;
    float animDistance = 100f;

    bool state = false;
    bool isMoving = false;

    RectTransform uiRect;
    RectTransform turnRect;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(turnUI));

        uiPanel = GetObject((int)turnUI.TurnUIPanel);
        turnPanel = GetObject((int)turnUI.ActTurnPanel);
        turnUIBtn = GetObject((int)turnUI.TurnOrderButton).GetComponent<Button>();
        BindEvent(turnUIBtn.gameObject, ClickTurnOrderButton, Define.UIEvent.Click);

        uiRect = uiPanel.GetComponent<RectTransform>();
        turnRect = turnPanel.GetComponent<RectTransform>();

        Managers.BattleUI.turnUI = gameObject.GetComponent<UI_ActTurn>();
        turnUIBtn.gameObject.SetActive(false);
    }

    public void InstantiateTurnOrderUI()
    {
        GameObject newTurnUI = Managers.Prefab.Instantiate("UI/SubItem/Turn", turnPanel.transform);
        newTurnUI.AddComponent<CanvasGroup>();
    }

    public void InstantiateAllTurnOrderUI()
    {
        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            InstantiateTurnOrderUI();
        }
        ShowTurnOrderUI();
    }

    public void UpdateTurnUI(int turnCnt)
    {
        if(turnCnt == -1)
        {
            turnCnt = 0;
        }
        int circleIdx = turnCnt;
        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            Image turnImage = turnPanel.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
            Sprite charImage = Util.FindChild(Managers.Battle.ObjectList[circleIdx], "Character", true).GetComponent<SpriteRenderer>().sprite;

            turnImage.sprite = charImage;
            circleIdx++;
            if (circleIdx == Managers.Battle.ObjectList.Count)
            {
                circleIdx = 0;
            }
        }
    }

    // TODO : Turn 생성 및 삭제
    public void MakeTurnUI()
    {
        // 소환 스킬을 했을 때, 소환수가 이번 페이즈에 행동하는가? 아니면 다음 행동부터 행동하는지 구별할 게 필요
        InstantiateTurnOrderUI();
        UpdateTurnUI(turnCnt);

        // 소환수가 이번 페이즈에 행동을 할 때
        // 소환수 스킬 더 추가 시 모듈화 또는 수정 필요
        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            GameObject pastPanel = turnPanel.transform.GetChild(i).GetChild(1).gameObject;
            pastPanel.SetActive(false);
        }
        for (int i = 0; i < turnCnt; i++)
        {
            GameObject pastPanel = turnPanel.transform.GetChild(Managers.Battle.ObjectList.Count - 1 - i).GetChild(1).gameObject;
            pastPanel.SetActive(true);
        }
    }

    public void DestroyTurnUI()
    {
        Destroy(transform.GetChild(0).gameObject);
        UpdateTurnUI(turnCnt);
    }

    public void ProceedTurnUI(int turnCnt)
    {
        this.turnCnt = turnCnt;
        if (turnCnt == 0)
        {
            return;
        }
        StartCoroutine(MoveTurnUIAnim());
        
    }

    IEnumerator MoveTurnUIAnim()
    {
        Debug.Log("Check B");
        isMoving = true;
        turnUIBtn.interactable = false;
        CanvasGroup firstChildCanvasGroup = turnPanel.transform.GetChild(0).GetComponent<CanvasGroup>();

        Vector3 startPos = turnRect.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(-animDistance, 0, 0);

        float elapsedTime = 0;

        StartCoroutine(FadeOut(firstChildCanvasGroup, animDuration));

        while (elapsedTime < animDuration)
        {
            turnRect.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        turnRect.anchoredPosition = endPos;

        turnPanel.transform.GetChild(0).SetSiblingIndex(turnPanel.transform.childCount - 1);
        GameObject pastPanel = turnPanel.transform.GetChild(Managers.Battle.ObjectList.Count - 1).GetChild(1).gameObject;
        pastPanel.SetActive(true);

        turnRect.anchoredPosition = startPos;

        StartCoroutine(FadeIn(firstChildCanvasGroup, animDuration));
        turnUIBtn.interactable = true;
        isMoving = false;
        Debug.Log("Check B End");
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
        Debug.Log("Check A");
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
        Debug.Log("Check A End");
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
