using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class TestSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Button idle, hit, attack, skill1, skill2, skill3, crab, lizard, golem, worker, soldier, queen;
    public GameObject canvas;
    void Start()
    {
        idle = canvas.transform.GetChild(0).GetComponent<Button>();
        hit = canvas.transform.GetChild(1).GetComponent<Button>();
        attack = canvas.transform.GetChild(2).GetComponent<Button>();
        skill1 = canvas.transform.GetChild(3).GetComponent<Button>();
        skill2 = canvas.transform.GetChild(4).GetComponent<Button>();
        skill3 = canvas.transform.GetChild(5).GetComponent<Button>();
        crab = canvas.transform.GetChild(6).GetComponent<Button>();
        lizard = canvas.transform.GetChild(7).GetComponent<Button>();
        golem = canvas.transform.GetChild(8).GetComponent<Button>();
        worker = canvas.transform.GetChild(9).GetComponent<Button>();
        soldier = canvas.transform.GetChild(10).GetComponent<Button>();
        queen = canvas.transform.GetChild(11).GetComponent<Button>();

        crab.onClick.AddListener(() => this.SpawnCrab());
        lizard.onClick.AddListener(() => this.SpawnLizard());
        golem.onClick.AddListener(() => this.SpawnGolem());
        worker.onClick.AddListener(() => this.SpawnWorker());
        soldier.onClick.AddListener(() => this.SpawnSoldier());
        queen.onClick.AddListener(() => this.SpawnQueen());
    }



    public void SpawnCrab()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Crab");
        Instantiate(obj,Vector3.zero,Quaternion.identity);
        SetButton();
    }
    public void SpawnLizard()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Lizard");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        SetButton();
    }
    public void SpawnGolem()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Crab");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        SetButton();
    }
    public void SpawnWorker()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Crab");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        SetButton();

    }
    public void SpawnSoldier()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Crab");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        SetButton();

    }
    public void SpawnQueen()
    {
        Despawn();
        GameObject obj = Resources.Load<GameObject>("Prefabs/Monster/Enemy_Crab");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        SetButton();
    }
    public void Despawn()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Monster");
        if (obj!= null)
            Destroy(GameObject.FindGameObjectWithTag("Monster"));
    }
    public void SetButton()
    {
        idle.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Idle));
        hit.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Hit));
        attack.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Attack));
        skill1.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Skill1));
        skill2.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Skill2));
        skill3.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Monster").GetComponent<SpriteController>().SetAnimState(AnimState.Skill3));
    }
}
