using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempController : MonoBehaviour
{
    public Canvas _world;
    UI_Pause pause;
    UI_Hud hud;

    int _speed = 10;

    //temp
    public Buff tempBuff;
    public TestPlayerInfo[] playerList = new TestPlayerInfo[3];
    public Image testImage;
    int tempTurn = 0;
    private void Start()
    {
        hud = PM_UI_Manager.UI.CreateSceneUI<UI_Hud>("HudUI");
        pause = PM_UI_Manager.UI.CreatePopupUI<UI_Pause>("PauseUI");
        hud.tempImage = testImage;
        PM_UI_Manager.UI.HideUI(pause.gameObject);
        hud.ChangeProfile(playerList[0]);  // test
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tempTurn++;
            if (tempTurn == 3)
            {
                tempTurn = 0;
            }
            hud.ChangeProfile(playerList[tempTurn]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.state == false)
            {
                PM_UI_Manager.UI.ShowUI(pause.gameObject);
                pause.state = true;
                Time.timeScale = 0;
            }
            else
            {
                PM_UI_Manager.UI.HideUI(pause.gameObject);
                pause.state = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _world.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _world.gameObject.SetActive(false);
    }
}
