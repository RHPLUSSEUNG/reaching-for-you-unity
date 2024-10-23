using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ComicAnimationType
{
    None,
    Slide,
    Fade
}

public class ComicController : MonoBehaviour
{
    [SerializeField] ComicAnimationType animationType = ComicAnimationType.None;
    [SerializeField] ScrollRect scrollView;
    [SerializeField] CanvasGroup fadeOverlay;
    List<ComicPage> pages = new List<ComicPage>();
    [SerializeField] ComicData comicData;
    Comic currentComic;

    Transform content;
    int currentPageIndex = 0;
    float smoothTime = 0.2f;
    float velocity = 0.0f;

    float fadeInDuration = 0.5f;
    float slideDuration = 0.25f;

    private void Awake()
    {
        content = scrollView.content;
    }

    void Start()
    {
        fadeOverlay.alpha = 1.0f;
                
        DeactivateAllPages();     
    }

    public void StartComic(ComicType comicType)
    {        
        currentComic = comicData.GetComic(comicType);
        
        if(pages != null)
        {
            for(int i = 0; i < pages.Count; i++)
            {
                Destroy(pages[i].gameObject);
            }
            pages.Clear();
        }        
        
        foreach (ComicPage pageData in currentComic.GetComicPages)
        {
            GameObject newPage = Instantiate(pageData.gameObject, transform);            
            pages.Add(pageData);
            
            ComicPage pageComponent = newPage.GetComponent<ComicPage>();
            if (pageComponent != null)
            {
                pages.Add(pageComponent);
                pageComponent.SetComicController(this);
            }
        }        
        
        //scrollView.horizontalNormalizedPosition = 0;

        StartCoroutine(FadeInFirstPage());
    }

    //public void StartComic(ComicType comicType)
    //{
    //    currentComic = comicData.GetComic(comicType);
        
    //    foreach (Transform child in content)
    //    {
    //        ComicPage page = child.GetComponent<ComicPage>();
    //        if (page != null)
    //        {
    //            pages.Add(page);
    //            page.SetComicController(this);
    //        }
    //    }
    //}

    private void DeactivateAllPages()
    {
        foreach (ComicPage page in pages)
        {
            page.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPageIndex >= pages.Count) return;

        switch (animationType)
        {
            case ComicAnimationType.None:
                TransitionToNextPage();
                break;
            case ComicAnimationType.Slide:
                pages[currentPageIndex].SetInteractable(false);
                StartCoroutine(SlideToNextPage((float)(++currentPageIndex) / (pages.Count - 1)));
                break;
        }
    }

    private void TransitionToNextPage()
    {
        pages[currentPageIndex++].gameObject.SetActive(false);
        if (currentPageIndex < pages.Count)
        {
            pages[currentPageIndex].gameObject.SetActive(true);
        }
    }

    public IEnumerator FadeInFirstPage()
    {
        fadeOverlay.blocksRaycasts = true;

        pages[0].gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeInDuration);
            yield return null;
        }

        fadeOverlay.alpha = 0;
        fadeOverlay.blocksRaycasts = false;
    }

    public IEnumerator FadeOutLastPage()
    {
        float elapsedTime = 0f;
        fadeOverlay.blocksRaycasts = true;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeInDuration);
            yield return null;
        }
        fadeOverlay.alpha = 1;        
    }

    IEnumerator SlideToNextPage(float targetPosition)
    {
        float currentPosition = scrollView.horizontalNormalizedPosition;

        while (Mathf.Abs(currentPosition - targetPosition) > 0.00005f)
        {
            currentPosition = Mathf.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
            scrollView.horizontalNormalizedPosition = currentPosition;
            yield return null;
        }

        scrollView.horizontalNormalizedPosition = targetPosition;
        pages[currentPageIndex].SetInteractable(true);
    }
}
