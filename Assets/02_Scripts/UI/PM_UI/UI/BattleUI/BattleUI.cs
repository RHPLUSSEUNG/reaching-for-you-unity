using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn,
        BattleStart,
        NoticeText,
        MoveCameraButton
    }

    Text noticeText;
    SlideEffect slideEffect;
    GameObject mainCamera;
    CameraController cameraController;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleUI));

        Managers.UI.HideUI(GetObject((int)battleUI.ActTurn));

        noticeText = GetObject((int)battleUI.NoticeText).GetComponent<Text>();
        GameObject noticeUI = GetObject((int)battleUI.BattleStart);
        slideEffect = noticeUI.GetComponent<SlideEffect>();
        GameObject cameraBtn = GetObject((int)battleUI.MoveCameraButton);
        BindEvent(cameraBtn.gameObject, OnCameraButton, Define.UIEvent.Click);

        Managers.BattleUI.battleUI = gameObject.GetComponent<BattleUI>();

        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();

        StartCoroutine(StartSlideCoroutine("전투 시작!"));
    }

    IEnumerator StartSlideCoroutine(string text)
    {
        yield return StartCoroutine(StartSlide(text));
    }

    public IEnumerator StartSlide(string text)
    {
        noticeText.text = text;
        yield return StartCoroutine(slideEffect.SetSlideElement());
    }

    public void OnCameraButton(PointerEventData data)
    {
        Debug.Log("Camera Mode Move");
        if (Managers.BattleUI.cameraMode == CameraMode.Follow || Managers.BattleUI.cameraMode == CameraMode.UI)
        {
            cameraController.ChangeCameraMode(CameraMode.Move, false, true);
            Managers.BattleUI.cameraMode = CameraMode.Move;
            if(Managers.Battle.isPlayerTurn)
            {
                Managers.UI.HideUI(Managers.BattleUI.actUI.gameObject);
            }
        }
        else if(Managers.BattleUI.cameraMode == CameraMode.Move)
        {
            cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
            Managers.BattleUI.cameraMode = CameraMode.Follow;
            if(Managers.Battle.isPlayerTurn)
            {
                Managers.UI.ShowUI(Managers.BattleUI.actUI.gameObject);
                if (Managers.UI.uiState == UIState.Idle)// Idle 상태일 경우 follow offset
                {
                    cameraController.ChangeCameraMode(CameraMode.UI, false, true);
                    Managers.BattleUI.cameraMode = CameraMode.UI;
                }
                else
                {
                    cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
                    Managers.BattleUI.cameraMode = CameraMode.Follow;
                }
            }
        }
    }
}
