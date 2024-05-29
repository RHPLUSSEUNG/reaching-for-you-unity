using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InvenItemUI : UI_Base
{
    public GameObject invenItem;
    public Image itemIcon;
    public ItemType itemType;

    public abstract void SetItemInfo(Image item);
}
