using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct SimpleComics
{    
    [SerializeField] List<Sprite> sequences;
        
    public List<Sprite> Sequences { get { return sequences; } }
}

public class LoadingComicController : MonoBehaviour
{
    [SerializeField] SimpleComics[] comics;
    [SerializeField] Image[] comicPivot;

    private void Start()
    {
        LoadComic();
    }
    
    public void LoadComic()
    {
        int comicNumber = Random.Range(0, comics.Length);
        for (int i = 0; i < comics[comicNumber].Sequences.Count; i++)
        {
            comicPivot[i].sprite = comics[comicNumber].Sequences[i];
        }
    }

}
