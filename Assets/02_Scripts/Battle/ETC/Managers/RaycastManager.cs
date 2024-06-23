using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager
{
    GameObject go;
    public bool detect_ready = false;

    public GameObject character;
    public CharacterState characterstate;
    public EntityStat characterstat;
    Equip_Item itemList;
  
    Active activeSkill;
    List<GameObject> targets;

    public void OnUpdate()
    {
        if (Managers.Battle.currentCharacter != null && Managers.Battle.currentCharacter.CompareTag("Player"))
        {
            character = Managers.Battle.currentCharacter;
            characterstat = character.GetComponent<EntityStat>();
            characterstate = character.GetComponent<CharacterState>();
            itemList = character.GetComponent<Equip_Item>();
        }

        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile")))
            {
                go = hit.collider.gameObject;
                if (Managers.UI.uiState == UIState.Move)
                {
                    character.GetComponent<PlayerBattle>().Move(go);
                }
            }
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
                            detect_ready = false;
                            break;
                        case UIState.SkillSet:
                            activeSkill = Managers.BattleUI.GetSkill();
                            if (activeSkill == null)
                            {
                                break;
                            }
                            else if (activeSkill.target_object == TargetObject.Me)
                            {
                                if (activeSkill.SetTarget(character))
                                {
                                    Managers.Battle.NextTurn();
                                }
                            }

                            else if (detect_ready == false)
                            {
                                PathFinder.RequestSkillRange(character.transform.position, activeSkill.range,RangeType.Normal, CallbackTargets);
                                detect_ready = true;
                            }

                            if (DetectTargets(go, activeSkill.target_object))
                            {
                                Managers.Manager.StartCoroutine(ActivatingSkillCoroutine());
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
                                PathFinder.RequestSkillRange(character.transform.position, characterstat.AttackRange, RangeType.Normal, CallbackTargets);
                            }
                            
                            if (DetectTargets(go, TargetObject.Enemy))
                            {
                                detect_ready = false;
                                Managers.Active.Damage(go.transform.parent.gameObject, characterstat.BaseDamage, characterstate.AttackType, characterstate.closeAttack);
                                if (!characterstate.after_move)
                                {
                                    Managers.Battle.NextTurn();
                                }
                            }
                            break;
                    }

                }
                //Battle Setting
                else if (Managers.UI.uiState == UIState.PlayerSet && hit.transform.gameObject.CompareTag("Cube"))
                {
                    Managers.BattleUI.SetPosition(go);
                }
            }
        }
    }

    public void CallbackTargets(List<GameObject> list)
    {
        targets = list;
    }

    public bool DetectTargets(GameObject target, TargetObject targetObject)
    {
        string targetLayer = targetObject.ToString();
        if (target.transform.parent.gameObject.layer != LayerMask.NameToLayer(targetLayer))
        {
            return false;
        }
        float posx = target.transform.position.x;
        float posz = target.transform.position.z;
        foreach (GameObject obj in targets)
        {
            if(obj.transform.position.x == posx && obj.transform.position.z == posz)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator ActivatingSkillCoroutine()
    {
        Managers.Battle.cameraController.ChangeFollowTarget(go, true);
        Managers.Battle.cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.Battle.cameraController.ChangeOffSet(0, 1, -3, 20);

        yield return new WaitForSeconds(1f);

        if (activeSkill.SetTarget(go.transform.parent.gameObject))
        {
            detect_ready = false;
            Managers.UI.HideUI(Managers.BattleUI.cancleBtn.gameObject);
            Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().ClearSkillRange();
            Managers.Skill.UseElementSkill(Managers.BattleUI.GetSkill().element);
            Managers.Battle.NextTurn();
            yield break;
        }
        yield break;
    }
}
