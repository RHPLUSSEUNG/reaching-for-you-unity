using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicPage : MonoBehaviour
{
    List<CanvasGroup> dialogueBalloons = new List<CanvasGroup>();
    Button pageButton;    
    int balloonIndex = 0;
    bool allDialoguesShown = false;

    ComicController comicController;

    private void Awake()
    {
        pageButton = GetComponent<Button>();
        dialogueBalloons.Clear();
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            CanvasGroup balloonGroup = child.GetComponent<CanvasGroup>();
            if (balloonGroup != null)
            {
                dialogueBalloons.Add(balloonGroup);
                balloonGroup.alpha = 0;                
            }
        }

        if (dialogueBalloons.Count == 0)
        {
            allDialoguesShown = true;
        }
        
        pageButton.onClick.AddListener(OnPageClicked);
    }
    
    public void OnPageClicked()
    {
        if (!allDialoguesShown)
        {
            if (balloonIndex < dialogueBalloons.Count)
            {                
                StartCoroutine(ShowNextBalloon(dialogueBalloons[balloonIndex], 0.5f));
                balloonIndex++;
            }
           
            if (balloonIndex >= dialogueBalloons.Count)
            {
                allDialoguesShown = true;
            }
        }
        else
        {
            OnPageFinished();
        }
    }
    
    IEnumerator ShowNextBalloon(CanvasGroup balloon, float duration)
    {        
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            balloon.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);            
            yield return null;
        }
        
        balloon.alpha = 1;        
    }    

    public void OnPageFinished()
    {
        comicController.NextPage();
    }

    public void SetComicController(ComicController _comicController)
    {
        comicController = _comicController;
    }
}
