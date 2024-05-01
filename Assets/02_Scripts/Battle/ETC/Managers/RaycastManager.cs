using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastManager
{
    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //while battle
            if (Physics.Raycast(ray, out hit) && Managers.Battle.battleState == BattleState.PlayerTurn)
            {
                if (hit.transform.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Character Selected : " + hit.collider.gameObject.name);
                    //TODO UI
                    Managers.PlayerButton.UpdateSkillButton(hit.collider.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO UI
                }
                else if (Managers.PlayerButton.state == ButtonState.PlayerSet && hit.transform.gameObject.CompareTag("Field"))
                {
                    Managers.PlayerButton.SetPosition(hit.collider.gameObject);
                }
            }
            //set character position
            
            //using skill
            else if (Managers.PlayerButton.state == ButtonState.Skill)
            {
                Managers.PlayerButton.SetSkillPos(Input.mousePosition);
            }
        }
    }
}
