using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureMapSelectUI : MonoBehaviour
{
    [SerializeField] Transform selectMapPanel;
    [SerializeField] List<Button> selectButton = new List<Button>();

    private void Start()
    {
        for (int i = 0; i < selectButton.Count; i++)
        {
            int index = i;
            selectButton[i].onClick.AddListener(() => SelectMap(index));
        }
    }

    private void SelectMap(int index)
    {
        AdventureManager.StageNumber = index;
        SceneChanger.Instance.ChangeScene(SceneType.PM_ADVENTURE);
    }
}
