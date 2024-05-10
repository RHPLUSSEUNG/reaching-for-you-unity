using System.Collections;
using UnityEngine;

public class RaycastManager
{
    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.name == "CombatMap(Clone)")
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            //while battle

            if (Physics.Raycast(ray, out hit) && Managers.Battle.battleState == BattleState.PlayerTurn)
            {
                //using skill
                if (Managers.PlayerButton.state == ButtonState.Skill)
                {
                    Managers.PlayerButton.player.GetComponent<CharacterBattle>().UseSkill(Managers.PlayerButton.GetSkill(), hit.collider.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Character Selected : " + hit.collider.gameObject.name);
                    //TODO UI
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO UI
                }
                else if (Managers.PlayerButton.state == ButtonState.PlayerSet && hit.transform.gameObject.name == "CombatMap(Clone)")
                {
                    Managers.PlayerButton.SetPosition(hit.collider.gameObject);
                    //TODO Player Move
                }
            }
        }
    }
}
