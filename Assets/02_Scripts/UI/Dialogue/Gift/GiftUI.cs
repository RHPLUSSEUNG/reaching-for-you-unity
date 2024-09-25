using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftUI : MonoBehaviour
{
    //[LSH:TODO] [High Complexity: REFACTORING REQUIRED]
    public static GiftUI Instance;
    [SerializeField] GiftListUI giftListUI;
    [SerializeField] Transform giftPanel;
    [SerializeField] Transform alertPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        giftPanel.gameObject.SetActive(false);
        alertPanel.gameObject.SetActive(false);
    }

    public void ShowGiftList()
    {
        giftPanel.gameObject.SetActive(true);        
    }

    public void ReportTargetNPC(Friend friendNPC)
    {
        giftListUI.SetNPC(friendNPC);
    }

    public void HideGiftPanel()
    {
        giftPanel.gameObject.SetActive(false);
    }
}
