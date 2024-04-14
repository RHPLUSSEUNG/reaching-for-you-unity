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

    void Start()
    {
        playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        nextButton.onClick.AddListener(Next);

        UpdateUI();
    }

    void Next()
    {
        playerConversant.Next();
        UpdateUI();
    }
    
    void UpdateUI()
    {        
        NPCResponse.SetActive(!playerConversant.IsChoosing());
        choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

        if(playerConversant.IsChoosing())
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
            }
        }        
        else
        {
            NPCText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
    }
}
