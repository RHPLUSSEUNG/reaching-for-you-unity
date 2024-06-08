using System.Collections.Generic;
using UnityEngine;

public class TestPlayerInfo : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    public string level;
    public Sprite iconImage;
    public Sprite element;
    public string charname;

    public List<TestBuff> buffList = new();
    public List<TestDebuff> debuffList = new();
    public List<TempStatus_Effect> seList = new();
}
