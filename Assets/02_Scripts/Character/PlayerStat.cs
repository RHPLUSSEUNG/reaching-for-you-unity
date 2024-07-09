using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : EntityStat
{
    [SerializeField]
    bool _canPassWall;

    public bool CanPassWall { get { return _canPassWall; } set { _canPassWall = value; } }

}
