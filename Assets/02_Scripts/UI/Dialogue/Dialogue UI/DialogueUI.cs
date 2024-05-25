using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI NPCText;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject NPCResponse;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI conversantName;

    float typingSpeed = 0.02f;
    //bool isSkip = false;

    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponentInChildren<PlayerConversant>();
        playerConversant.onConversationUpdated += UpdateUI;
        nextButton.onClick.AddListener(() => playerConversant.Next());
        quitButton.onClick.AddListener(() => playerConversant.Quit());
        nextButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        UpdateUI();
    }    
    
    void UpdateUI()
    {        
        gameObject.SetActive(playerConversant.IsActive());

        if(!playerConversant.IsActive())
        {
            return;
        }
        nextButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        conversantName.text = playerConversant.GetCurrentConversantName();
        NPCResponse.SetActive(!playerConversant.IsChoosing());
        choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

        if(playerConversant.IsChoosing())
        {
            BuildChoiceList();
        }        
        else
        {
            //NPCText.text = playerConversant.GetText();
            NPCText.text = "";
            StartCoroutine(TypingScript(playerConversant.GetText(), typingSpeed));
            //nextButton.gameObject.SetActive(playerConversant.HasNext());
            //quitButton.gameObject.SetActive(!playerConversant.HasNext());
        }
    }

    void BuildChoiceList()
    {
        //choiceRoot.DetachChuildren();
        foreach (Transform item in choiceRoot)
        {
            Destroy(item.gameObject);
        }

        foreach (DialogueNode choice in playerConversant.GetChoices())
        {
            GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
            var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
            textComp.text = choice.GetText();
            Button button = choiceInstance.GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                playerConversant.SelectChoice(choice);                
            });
        }
    }

    IEnumerator TypingScript(string text, float typingSpeed)
    {
        NPCText.text = "";
        foreach (char character in text)
        {            
            NPCText.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (NPCText.text.Length == text.Length && playerConversant.HasNext())
        {
            nextButton.gameObject.SetActive(true);
        }
        else if (NPCText.text.Length == text.Length && !playerConversant.HasNext())
        {
            quitButton.gameObject.SetActive(true);
        }
    }

    public void SkipTyping()
    {
        //[TODO]
    }
}
