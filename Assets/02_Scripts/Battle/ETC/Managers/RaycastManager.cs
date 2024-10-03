using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager
{
    public bool detect_ready = false;

    public GameObject character;
    public CharacterState characterstate;
    public EntityStat characterstat;

    Equip_Item itemList;
    Active activeSkill;
    Consume consume;

    List<GameObject> ranges;
    GameObject target;
    List<GameObject> targets;
    SkillExtent extent;
    public void OnStart()
    {
        extent= GameObject.Find("SkillExtent").GetComponent<SkillExtent>();
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

        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Managers.UI.uiState == UIState.PlayerSet)
            {
                target = RaycastTile(ray);
                Debug.Log(target.transform.position);
                Managers.BattleUI.SetPosition(target);
            }
            else if (Managers.Battle.battleState == BattleState.PlayerTurn)
            {
                switch (Managers.UI.uiState)
                {
                    case UIState.Move:
                        character.GetComponent<PlayerBattle>().Move(RaycastTile(ray));
                        target = null;
                        break;
                    case UIState.Idle:
                        target = null;
                        detect_ready = false;
                        break;
                    case UIState.SkillSet:
                        activeSkill = Managers.BattleUI.GetSkill();
                        if (!detect_ready)
                        {
                            PathFinder.RequestSkillRange(character.transform.position, activeSkill.range, RangeType.Normal, CallbackTargets);
                            detect_ready = true;
                        }
                        switch (activeSkill.target_object)
                        {
                            case TargetObject.Enemy:
                                RaycastEnemy(ray);
                                break;
                            case TargetObject.Friendly:
                                RaycastFriendly(ray);
                                break;
                            case TargetObject.Tile:
                                RaycastTile(ray);
                                break;
                            case TargetObject.Me:
                                target = Managers.Battle.currentCharacter;
                                break;
                        }
                        if (DetectTargets(target))
                        {
                            Managers.Manager.StartCoroutine(ActivatingSkillCoroutine());
                        }
                        break;
                    case UIState.ItemSet:
                        if (!detect_ready)
                        {
                            PathFinder.RequestSkillRange(character.transform.position, consume.range, RangeType.Normal, CallbackTargets);
                            detect_ready = true;
                        }
                        consume = (Consume)Managers.BattleUI.GetItem();
                        switch (consume.targetObject)
                        {
                            case TargetObject.Enemy:
                                target = RaycastEnemy(ray);
                                break;
                            case TargetObject.Friendly:
                                target = RaycastFriendly(ray);
                                break;
                            case TargetObject.Tile:
                                target = RaycastTile(ray);
                                break;
                            case TargetObject.Me:
                                target = Managers.Battle.currentCharacter;
                                break;
                        }
                        if (DetectTargets(target))
                        {
                            Managers.Manager.StartCoroutine(ActivatingConsumeCoroutine());
                        }
                        break;
                    case UIState.Attack:
                        if (detect_ready == false)
                        {
                            detect_ready = true;
                            PathFinder.RequestSkillRange(character.transform.position, characterstat.AttackRange, RangeType.Normal, CallbackTargets);
                        }
                        RaycastEnemy(ray);
                        if (DetectTargets(target))
                        {
                            detect_ready = false;
                            Managers.Active.Damage(target, characterstat.BaseDamage, characterstate.AttackType, characterstate.closeAttack);
                            if (!characterstate.after_move)
                            {
                                Managers.Battle.NextTurn();
                            }
                        }
                        break;
                }
            }
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
        }
    }

    public void CallbackTargets(List<GameObject> list)
    {
        targets = list;
        if(activeSkill.target_object == TargetObject.Friendly)
        {
            list.Add(Managers.Battle.currentCharacter);
        }
    }

    public bool DetectTargets(GameObject target)
    {
        //Debug.Log(target);
        targets = extent.SetArea(activeSkill.range, activeSkill.target_object, Managers.Battle.currentCharacter.transform.position, false);
        foreach (GameObject obj in targets)
        {
            if(target == obj)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator ActivatingSkillCoroutine()
    {
        Managers.Battle.cameraController.ChangeFollowTarget(target, true);
        Managers.Battle.cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.Battle.cameraController.ChangeOffSet(-3, 1.5f, -3, 20, 45);

        yield return new WaitForSeconds(1f);

        if (activeSkill.SetTarget(target))
        {
            detect_ready = false;
            Managers.UI.HideUI(Managers.BattleUI.cancleBtn.gameObject);
            Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().ClearSkillRange();
            Managers.Skill.UseElementSkill(Managers.BattleUI.GetSkill().element);
            target = null;
            Managers.Battle.NextTurn();
            yield break;
        }
        yield break;
    }

    private IEnumerator ActivatingConsumeCoroutine()
    {
        Managers.Battle.cameraController.ChangeFollowTarget(target, true);
        Managers.Battle.cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.Battle.cameraController.ChangeOffSet(-3, 1.5f, -3, 20,45);

        yield return new WaitForSeconds(1f);

        if (consume.Activate(target))
        {
            detect_ready = false;
            Managers.UI.HideUI(Managers.BattleUI.cancleBtn.gameObject);
            Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().ClearSkillRange();
            target = null;
            Managers.Battle.NextTurn();
            yield break;
        }
        yield break;
    }

    #region Raycast
    private GameObject RaycastTile(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile")))
        {
            target = hit.collider.gameObject;
            return target;
        }
        return null;
    }

    private GameObject RaycastEnemy(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Enemy")))
        {
            target = hit.collider.gameObject.transform.parent.gameObject;
            return target;
        }
        return null;
    }

    private GameObject RaycastFriendly(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Friendly")))
        {
            target = hit.collider.gameObject.transform.parent.gameObject;
            return target;
        }
        return null;
    }
    #endregion
}
