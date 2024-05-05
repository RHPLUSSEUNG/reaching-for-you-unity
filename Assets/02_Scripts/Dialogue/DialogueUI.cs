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

    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        playerConversant.onConversationUpdated += UpdateUI;
        nextButton.onClick.AddListener(() => playerConversant.Next());
        quitButton.onClick.AddListener(() => playerConversant.Quit());

        UpdateUI();
    }    
    
    void UpdateUI()
    {        
        gameObject.SetActive(playerConversant.IsActive());

        if(!playerConversant.IsActive())
        {
            return;
        }

        NPCResponse.SetActive(!playerConversant.IsChoosing());
        choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

        if(playerConversant.IsChoosing())
        {
            BuildChoiceList();
        }        
        else
        {
            NPCText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
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
}
