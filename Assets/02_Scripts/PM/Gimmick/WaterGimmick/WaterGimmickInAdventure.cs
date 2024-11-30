using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGimmickInAdventure : MonoBehaviour
{
    // 해제할 때까지 hp - 5;
    bool isActive;
    bool isClear = false;
    Button button;

    public delegate void ClearGimmickDelegate();
    public ClearGimmickDelegate clearGimmickDelegate;

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
            isClear = true;

            AdventureManager.GimmickCount--;
            if(AdventureManager.GimmickCount <= 0) 
            {
                clearGimmickDelegate();
                Debug.Log("모든 기믹 해제!");
            }
            
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag != "Untagged" && collider.transform.CompareTag("Player") && !isClear)
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
