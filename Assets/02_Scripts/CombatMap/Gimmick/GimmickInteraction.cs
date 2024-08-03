using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GimmickName
{
    SlowGimmick,
    FallingGimmick,
}
public class GimmickInteraction : MonoBehaviour
{
    public Color warningColor { get; set;}
    public int TurnCnt { get; set; }
    public GimmickName EGimmickName;
}
