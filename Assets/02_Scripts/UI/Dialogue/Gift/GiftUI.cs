using System;
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
    bool isGiftActive = false;

    public event Action onGiftPanelUpdated;

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

    public void ShowGiftPanel()
    {
        onGiftPanelUpdated?.Invoke();
        SetGiftActive(true);
        giftPanel.gameObject.SetActive(true);        
    }

    public void ReportTargetNPC(Friend friendNPC)
    {
        giftListUI.SetNPC(friendNPC);
    }

    public void HideGiftPanel()
    {
        SetGiftActive(false);
        giftPanel.gameObject.SetActive(false);        
    }

    public void SetGiftActive(bool _isActive)
    {
        isGiftActive = _isActive;
    }

    public bool GetGiftActive()
    {
        return isGiftActive;
    }

}
