using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenUI : UI_Popup
{
    enum invenUI
    {
        HatEquip,
        HeadEquip,
        UpBodyEquip,
        DownBodyEquip,
        ChestEquip,
        PantsEquip,
        WeaponEquip,
        InvenContent

    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(invenUI));

        PM_UI_Manager.InvenUI.hat = GetObject((int)invenUI.HatEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.head = GetObject((int)invenUI.HeadEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.upbody = GetObject((int)invenUI.UpBodyEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.downbody = GetObject((int)invenUI.DownBodyEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.chest = GetObject((int)invenUI.ChestEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.pants = GetObject((int)invenUI.PantsEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.weapon = GetObject((int)invenUI.WeaponEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.invenContent = GetObject((int)invenUI.InvenContent);

        //for(int i = 0; i < Managers.Item.equipmentInven.Count; i++)
        //{
        //    PM_UI_Manager.UI.MakeSubItem<InvenItemUI>(PM_UI_Manager.InvenUI.invenContent.transform, "InvenItemUI");
        //}
    }
}
