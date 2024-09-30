using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureMapSelectUI : MonoBehaviour
{
    public static AdventureMapSelectUI Instance;
    [SerializeField] Transform selectMapPanel;
    [SerializeField] List<Button> selectButton = new List<Button>();
    [SerializeField] Button exitButton;

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
        exitButton.onClick.AddListener(ClosePanel);
        for (int i = 0; i < selectButton.Count; i++)
        {
            int index = i;
            selectButton[i].onClick.AddListener(() => SelectMap(index));
        }
        ClosePanel();
    }

    private void SelectMap(int index)
    {
        AdventureManager.StageNumber = index;
        SceneChanger.Instance.ChangeScene(SceneType.PM_ADVENTURE);
    }

    public void OpenPanel()
    {
        selectMapPanel.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        selectMapPanel.gameObject.SetActive(false);
    }
}
