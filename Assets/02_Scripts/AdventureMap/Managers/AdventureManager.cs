using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureManager : MonoBehaviour
{
    static AdventureManager _adventure;
    public static AdventureManager adventure { get { return _adventure; } }

    private static int stageCount = 1;
    public static int StageCount {
        get { return stageCount; }
        set {
            stageCount = value;
        }
    }

    public RandomPassage randomPassage;
    public RandomPlane randomPlane;
    public DynamicSpawner dynamicSpawner;

    void Awake()
    {
        _adventure = this;
    }

    void Start()
    {
        if((StageCount % 5) != 0)
        {
            randomPlane.SpawnBasic();
            randomPassage.Init();

            dynamicSpawner.RandomSpawn();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnSave();

                dynamicSpawner.DestroyObject();
            }
            else 
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnCure();

                dynamicSpawner.DestroyObject();
            }
        }
    }

    public void SpawnPlane(Collider collider)
    {
        if((StageCount % 5) != 0)
        {
            randomPlane.SpawnBasic();
            randomPassage.DeletePassage();
            randomPassage.AddPassage();

            dynamicSpawner.RandomSpawn();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnSave();

                dynamicSpawner.DestroyObject();
            }
            else 
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnCure();
                
                PlayerStat player = collider.gameObject.GetComponentInParent<PlayerStat>();
                player.Hp = player.MaxHp;

                dynamicSpawner.DestroyObject();
            }
        }
    }
}
