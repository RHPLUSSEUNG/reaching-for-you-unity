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
    float smoothTime = 0.2f;
    float velocity = 0.0f;

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
        float currentPosition = scrollView.horizontalNormalizedPosition;

        pages[currentPageIndex].GetComponent<Button>().interactable = false;

        //while (elapsedTime < duration)
        //{
        //    elapsedTime += Time.deltaTime;
        //    scrollView.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);            
        //    yield return null;
        //}

        while (Mathf.Abs(currentPosition - targetPosition) > 0.00005f)  // 목표 위치에 거의 도달할 때까지 반복
        {
            currentPosition = Mathf.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
            scrollView.horizontalNormalizedPosition = currentPosition;  // ScrollRect 위치 업데이트
            yield return null;  // 한 프레임 대기
        }

        scrollView.horizontalNormalizedPosition = targetPosition;
        pages[currentPageIndex].GetComponent<Button>().interactable = true;
    }

    public void NextPage()
    {
        pages[currentPageIndex++].GetComponent<Button>().interactable = false;        
        if (currentPageIndex < pages.Count)
        {            
            StartCoroutine(SlideToNextPage((float)currentPageIndex / (pages.Count - 1), 0.25f));
        }
    }
}
