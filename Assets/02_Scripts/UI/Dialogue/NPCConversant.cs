using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCConversant : MonoBehaviour
{
    [SerializeField] string conversantName;
    [SerializeField] Dialogue dialogue = null;
    [SerializeField] Button actionButton;
    //[SerializeField] Dialogue endDialgoue = null;

    PlayerConversant playerConversant;
    bool isDialogueAction = false;

    private void Start()
    {
        actionButton.onClick.AddListener(StartDialogue);
        actionButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDialogueAction)
        {
            StartDialogue();
            isDialogueAction = false;
            playerConversant = null;
        }
    }

    public void StartDialogue()
    {
        if (dialogue == null)
        {
            Debug.Log("Empty Dialogue");
        }
        else
        {
            isDialogueAction = false;
            playerConversant.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            actionButton.gameObject.SetActive(false);
            playerConversant = null;
            ObjectiveTracer.Instance.ReportNPCTalked(conversantName);
        }
    }

    public void SetActionButton(PlayerConversant _playerConversant)
    {
        isDialogueAction = true;
        playerConversant = _playerConversant;
        actionButton.gameObject.SetActive(true);
    }

    public void OffButton()
    {
        isDialogueAction = false;
        actionButton.gameObject.SetActive(false);
        playerConversant = null;
    }

    public string GetName()
    {
        return conversantName;
    }

    public bool GetIsOffActionButton()
    {
        return actionButton.IsActive();
    }

    public Button GetActionButton()
    {
        return actionButton;
    }
}
