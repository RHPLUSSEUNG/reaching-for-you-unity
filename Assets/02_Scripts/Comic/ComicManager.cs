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

public class ComicManager : MonoBehaviour
{
    [SerializeField] ComicController comic;
    
    private void Start()
    {
        //comic.gameObject.SetActive(false);
    }

    public void ShowComic(ComicType comicType)
    {
        comic.StartComic(comicType);
    }
}
