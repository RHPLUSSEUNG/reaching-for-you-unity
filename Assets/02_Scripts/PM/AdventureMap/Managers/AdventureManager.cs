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

    private int gimmickpercent;
    public static int GimmickCount;
    public static bool isGimmickRoom;

    int roll;
    int currentStep = 0;
    int roomCount = 0;

    void Awake()
    {
        _adventure = this;
    }

    void BasicSpawn()
    {
        randomPlane.SpawnBasic();
            
        staticSpawner.RandomSpawn();
        dynamicSpawner.RandomSpawn();

        gimmickpercent = Random.Range(1, 100);

        // 랜덤 인카운트(점점 5%로씩 확률 높임)
        roll = Random.Range(1, 100);
        currentStep += 5;
        roomCount++;
        Managers.BattleUI.hudUI.UpdateSearchCountTextUI(roomCount);
        Managers.BattleUI.hudUI.UpdateEncounterTextUI(currentStep);

        if(StageNumber > 0) // 2 스테이지부터 기믹 有
        {
            if(gimmickpercent <= 30) // 30% 확률로 기믹
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

    //Edit : Encounter Chance Function
    public void EncounterChance()
    {
        if (roomCount > 1 && roll <= currentStep)
        {
            currentStep = 0;
            roomCount = 0;

            float waitTime = Managers.BattleUI.productionUI.EncounterProduction();

            StartCoroutine(WaitAndChangeCombatScene(waitTime));
        }
    }

    //Edit : Encounter Chance Function
    IEnumerator WaitAndChangeCombatScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Player_Girl").GetComponent<PlayerController>().ChangeActive(true);
        SceneChanger.Instance.ChangeScene(SceneType.PM_COMBAT);
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
