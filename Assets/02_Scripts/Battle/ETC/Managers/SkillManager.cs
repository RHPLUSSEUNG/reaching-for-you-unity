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

    public GameObject Instantiate(int id)
    {
        GameObject Skill = Managers.Prefab.Instantiate($"Skill/{Managers.Data.GetSkillData(id).SkillName}", usingSkill.transform);

        if(Skill.GetComponent<Active>() != null)
        {
            GameObject effect = Instantiate(Managers.Data.GetSkillData(id).SkillName);
            Skill.GetComponent<Active>().Effect = effect;
        }
        return Skill;
    }

    public GameObject Instantiate(string name)
    {
        GameObject Effect = Managers.Prefab.Instantiate($"Effect/{name}", usingEffect.transform);
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
