using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempController : MonoBehaviour
{
    public Canvas _hud;
    public Canvas _pause;
    public Canvas _world;
    UI_Pause pause;
    UI_Hud hud;

    int _speed = 10;

    //temp
    public Buff tempBuff;
    public TestPlayerInfo[] playerList = new TestPlayerInfo[3];
    int tempTurn = 0;
    private void Start()
    {
        hud = _hud.GetComponent<UI_Hud>();
        pause = _pause.GetComponent<UI_Pause>();
        pause.gameObject.SetActive(false);
        _world.gameObject.SetActive(false);
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
            pause.ManagerPopUI();
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
