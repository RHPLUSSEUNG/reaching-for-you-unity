using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : EntityStat
{
    [SerializeField]
    bool _canPassWall;

    private void Start()
    {
        //Managers.Save.SetData();
    }
    public bool CanPassWall { get { return _canPassWall; } set { _canPassWall = value; } }

}
