using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AdventureManager : MonoBehaviour
{
    static AdventureManager _adventure;
    public static AdventureManager adventure { get { return _adventure; } }

    [SerializeField] UnityEvent[] respawnedRoom;

    private static int stageCount = 1;
    public static int StageCount {
        get { return stageCount; }
        set {
            stageCount = value;
        }
    }

    public const int DESERT = 0, WATER = 1;

    public RandomPassage randomPassage;
    public RandomPlane randomPlane;
    public DynamicSpawner dynamicSpawner;
    public StaticSpawner staticSpawner;

    [SerializeField]
    private GameObject heal_effect;
    public bool is_effect = false;

    void Awake()
    {
        _adventure = this;
    }

    void BasicSpawn()
    {
        randomPlane.SpawnBasic();

        staticSpawner.RandomSpawn();
        dynamicSpawner.RandomSpawn();
    }

    void SpecificSpawn()
    {
        randomPassage.DeletePassage();
        

        staticSpawner.DestroyObject();
        dynamicSpawner.DestroyObject();   
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
