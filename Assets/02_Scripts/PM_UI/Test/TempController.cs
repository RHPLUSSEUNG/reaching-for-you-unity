using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    // public Canvas worldUI;
    public Canvas _pause;
    int _speed = 10;
    UI_Pause pause;
    private void Start()
    {
        // temp
        // Managers.UI.CreateSceneUI<UI_Hud>();
        // pause = Managers.UI.CreatePopupUI<UI_Pause>();
        pause = _pause.GetComponent<UI_Pause>();
        pause.gameObject.SetActive(false);
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

        if (Input.GetKey(KeyCode.Tab))
        {
            // ChangeProfile
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.ManagerPopUI();
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
