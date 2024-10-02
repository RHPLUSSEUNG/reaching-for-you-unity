using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGimmickInAdventure : MonoBehaviour
{
    bool isActive;
    Button button;

    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.gameObject.SetActive(false);
        isActive = false;

        StartCoroutine(ActiveCollider());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            button.gameObject.SetActive(false);
            isActive = false;
            AdventureManager.GimmickCount--;
            if(AdventureManager.GimmickCount <= 0) 
            {
                ClearGimmick();
                Debug.Log("모든 기믹 해제!");
            }
            
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    
    public void ClearGimmick()
    {
        for (int i = 0; i < RandomPassage.gimmickTrigger.Count; i++)
        {
            RandomPassage.gimmickTrigger[i].gameObject.tag = "Teleport";
            RandomPassage.gimmickTrigger[i].isTrigger = true;
        }
        
        AdventureManager.isGimmickRoom = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag != "Untagged" && collider.transform.CompareTag("Player"))
        {
            isActive = true;
            StopAllCoroutines();
            StartCoroutine(ButtonActiveCoroutine());
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag != "Untagged" && collider.transform.CompareTag("Player"))
        {
            isActive = false;
            button.gameObject.SetActive(false);
        }
    }

    IEnumerator ActiveCollider()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return null;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    IEnumerator ButtonActiveCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        button.gameObject.SetActive(true);
    }
}
