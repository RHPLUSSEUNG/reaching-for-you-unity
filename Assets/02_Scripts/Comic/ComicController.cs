using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComicController : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;    
    [SerializeField] List<ComicPage> pages = new List<ComicPage>();
    Transform content;   
    int currentPageIndex = 0;

    private void Awake()
    {
        content = scrollView.content;
    }

    void Start()
    {
        foreach (Transform child in content)
        {
            ComicPage page = child.GetComponent<ComicPage>();
            if (page != null)
            {
                pages.Add(page);
                page.SetComicController(this);
            }
        }

        //if (pages.Count > 0)
        //{
        //    pages[0].gameObject.SetActive(true);
        //}

    }
    IEnumerator SlideToNextPage(float targetPosition, float duration)
    {
        float elapsedTime = 0f;
        float startPosition = scrollView.horizontalNormalizedPosition;

        pages[currentPageIndex].GetComponent<Button>().interactable = false;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollView.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }
        
        scrollView.horizontalNormalizedPosition = targetPosition;
        pages[currentPageIndex].GetComponent<Button>().interactable = true;
    }

    public void NextPage()
    {
        pages[currentPageIndex++].GetComponent<Button>().interactable = false;        
        if (currentPageIndex < pages.Count)
        {            
            StartCoroutine(SlideToNextPage((float)currentPageIndex / (pages.Count - 1), 0.4f));
        }
    }
}
