using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComicType
{
    INTRO,
    CHAPTER_1,
    CHAPTER_2,
    MAX_SIZE,
}

public class ComicManager : MonoBehaviour, ICanvasSortingOrder
{
    public static ComicManager Instance;
    [SerializeField] ComicController comic;
    Canvas canvas;
    SceneType targetScene = SceneType.NONE;    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (comic == null)
        {
            comic = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ComicController>();
        }
        canvas = GetComponent<Canvas>();
    }

    public void ShowComic(ComicType comicType)
    {
        SetCanvasSortingOrder(1);
        targetScene = GetSceneForComic(comicType);
        comic.StartComic(comicType);
    }

    private SceneType GetSceneForComic(ComicType comicType)
    {
        switch (comicType)
        {
            case ComicType.INTRO:
                return SceneType.AM;
            case ComicType.CHAPTER_1:
                return SceneType.PM_ADVENTURE;
            case ComicType.CHAPTER_2:
                return SceneType.PM_COMBAT;
            default:
                return SceneType.NONE;
        }
    }

    public SceneType GetTargetScene()
    {
        return targetScene;
    }

    public void SetCanvasSortingOrder(int order)
    {
        canvas.sortingOrder = order;
    }
}
