using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpec : CharacterSpec
{
    public int uid;

    public Skill[] _equipSkills = new Skill[3];
    public Equipment[] _equipItems = new Equipment[5];

    public PlayerSpec(int uid)
    {
        this.uid = uid;
        for(int i = 0; i < 5; i++)
        {
            if(i < 3)
            {
                _equipSkills[i] = null;
            
            }
            _equipItems[i] = null;
        }
    }

    public void CalcElement()
    {
        int[] eles = new int[5];
        int max = 0;

        for(int i = 0;i < 5;i++)
        {
            eles[i]= 0;
        }
        foreach(Equipment equip in _equipItems)
        {
            eles[(int)equip.elementType]++;
            if (eles[(int)equip.elementType] > max)
            {
                max = eles[(int)equip.elementType];
                this.elementType = equip.elementType;
            }
        }
    }
}
