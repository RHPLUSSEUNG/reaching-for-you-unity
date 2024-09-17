using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestGather : MonoBehaviour
{
    [SerializeField] bool isAddItem;
    [SerializeField] bool isRemoveItem;
    [SerializeField] int count = 0;
    private void Start()
    {
        isAddItem = false;
        isRemoveItem = false;
    }

    private void Update()
    {
        if(isAddItem)
        {
            Managers.Item.AddItem(0, count);
            isAddItem = false;
        }
        if (isRemoveItem)
        {
            Managers.Item.RemoveItem(0, count);
            isRemoveItem = false;
        }
    }
}
