using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueBalloon : MonoBehaviour
{
    [SerializeField] string dialogue;
    TextMeshProUGUI dialogueText;
    ComicPage comicPage;
    float typingSpeed = 0.02f;

    private void Start()
    {
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetComicPage(ComicPage _comicPage)
    {
        comicPage = _comicPage;
    }

    public IEnumerator TypingScript()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        comicPage.CheckAllDialogueShown();
    }
}
