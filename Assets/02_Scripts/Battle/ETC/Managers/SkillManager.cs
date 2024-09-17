using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager
{
    Active skill;
    GameObject target;
    GameObject usingSkill;
    GameObject usingEffect;
    
    public GameObject extent;

    public bool is_effect = false;

    public void OnAwake()
    {
        usingSkill = GameObject.Find("UsingSkill");
        extent = GameObject.Find("SkillExtent");
        usingEffect = GameObject.Find("Effect");
    }

    public void ReadyGameSkill()
    {
        foreach (GameObject character in Managers.Battle.ObjectList)
        {
            character.GetComponent<SkillList>().AddSkill(character.CompareTag("Monster"));
        }
    }

    public GameObject InstantiateSkill(int id, bool monster = false)
    {
        GameObject go;
        if (monster)
        {
            go = Managers.Prefab.Instantiate($"Skill/Monster/{Managers.Data.GetMonsterSkillData(id).SkillName}", usingSkill.transform);
        }
        else
        {
            //Debug.Log("player skill instantiate");
            go = Managers.Prefab.Instantiate($"Skill/Player/{Managers.Data.GetPlayerSkillData(id).SkillName}", usingSkill.transform);
        }
        
        if(go.GetComponent<MonsterSkill>() != null)
        {
            GameObject effect = InstantiateEffect(Managers.Data.GetMonsterSkillData(id).SkillName, true);
            go.GetComponent<MonsterSkill>().Effect = effect;
        }

        else if (go.GetComponent<Active>() != null)
        {
            GameObject effect = InstantiateEffect(Managers.Data.GetPlayerSkillData(id).SkillName, false);
            go.GetComponent<Active>().Effect = effect;
        }
        return go;
    }

    public GameObject InstantiateEffect(string name, bool monster)
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

    public GameObject InstantiateEffect(string name, GameObject target)
    {
        GameObject effect = Managers.Prefab.Instantiate($"Effect/BuffDebuff/{name}", target.transform);
        effect.transform.localPosition = new Vector3(0, 0, -0.5f);
        return effect;
    }

    public IEnumerator StartEffect(GameObject effect, Vector3 pos, bool other = false)
    {
        effect.transform.position = pos;
        if (other)
        {
            effect.transform.rotation = Quaternion.LookRotation(Managers.Battle.currentCharacter.transform.position - pos);
            //Debug.Log(effect.transform.rotation);
        }
        effect.GetComponent<ParticleSystem>().Play();
        is_effect = true;

        yield return new WaitForSeconds(0.5f);
        if (effect.GetComponent<ParticleSystem>().isPlaying)
        {
            effect.GetComponent<ParticleSystem>().Stop();
        }
        
        is_effect = false;
    }

    public IEnumerator StartBuffEffect(GameObject effect, Vector3 pos)
    {
        effect.transform.position = pos;
        effect.GetComponent<ParticleSystem>().Play();
        is_effect = true;
        yield return new WaitForSeconds(1.5f);
        effect.GetComponent<ParticleSystem>().Stop();
        is_effect = false;
    }

    public void UseElementSkill(ElementType type)
    {
        CharacterState state = Managers.Battle.currentCharacter.GetComponent<CharacterState>();
        switch (type)
        {
            case ElementType.Electric:
                if (state.capacity == 1 || state.capacity == 3)
                {
                    state.capacityStack++;
                }
                break;
        }
    }

    
}
