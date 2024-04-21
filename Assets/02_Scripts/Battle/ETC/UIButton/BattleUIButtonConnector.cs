//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class BattleUIButtonConnector : MonoBehaviour
//{
//    Button button;

//    public void NextTurnButton()
//    {
//        if(BattleManager.Instance.battleState != BattleState.PlayerTurn)
//        {
//            return;
//        }
//        BattleManager.Instance.NextTurn();
//    }

//    public void UseActiveSkillButton()
//    {
//        Vector3 pos = Vector3.zero;
//        Active active = this.GetComponent<Active>();
//        //TODO input position UI that use skill
//        if(Input.GetMouseButtonDown(0))
//        {
//            pos = Input.mousePosition;
//        }
        
//        this.GetComponent<CharacterBattle>().UseSkill(active, pos);
//    }

//    public void UseTravelButton()
//    {
//        Travel travel = this.GetComponent<Travel>();

//        travel.Activate();
//    }

//    public void ChangePartyButton(GameObject character1, GameObject character2 = null)
//    {
//        if(character2!=null)
//        {
//            Managers.Party.RemoveParty(character2);
//            Managers.Party.AddParty(character1);
//        }
//        else
//        {
//            Managers.Party.AddParty(character1);
//        }
//    }

//    public void OpenEquipmentButton()
//    {
//        List<Equipment> inven = Managers.Item.inven.equipmentInven;
//        //TODO show in UI
//    }

//    public void EquipButton()
//    {
//        //TODO select character
//        Managers.Item.inven.EquipItem(this.GetComponent<Equipment>(), new PlayerSpec(1, 0));
//    }

//    public void OpenConsumeButton()
//    {
//        Dictionary<Consume, int> inven = Inventory.inven.consumeInven;
//        //TODO show in UI
//    }

//    public void UseConsumeItemButton(Consume item, GameObject target)
//    {
//        if (item.Activate(target))
//        {
//            Inventory.Inven.ConsumeItem(item);
//        }
//    }

//    public void SelectCharacterButton()
//    {
//        return;
//    }
//}
