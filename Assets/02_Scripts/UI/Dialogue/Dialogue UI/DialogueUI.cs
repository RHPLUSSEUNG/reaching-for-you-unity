using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI NPCText;    
    [SerializeField] GameObject NPCResponse;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;
    //[SerializeField] Button nextButton;
    //[SerializeField] Button quitButton;
    [SerializeField] TextMeshProUGUI conversantName;
    [SerializeField] TextMeshProUGUI nextText;
    [SerializeField] TextMeshProUGUI quitText;

    float typingSpeed = 0.02f;    
    //bool isSkip = false;

    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<PlayerConversant>();
        playerConversant.onConversationUpdated += UpdateUI;
        //nextButton.onClick.AddListener(() => playerConversant.Next());
        //quitButton.onClick.AddListener(() => playerConversant.Quit());
        //nextButton.gameObject.SetActive(false);
        //quitButton.gameObject.SetActive(false);
        nextText.gameObject.SetActive(false);
        quitText.gameObject.SetActive(false);        
        UpdateUI();
    }    
    
    void UpdateUI()
    {        
        gameObject.SetActive(playerConversant.IsActive());

        if(!playerConversant.IsActive())
        {
            return;
        }
        //nextButton.gameObject.SetActive(false);
        //quitButton.gameObject.SetActive(false);
        conversantName.text = playerConversant.GetCurrentConversantName();
        NPCResponse.SetActive(!playerConversant.IsChoosing());
        choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
        nextText.gameObject.SetActive(false);
        quitText.gameObject.SetActive(false);        

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

        if (NPCText.text.Length == text.Length)
        {
            if(playerConversant.HasNext())
            {
                //nextButton.gameObject.SetActive(true);
                StartCoroutine(NextDialgoue());
                nextText.gameObject.SetActive(true);

            } 
            else
            {
                //quitButton.gameObject.SetActive(true);
                StartCoroutine(ExitDialogue());
                quitText.gameObject.SetActive(true);
            }
        }     
    }

    IEnumerator NextDialgoue()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                playerConversant.Next();
                yield break;
            }
        }
    }

    IEnumerator ExitDialogue()
    {        
        while (true) 
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                playerConversant.Quit();
                yield break;
            }
        }             
    }

    public void SkipTyping()
    {
        //[TODO]
    }
}
