using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("Top Button Panel")]
    [SerializeField] Button generalButton;
    [SerializeField] Button controlButton;
    [SerializeField] Scrollbar scrollbar;
    
    float scrollSpeed = 10f;
    Coroutine currentCoroutine;

    private void Start()
    {
        generalButton.onClick.AddListener(() => TopButtonClick(true));
        controlButton.onClick.AddListener(() => TopButtonClick(false));
    }

    void TopButtonClick(bool _isGeneral)
    {        
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SlideSettingWindow(_isGeneral));
    }

    IEnumerator SlideSettingWindow(bool _isGeneral)
    {
        while(true)
        {
            yield return null;

            if (_isGeneral)
            {
                scrollbar.value = Mathf.Lerp(scrollbar.value, 0f, Time.deltaTime * scrollSpeed);

                if (Mathf.Approximately(scrollbar.value, 0f))
                {
                    scrollbar.value = 0f;
                    yield break;
                }
            }
            else
            {
                scrollbar.value = Mathf.Lerp(scrollbar.value, 1f, Time.deltaTime * 10f);

                if (Mathf.Approximately(scrollbar.value, 1f))
                {
                    scrollbar.value = 1f;
                    yield break;
                }
            }
        }
                       
    }
}
