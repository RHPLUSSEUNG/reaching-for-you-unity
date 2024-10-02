using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Add Consume Item");
            Managers.Item.AddItem(0);
            if (Managers.InvenUI.invenContent != null)
            {
                Managers.InvenUI.UpdateItemUI(0);
            }

            Managers.Item.AddItem(1);
            if (Managers.InvenUI.invenContent != null)
            {
                Managers.InvenUI.UpdateItemUI(1);
            }
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Add Equip Item");
            // Managers.Item.AddItem(15, 1, true);
            if (Managers.InvenUI.invenContent != null)
            {
                Managers.InvenUI.UpdateItemUI(15, true);
            }
        }
    }   
}
