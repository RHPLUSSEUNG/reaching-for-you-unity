using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager
{
    public GameObject actUI;
    public GameObject magicPanel;
    public GameObject itemPanel;
    public GameObject descriptPanel;
    public GameObject skill;
    public GameObject cancleBtn;
    public Item item;

    public Active GetSkill()
    {
        if (skill == null || skill.GetComponent<Active>() == null)
        {
            return null;
        }
        return skill.GetComponent<Active>();
    }

    public Item GetItem()
    {
        if (item == null)
        {
            return null;
        }
        return item;
    }
}
