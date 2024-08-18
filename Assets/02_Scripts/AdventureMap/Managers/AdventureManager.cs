using System.Collections;
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
    public StaticSpawner staticSpawner;

    [SerializeField]
    private GameObject heal_effect;
    public bool is_effect = false;

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

            staticSpawner.RandomSpawn();
            dynamicSpawner.RandomSpawn();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnSave();

                staticSpawner.DestroyObject();
                dynamicSpawner.DestroyObject();
            }
            else 
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnCure();

                staticSpawner.DestroyObject();
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

            staticSpawner.RandomSpawn();
            dynamicSpawner.RandomSpawn();
        }
        else
        {
            if((StageCount % 2) == 0)
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnSave();

                staticSpawner.DestroyObject();
                dynamicSpawner.DestroyObject();
            }
            else 
            {
                randomPassage.DeletePassage();
                randomPlane.SpawnCure();
                
                PlayerStat player = collider.gameObject.GetComponentInParent<PlayerStat>();
                player.Hp = player.MaxHp;

                StartCoroutine(StartEffect(heal_effect));
                staticSpawner.DestroyObject();
                dynamicSpawner.DestroyObject();
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
