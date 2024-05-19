using System.Collections;
using UnityEngine;

public class RaycastManager
{
    UI_Battle battleUI;
    public void TestInit()
    {
        GameObject UI = GameObject.Find("BattleUI");
        battleUI = UI.GetComponent<UI_Battle>();
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("ray :" + hit.collider.gameObject);
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
                        battleUI.GetSkill().SetTarget(hit.collider.gameObject);
                        // Managers.PlayerButton.GetSkill().SetTarget(hit.collider.gameObject);
                    }
                    else if(Managers.PlayerButton.state == ButtonState.Idle && hit.collider.gameObject.name == "CombatMap(Clone)")
                    {
                        //player ¿Ãµø
                        Managers.PlayerButton.player.transform.position = hit.collider.transform.position + Vector3.up;
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
