using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager
{
    Active skill;
    GameObject target;
    GameObject usingSkill;
    GameObject usingEffect;
    public GameObject extent;

    public bool is_effect = false;

    public void Awake()
    {
        usingSkill = GameObject.Find("UsingSkill");
        Debug.Log(usingSkill);
        extent = GameObject.Find("SkillExtent");
        usingEffect = GameObject.Find("Effect");
    }

    public GameObject Instantiate(int id, bool monster = false)
    {
        GameObject go;
        if (monster)
        {
            go = Managers.Prefab.Instantiate($"Skill/Monster/{Managers.Data.GetMonsterSkillData(id).SkillName}", usingSkill.transform);
        }
        else
        {
            go = Managers.Prefab.Instantiate($"Skill/Player/{Managers.Data.GetPlayerSkillData(id).SkillName}", usingSkill.transform);
        }
        
        if(go.GetComponent<MonsterSkill>() != null)
        {
            GameObject effect = Instantiate(Managers.Data.GetMonsterSkillData(id).SkillName, true);
            go.GetComponent<MonsterSkill>().Effect = effect;
        }

        else if (go.GetComponent<Active>() != null)
        {
            GameObject effect = Instantiate(Managers.Data.GetPlayerSkillData(id).SkillName, false);
            go.GetComponent<Active>().Effect = effect;
        }
        return go;
    }

    public GameObject Instantiate(string name, bool monster)
    {
        GameObject Effect;
        if (monster)
        {
            Effect = Managers.Prefab.Instantiate($"Effect/Monster/{name}", usingEffect.transform);
        }
        else
        {
            Effect = Managers.Prefab.Instantiate($"Effect/Player/{name}", usingEffect.transform);
        }
        
        return Effect;
    }

    public IEnumerator StartEffect(GameObject effect, Vector3 pos)
    {
        effect.transform.position = pos;
        effect.GetComponent<ParticleSystem>().Play();
        is_effect = true;
        while(effect.GetComponent<ParticleSystem>().isPlaying)
        {
            Debug.Log(effect);
            yield return null;
        }
        is_effect = false;
        yield break;
    }
}
