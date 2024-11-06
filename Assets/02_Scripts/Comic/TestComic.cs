using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComic : MonoBehaviour
{
    [SerializeField] ComicType comicType;
    [SerializeField] bool isShowComic;
    [SerializeField] ComicManager comicManager;

    private void Start()
    {
        isShowComic = false;
    }

    private void Update()
    {
        if(isShowComic)
        {
            comicManager.ShowComic(comicType);
            isShowComic = false;
        }
    }

}
