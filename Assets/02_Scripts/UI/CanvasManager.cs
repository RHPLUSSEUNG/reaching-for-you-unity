using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    List<Canvas> canvasList = new List<Canvas>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeCanvasOrder(Canvas canvas)
    {        
        canvasList.RemoveAll(c => c == null);

        if (canvasList.Contains(canvas))
        {
            canvasList.Remove(canvas);
        }

        canvasList.Add(canvas);

        if (canvasList.Count == 1)
        {
            canvasList[0].sortingOrder = 1;
            return;
        }

        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].sortingOrder = i;
        }
    }
}
