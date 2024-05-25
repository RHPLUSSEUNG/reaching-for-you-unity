using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        PM_UI_Manager.UI.CreatePopupUI<ActUI>("ActUI");
    }

}
