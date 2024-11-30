using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTest : MonoBehaviour
{
    void Start()
    {
        Managers.UI.CreatePopupUI<MiniGameStartPopupUI>("MiniGameStartPopup");
    }
}
