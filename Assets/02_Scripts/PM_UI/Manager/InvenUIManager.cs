using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenUIManager
{
    public Image hat;
    public Image chest;
    public Image pants;
    public Image head;
    public Image upbody;
    public Image downbody;
    public Image weapon;
    public GameObject invenContent;
    public GameObject player;
    public EquipUI equipUI = null;

    public void UpdateEquipUI(Image cur, Image change)
    {
        cur.sprite = change.sprite;
    }

    public void CreateEquipUI()
    {
        equipUI = PM_UI_Manager.UI.CreatePopupUI<EquipUI>("EquipUI");
    }
}
