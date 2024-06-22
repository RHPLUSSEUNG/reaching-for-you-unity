using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowInteration : GimmickInteraction
{
    GameObject character;
    CharacterState characterstate;
    EntityStat characterstat;

    Text warningText;
    Slow slow;

    // int gimmickTurnCnt = 2; // 기믹 작동에 필요한 턴 개수
    int remainTurnCnt = -1;
    int currentTurnCnt = -1; // 현재 총 턴 횟수

    private void Awake() {
        warningColor = new Color32(60, 100, 180, 255);
        TurnCnt = 2;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (Managers.Battle.currentCharacter != null && Managers.Battle.currentCharacter != character)
        {
            character = Managers.raycast.character;
            characterstat = Managers.raycast.characterstat;
            characterstate = Managers.raycast.characterstate;
        }

        if (Managers.Battle.battleState == BattleState.PlayerTurn)
        {
            slow = new Slow();
            slow.SetDebuff(TurnCnt, character, 10, true);
            Debug.Log("Slow");

            // warningText = Managers.BattleUI.warningUI.GetText();
            currentTurnCnt = Managers.Battle.totalTurnCnt;

            // StartCoroutine(ShowWarningCoroutine());
            // Managers.BattleUI.warningUI.ShowWarningUI();  
        }
    }

    private IEnumerator ShowWarningCoroutine()
    {
        warningText.text = $"{TurnCnt}턴동안 이동력 감소!!";
        Managers.BattleUI.warningUI.SetText(warningText.text);
        yield return new WaitForSeconds(2f);
        Managers.Battle.isPlayerTurn = false;
    }
}
