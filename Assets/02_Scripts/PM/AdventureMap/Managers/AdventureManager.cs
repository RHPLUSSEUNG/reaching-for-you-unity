using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Stage
{
    public string name;
    public int stageNum;
    public GameObject[] stageOfPrefabs;
}

public class AdventureManager : MonoBehaviour
{
    static AdventureManager _adventure;
    public static AdventureManager adventure { get { return _adventure; } }

    private const int DESERT = 0, WATER = 1;
    public static int StageNumber = DESERT;

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
    public StaticSpawner staticSpawner;

    [SerializeField]
    private GameObject heal_effect;
    public bool is_effect = false;

    private float gimmickpercent;
    public static int GimmickCount;
    public static bool isGimmickRoom;

    public int randomEncounter;
    public int roomCount = 0;
    public float encounterChance = 0.4f;

    void Awake()
    {
        _adventure = this;
    }

    void BasicSpawn()
    {
        randomPlane.SpawnBasic();
            
        staticSpawner.RandomSpawn();
        dynamicSpawner.RandomSpawn();

        gimmickpercent = Random.Range(0, 2);
        randomEncounter = Random.Range(0, 5);

        roomCount++;
        Managers.BattleUI.hudUI.UpdateSearchCountTextUI(roomCount);
        Managers.BattleUI.hudUI.UpdateEncounterTextUI(encounterChance);

        if(randomEncounter < 2 && roomCount > 1)
        {
            SceneChanger.Instance.ChangeScene(SceneType.PM_COMBAT);
        }

        if(StageNumber > 0) // 30% 확률로 기믹
        {
            if(gimmickpercent < 0.3f)
            {
                isGimmickRoom = true;
                GimmickCount = Random.Range(1, 5);
                dynamicSpawner.FillWater();
                dynamicSpawner.RandomGimmick();
            }     
            else   
            {
                isGimmickRoom = false;
                dynamicSpawner.ClearWater();
            }
        }
    }

    void SpecificSpawn()
    {
        randomPassage.DeletePassage();
        
        staticSpawner.DestroyObject();
        dynamicSpawner.DestroyObject();  
        dynamicSpawner.ClearWater();
    }

    void Start()
    {
        if((StageCount % 5) != 0)
        {
            BasicSpawn();
            randomPassage.Init();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                SpecificSpawn();
                randomPlane.SpawnSave();
            }
            else 
            {
                SpecificSpawn();
                randomPlane.SpawnCure();
            }
        }
    }

    public void SpawnPlane(Collider collider)
    {
        if((StageCount % 5) != 0)
        {
            BasicSpawn();
            randomPassage.DeletePassage();
            randomPassage.AddPassage();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                SpecificSpawn();
                randomPlane.SpawnSave();
            }
            else 
            {
                SpecificSpawn();
                randomPlane.SpawnCure();
                
                PlayerStat player = collider.gameObject.GetComponentInParent<PlayerStat>();
                player.Hp = player.MaxHp;

                StartCoroutine(StartEffect(heal_effect));
            }
        }
    }

    public IEnumerator StartEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.5f);
        
        effect.GetComponent<ParticleSystem>().Play();
        is_effect = true;
        while(effect.GetComponent<ParticleSystem>().isPlaying)
        {
            yield return null;
        }
        is_effect = false;
    }
}
