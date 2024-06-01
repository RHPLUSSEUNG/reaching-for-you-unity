using UnityEngine;

public class RaycastManager
{
    UI_Battle battleUI;
    GameObject go;
    public RangeDetector detector;
    public bool detect_ready = false;

    public GameObject character;
    public CharacterState characterstate;
    public EntityStat characterstat;

    public void TestInit()
    {
        GameObject UI = GameObject.Find("BattleUI");
        battleUI = UI.GetComponent<UI_Battle>();
        detector = GameObject.Find("RangeDetector").GetComponent<RangeDetector>();
        Debug.Log(detector);
    }

    public void OnUpdate()
    {
        if (Managers.Battle.currentCharacter == character)
        {
            character = Managers.Battle.currentCharacter;
            characterstat = character.GetComponent<EntityStat>();
            characterstate = character.GetComponent<CharacterState>();
        }
        
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
                        case ButtonState.Skill:
                            if (battleUI.GetSkill() == null)
                            {
                                break;
                            }
                            if(battleUI.GetSkill().target_object == TargetObject.Me)
                            {
                                if (battleUI.GetSkill().SetTarget(character))
                                {
                                    Managers.Battle.NextTurn();
                                }
                            }

                            if(detect_ready == false)
                            {
                                detector.SetDetector(character, battleUI.GetSkill().range + 1, battleUI.GetSkill().target_object);
                                detect_ready = true;
                            }

                            if (detector.Detect(hit.collider.gameObject))
                            {
                                if (battleUI.GetSkill().SetTarget(hit.collider.gameObject))
                                {
                                    detect_ready = false;
                                    Managers.Battle.NextTurn();
                                }
                            }
                            break;
                        case ButtonState.Item:
                            //TODO Item Use
                            break;
                        case ButtonState.Attack:
                            if (detect_ready == false)
                            {
                                detect_ready = true;
                                detector.SetDetector(character, (characterstat.AttackRange * 2) + 1, TargetObject.Enemy);
                            }

                            if (detector.Detect(hit.collider.gameObject))
                            {
                                detect_ready = false;
                                Managers.Active.Damage(hit.collider.gameObject, characterstat.BaseDamage, characterstate.AttackType, characterstate.closeAttack);
                                if (!characterstate.after_move)
                                {
                                    Managers.Battle.NextTurn();
                                }
                                
                            }
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
