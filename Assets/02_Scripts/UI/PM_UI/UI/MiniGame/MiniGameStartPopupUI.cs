using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameStartPopupUI : UI_Popup
{
    enum miniGameStartUI
    {
        GamePanel,
        GameNameText,
        GameExImage,
        MiniGameDescription,
        StartButton,
        CloseButton
    }

    [SerializeField]
    List<MiniGameData> miniGameDataList = new List<MiniGameData>();
    [SerializeField]
    List<GameObject> miniGameList = new List<GameObject>();
    RectTransform gamePanel;

    Button startBtn;
    Button closeBtn;

    int miniGameID;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(miniGameStartUI));

        gamePanel = GetObject((int)miniGameStartUI.GamePanel).GetComponent<RectTransform>();
        startBtn = GetObject((int)miniGameStartUI.StartButton).GetComponent<Button>();
        closeBtn = GetObject((int)miniGameStartUI.CloseButton).GetComponent<Button>();

        BindEvent(startBtn.gameObject, StartButtonClick, Define.UIEvent.Click);
        BindEvent(closeBtn.gameObject, CloseButtonClick, Define.UIEvent.Click);


        StartCoroutine(AnimPopup(gamePanel));
    }

    public void SetGameInfo(int gameID)
    {
        Debug.Log("Set Game Data");
        TextMeshProUGUI nameText = GetObject((int)miniGameStartUI.GameNameText).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI gameDescript = GetObject((int)miniGameStartUI.MiniGameDescription).GetComponent<TextMeshProUGUI>();
        Image gameImg = GetObject((int)miniGameStartUI.GameExImage).GetComponent<Image>();

        MiniGameData gameData = miniGameDataList[gameID];

        miniGameID = gameData.GetGameID();
        nameText.text = gameData.GetGameName();
        gameDescript.text = gameData.GetGameDescript();
        gameImg.sprite = gameData.GetGameImage();
    }

    public void StartButtonClick(PointerEventData data)
    {
        Debug.Log($"Game ID : {miniGameID}");
        Debug.Log($"Game : {miniGameList[miniGameID]}");
        Instantiate(miniGameList[miniGameID]);
        
        // Managers.UI.CreatePopupUI<BasicHealthUI>("BasicHealthUI");
        StartCoroutine(ClosePopupUIAnim());
    }

    public void CloseButtonClick(PointerEventData data)
    {
        StartCoroutine(ClosePopupUIAnim());
    }

    IEnumerator ClosePopupUIAnim()
    {
        StartCoroutine(CloseAnimPopup(gamePanel));
        yield return new WaitForSeconds(animDuration);
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(true);
        Managers.Prefab.Destroy(gameObject);
    }
}
