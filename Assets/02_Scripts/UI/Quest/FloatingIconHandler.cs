using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FloatingIconHandler : MonoBehaviour
{    
    [SerializeField] Image exclamationMark;
    NPCConversant npcConversant;
    QuestHandler questHandler;
    RectTransform exclamationRectTransform;
    Button actionButton;
    Vector3 originalPosition;
    RectTransform actionButtonRectTransform;
    Vector3 actionButtonOriginalPosition;
    Camera mainCamera;
    float rotateSpeed = 5.0f;
    float elapsedTime = 0f;

    private void Start()
    {
        npcConversant = GetComponent<NPCConversant>();
        questHandler = GetComponent<QuestHandler>();
        exclamationRectTransform = exclamationMark.GetComponent<RectTransform>();
        originalPosition = exclamationRectTransform.localPosition;

        actionButton = npcConversant.GetActionButton();
        actionButtonRectTransform = actionButton.GetComponent<RectTransform>();
        actionButtonOriginalPosition = actionButtonRectTransform.localPosition;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (questHandler.GetIsQuestGiven() && !npcConversant.GetIsOffActionButton())
        {
            exclamationMark.gameObject.SetActive(true);

            elapsedTime += Time.deltaTime;
            float offset = Mathf.Sin(elapsedTime * 2f) * 0.1f;
            exclamationRectTransform.localPosition = originalPosition + new Vector3(0, offset, 0);
            StartCoroutine(RotateUI(exclamationRectTransform));
        }
        else
        {
            exclamationMark.gameObject.SetActive(false);

            elapsedTime += Time.deltaTime;
            float offset = Mathf.Sin(elapsedTime * 2f) * 0.1f;
            actionButtonRectTransform.localPosition = actionButtonOriginalPosition + new Vector3(0, offset, 0);
            StartCoroutine(RotateUI(actionButtonRectTransform));
        }
    }

    IEnumerator RotateUI(RectTransform uiElement)
    {
        float startAngleY = uiElement.eulerAngles.y;
        float cameraAngleY = mainCamera.transform.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float yRotation = Mathf.LerpAngle(startAngleY, cameraAngleY, speed);
            uiElement.eulerAngles = new Vector3(uiElement.eulerAngles.x, yRotation, uiElement.eulerAngles.z);

            yield return null;
        }
    }
}
