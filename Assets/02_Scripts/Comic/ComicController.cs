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
    SceneType sceneType;

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
        fadeOverlay.alpha = 0.0f;
        fadeOverlay.gameObject.SetActive(false);
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
            pageComponent.gameObject.SetActive(false);
        }
        fadeOverlay.gameObject.SetActive(true);
        StartCoroutine(FadeFirstPage());
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
            pages[currentPageIndex].gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(FadeLastPage());
        }
    }

    private void InitializeComicController()
    {
        for(int i = 0; i < pages.Count; i++)
        {
            Destroy(pages[i].gameObject);
        }
        pages.Clear();
        currentPageIndex = 0;
        fadeOverlay.alpha = 0.0f;
        fadeOverlay.gameObject.SetActive(false);
        ComicManager.Instance.SetCanvasSortingOrder(0);
        SceneChange();
    }

    void SceneChange()
    {
        SceneType targetScene = ComicManager.Instance.GetTargetScene();

        if (targetScene != SceneType.NONE)
        {
            SceneChanger.Instance.ChangeScene(targetScene);
        }
    }

    public IEnumerator FadeFirstPage()
    {
        float elapsedTime = 0f;
        fadeOverlay.alpha = 1;
        fadeOverlay.blocksRaycasts = true;
        fadeOverlay.gameObject.SetActive(true);

        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        
        elapsedTime = 0f;
        pages[0].gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeOverlay.alpha = 0;
        fadeOverlay.blocksRaycasts = false;
    }

    public IEnumerator FadeLastPage()
    {
        float elapsedTime = 0f;
        fadeOverlay.blocksRaycasts = true;
        SoundManager.Instance.StopMusic();
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 1;
        fadeOverlay.gameObject.SetActive(false);
        InitializeComicController();
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
