using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    bool status = false;
    enum PauseButtons
    {
        GameSettingButton,
        ControlButton,
        DisplayButton,
        LanguageButton,
        ExitButton,
        CloseButton
    }

    public override void Init()
    {
        Bind<Button>(typeof(PauseButtons));

        Util.GetOrAddComponent<UI_Pause>(gameObject);
        Button gameSetting = GetButton((int)PauseButtons.GameSettingButton);
        Button control = GetButton((int)PauseButtons.ControlButton);
        Button display = GetButton((int)PauseButtons.DisplayButton);
        Button language = GetButton((int)PauseButtons.LanguageButton);
        Button exit = GetButton((int)PauseButtons.ExitButton);
        Button close = GetButton((int)PauseButtons.CloseButton);
        BindEvent(gameSetting.gameObject, OnGameSetting, Define.UIEvent.Click);
        BindEvent(control.gameObject, OnControlSetting, Define.UIEvent.Click);
        BindEvent(display.gameObject, OnDisplaySetting, Define.UIEvent.Click);
        BindEvent(language.gameObject, OnLanguageSetting, Define.UIEvent.Click);
        BindEvent(exit.gameObject, OnExitGame, Define.UIEvent.Click);
        BindEvent(close.gameObject, OnClosePause, Define.UIEvent.Click);
    }

    public void OnGameSetting(PointerEventData data)
    {
        Debug.Log("OnGameSetting");
    }

    public void OnControlSetting(PointerEventData data)
    {
        Debug.Log("OnControlSetting");
    }

    public void OnDisplaySetting(PointerEventData data)
    {
        Debug.Log("OnDisplaySetting");
    }

    public void OnLanguageSetting(PointerEventData data)
    {
        Debug.Log("OnLanguageSetting");
    }

    public void OnExitGame(PointerEventData data)
    {
        Debug.Log("OnExitGame");
    }

    public void OnClosePause(PointerEventData data)
    {
        ManagerPopUI();
    }

    public void ManagerPopUI()
    {
        status = !status;
        gameObject.SetActive(status);
        if (status == true)
        {
            // Time.timeScale = 0;

        }
        else
        {
            // base.ClosePopUI();
        }
    }
}
