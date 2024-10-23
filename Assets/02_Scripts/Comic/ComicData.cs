using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Comic
{
    [SerializeField] ComicType comicType;    
    [SerializeField] List<ComicPage> comicPages;

    public ComicType GetComicType { get { return comicType; } }
    public List<ComicPage> GetComicPages { get { return comicPages; } }
}

[CreateAssetMenu(fileName = "ComicsData", menuName = "Comic / Comics", order = 0)]
public class ComicData : ScriptableObject
{
    [SerializeField] Comic[] comicDatas;

    public Comic GetComic(ComicType comicType)
    {
        foreach (var comic in comicDatas)
        {
            if (comic.GetComicType == comicType)
            {
                return comic;
            }
        }
        return new Comic();
    }
}
