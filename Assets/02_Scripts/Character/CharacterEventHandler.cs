using UnityEngine;

public class CharacterEventHandler : MonoBehaviour
{
    BattleGuideUI battleguide;
    void Start()
    {
        battleguide = GameObject.Find("BattleGuideUI(Clone)").GetComponent<BattleGuideUI>();
    }

    void HitEventEnd()
    {
        battleguide.EndHit();
    }
    void RollEventEnd()
    {
        battleguide.EndRoll();
    }
}
