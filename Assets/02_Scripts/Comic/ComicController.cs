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
    [SerializeField] ComicData comicData;
    List<ComicPage> pages = new List<ComicPage>();
    Transform content;
    Comic currentComic;
    
    int currentPageIndex = 0;    
    float fadeDuration = 0.75f;
    float slideDuration = 0.25f;

    [Header("Slide Option")]
    float smoothTime = 0.2f;
    float velocity = 0.0f;

    private void Awake()
    {
        content = scrollView.content;
    }

    void Start()
    {
        fadeOverlay.alpha = 1.0f;                         
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
            
            ComicPage pageComponent = newPage.GetComponent<ComicPage>();
            if (pageComponent != null)
            {
                pages.Add(pageComponent);
                pageComponent.SetComicController(this);
            }
            pageComponent.gameObject.SetActive(true);
        }                        

        StartCoroutine(FadeInFirstPage());
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
        if (currentPageIndex + 1 < pages.Count)
        {
            pages[currentPageIndex++].gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(FadeOutLastPage());
        }
    }

    private void DestroyCurrentPages()
    {
        for(int i = 0; i < pages.Count; i++)
        {
            Destroy(pages[i].gameObject);
        }
        pages.Clear();
        currentPageIndex = 0;
    }

    public IEnumerator FadeInFirstPage()
    {
        fadeOverlay.blocksRaycasts = true;

        pages[0].gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeOverlay.alpha = 0;
        fadeOverlay.blocksRaycasts = false;
    }

    public IEnumerator FadeOutLastPage()
    {
        float elapsedTime = 0f;
        fadeOverlay.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 1;
        DestroyCurrentPages();
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
