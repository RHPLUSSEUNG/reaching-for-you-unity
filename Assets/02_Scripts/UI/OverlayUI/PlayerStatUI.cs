using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text acText;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text mobilityText;
    PlayerStat playerStat;

    private void Start()
    {
        //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        //Redraw();
    }

    void Redraw()
    {
        hpText.text = playerStat.Hp.ToString() + "/" + playerStat.MaxHp.ToString();
        acText.text = playerStat.Defense.ToString();
        manaText.text = playerStat.Mp.ToString() + "/" + playerStat.MaxMp.ToString();
        mobilityText.text = playerStat.MovePoint.ToString() + "/" + playerStat.MaxMovePoint.ToString();
    }
}
