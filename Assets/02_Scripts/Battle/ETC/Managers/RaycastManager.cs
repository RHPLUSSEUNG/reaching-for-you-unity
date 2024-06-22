using UnityEngine;

public class RaycastManager
{
    GameObject go;
    RangeDetector detector;
    public bool detect_ready = false;

    public GameObject character;
    public CharacterState characterstate;
    public EntityStat characterstat;
    Equip_Item itemList;

    public void OnAwake()
    {
        detector = GameObject.Find("RangeDetector").GetComponent<RangeDetector>();
        //Debug.Log(detector);
    }

    public void OnUpdate()
    {
        if (Managers.Battle.currentCharacter != null && Managers.Battle.currentCharacter.CompareTag("Player"))
        {
            character = Managers.Battle.currentCharacter;
            characterstat = character.GetComponent<EntityStat>();
            characterstate = character.GetComponent<CharacterState>();
            itemList = character.GetComponent<Equip_Item>();
        }
        if (Managers.UI.uiState == UIState.Idle && detect_ready == true)
        {
            Debug.Log("detect cancle");
            detect_ready = false;
        }

        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                go = hit.collider.gameObject;
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
                    switch (Managers.UI.uiState)
                    {
                        case UIState.Idle:
                            break;
                        case UIState.SkillSet:
                            if (Managers.BattleUI.GetSkill() == null)
                            {
                                break;
                            }
                            else if (Managers.BattleUI.GetSkill().target_object == TargetObject.Me)
                            {
                                if (Managers.BattleUI.GetSkill().SetTarget(character))
                                {
                                    Managers.Battle.NextTurn();
                                }
                            }

                            else if (detect_ready == false)
                            {
                                detector.SetDetector(character, Managers.BattleUI.GetSkill().range + 1, Managers.BattleUI.GetSkill().target_object);
                                detect_ready = true;
                            }

                            if (detector.Detect(hit.collider.gameObject) != null)
                            {
                                if (Managers.BattleUI.GetSkill().SetTarget(hit.collider.gameObject.transform.parent.gameObject))
                                {
                                    detect_ready = false;
                                    Managers.UI.HideUI(Managers.BattleUI.cancleBtn.gameObject);
                                    Managers.Skill.UseElementSkill(Managers.BattleUI.GetSkill().element);
                                    Managers.Battle.NextTurn();
                                }
                            }
                            break;
                        case UIState.ItemSet:
                            //Range Set
                            //itemList.UseConsume(item, hit.collider.gameObject.transform.parent.gameObject);
                            break;
                        case UIState.Attack:
                            if (detect_ready == false)
                            {
                                detect_ready = true;
                                detector.SetDetector(character, (characterstat.AttackRange * 2) + 1, TargetObject.Enemy);
                            }
                            
                            if (detector.Detect(hit.collider.gameObject) != null)
                            {
                                detect_ready = false;
                                Managers.Active.Damage(hit.collider.gameObject.transform.parent.gameObject, characterstat.BaseDamage, characterstate.AttackType, characterstate.closeAttack);
                                if (!characterstate.after_move)
                                {
                                    Managers.Battle.NextTurn();
                                }

                            }
                            break;
                        case UIState.Move:
                            character.GetComponent<PlayerBattle>().Move(go);
                            break;

                    }

                }
                //Battle Setting
                else if (Managers.UI.uiState == UIState.PlayerSet && hit.transform.gameObject.CompareTag("Cube"))
                {
                    Managers.BattleUI.SetPosition(hit.collider.gameObject);
                }
            }
        }
    }
}
