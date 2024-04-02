using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonConnector : MonoBehaviour
{
    public void NextTurnButton()
    {
        if(BattleManager.Instance.battleState != BattleState.PlayerTurn)
        {
            return;
        }
        BattleManager.Instance.NextTurn();
    }

    public void UseActiveButton()
    {
        Vector3 pos = Vector3.zero;
        Active active = this.GetComponent<Active>();
        //TODO input position that use skill
        if(Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
        }
        
        this.GetComponent<CharacterBattle>().UseSkill(active, pos);
    }

    public void UseTravelButton()
    {
        Travel travel = this.GetComponent<Travel>();

        travel.Activate();
    }

    public void ChangePartyButton(GameObject character1, GameObject character2 = null)
    {
        if(character2!=null)
        {
            BattleManager.Instance.RemoveParty(character2);
            BattleManager.Instance.AddParty(character1);
        }
        else
        {
            BattleManager.Instance.AddParty(character1);
        }
    }

    public void OpenEquipmentButton()
    {
        List<Equipment> inven = BattleManager.inven.equipmentInven;
        //TODO show in UI
    }

    public void EquipButton()
    {
        //TODO select character
        BattleManager.inven.EquipItem(this.GetComponent<Equipment>(), new PlayerSpec(1, 0));
    }
}
