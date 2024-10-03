using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void AttackEvent()
    {
        gameObject.GetComponentInParent<EnemyAI_Base>().AttackEvent();
    }
    void SkillEventStart()
    {
        Managers.Skill.is_effect = true;
    }
    void SkillEventEnd()
    {
        Managers.Skill.is_effect = false;
    }
}
