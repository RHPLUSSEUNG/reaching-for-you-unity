using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTest : MonoBehaviour
{
    public GameObject Test;

    public Sprite[] test_sprite = new Sprite[3];
    void Awake ()
    {
        Debug.Log("아이템 생성");
        Managers.Item.AddItem(0);
        Managers.Item.AddItem(1);
        Managers.Item.AddItem(2);
        Managers.Item.AddItem(0);

        for(int i = 0; i < test_sprite.Length; i++)
        {
            Managers.InvenUI.test_sprite[i] = test_sprite[i];
        }
        Debug.Log("아이템 생성 완료");

        Managers.InvenUI.player = gameObject;
        Managers.InvenUI.SetInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(Managers.InvenUI.inven_state == false)
            {
                Managers.InvenUI.inven_state = true;
                Managers.UI.ShowUI(Managers.InvenUI.invenUI);
                Managers.InvenUI.invenUI.GetComponent<InvenUI>().MoveWeaponTab();
            }
            else
            {
                Managers.InvenUI.inven_state = false;
                Managers.UI.HideUI(Managers.InvenUI.invenUI);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Managers.Item.AddItem(0);
            Managers.InvenUI.UpdateItemUI(0);
        }
    }   
}
