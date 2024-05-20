using UnityEngine;

public class RaycastManager
{
    UI_Battle battleUI;
    GameObject go;
    public void TestInit()
    {
        GameObject UI = GameObject.Find("BattleUI");
        battleUI = UI.GetComponent<UI_Battle>();
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                go = hit.collider.gameObject;
                Debug.Log("ray :" + go);
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
                    switch(Managers.PlayerButton.state)
                    {
                        case 0:
                            //TODO current Character Move
                            break;
                        case (ButtonState)1:
                            if(battleUI.GetSkill().target_object == TargetObject.Character && (go.CompareTag("Player") || go.CompareTag("Monster"))){
                                battleUI.GetSkill().SetTarget(go);
                            }
                            else if (battleUI.GetSkill().target_object == TargetObject.Tile && go.CompareTag("Cube"))
                            {
                                battleUI.GetSkill().SetTarget(go);
                            }
                            break;
                        case (ButtonState)2:
                            //TODO Item Use
                            break;
                    }
                    
                }
                //Battle Setting
                else if (Managers.PlayerButton.state == ButtonState.PlayerSet && hit.transform.gameObject.CompareTag("Cube"))
                {
                    Managers.PlayerButton.SetPosition(hit.collider.gameObject);
                }
            }
        }
    }
}
