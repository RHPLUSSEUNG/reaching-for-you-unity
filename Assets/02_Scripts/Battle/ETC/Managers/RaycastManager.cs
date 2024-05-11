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
            
            if (Physics.Raycast(ray, out hit))
            {
                /*
                if (hit.transform.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Character Selected : " + hit.collider.gameObject.name);
                    //TODO Charcter Info UI
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO Monster Info UI
                }
                */
                //Player Turn
                if (Managers.Battle.battleState == BattleState.PlayerTurn)
                {
                    if (Managers.PlayerButton.state == ButtonState.Skill)
                    {
                        Managers.PlayerButton.player.GetComponent<PlayerBattle>().UseSkill(Managers.PlayerButton.GetSkill(), hit.collider.gameObject);
                    }
                    else if(Managers.PlayerButton.state == ButtonState.Idle && hit.collider.gameObject.name == "CombatMap(Clone)")
                    {
                        //player ¿Ãµø
                        Managers.PlayerButton.player.transform.position = hit.collider.transform.position;
                    }
                    
                }
                //Battle Setting
                else if (Managers.PlayerButton.state == ButtonState.PlayerSet && hit.transform.gameObject.name == "CombatMap(Clone)")
                {
                    Managers.PlayerButton.SetPosition(hit.collider.gameObject);
                }
            }
        }
    }
}
