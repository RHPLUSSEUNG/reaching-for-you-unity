using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempController : MonoBehaviour
{
    // public Canvas worldUI;
    public Canvas _hud;
    public Canvas _pause;
    
    int _speed = 10;
    UI_Pause pause;
    UI_Hud hud;
    PlayerSpec curInfo;

    //temp
    public GameObject temp;
    PlayerSpec[] playerList = new PlayerSpec[3];
    Image tempImage;
    int tempTurn = 0;
    private void Start()
    {
        hud = _hud.GetComponent<UI_Hud>();
        pause = _pause.GetComponent<UI_Pause>();
        pause.gameObject.SetActive(false);

        // temp
        Buff temp1 = null;
        Buff temp2 = null;
        Buff temp3 = null;
        playerList[0].buffs.Add(temp1);
        playerList[1].buffs.Add(temp1);
        playerList[1].buffs.Add(temp2);
        playerList[2].buffs.Add(temp1);
        playerList[2].buffs.Add(temp2);
        playerList[2].buffs.Add(temp3);

        hud.ChangeProfile(playerList[0], tempImage);
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
            hud.ChangeProfile(playerList[tempTurn], tempImage);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.ManagerPopUI();
        }

        if (Input.GetKey(KeyCode.R))
        {
            // Äü½½·Ô ¸¶¹ý º¯°æ ½Ã : ¾î¶² Äü½½·Ô¿¡ ¾ðÁ¦ ¹Ù²ãÁö´ÂÁö ÇÊ¿ä
            UI_QuickSlot quick = temp.GetComponent<UI_QuickSlot>();
            quick.ChangeMagic();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    worldUI.gameObject.SetActive(true);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    worldUI.gameObject.SetActive(false);
    //}
}
