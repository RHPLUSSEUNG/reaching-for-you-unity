using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode
{
    public void Run()
    {
        //Conusme Item Add
        for(int i =0;i<= 14; i++)
        {
            Managers.Item.AddItem(6000 + i);
        }
        //Weapon Item Add
        for(int i =0;i<= 15;i++)
        {
            Managers.Item.AddItem(4000 + i);
        }
        //Armor Item Add
        for (int i = 0; i <= 3; i++)
        {
            Managers.Item.AddItem(5000 + i);
        }
    }
}
