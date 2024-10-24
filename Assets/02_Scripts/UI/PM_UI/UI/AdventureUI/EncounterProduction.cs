using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterProduction : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    Transform cameraTransform;

    [SerializeField]
    Image fadeImage;

    float slowDuration = 0.25f;
    float productionDuration = 1.25f;
    float fadeDuration = 1.0f;

    Vector3 initialPos;
    Vector3 velocity = Vector3.zero;
    bool isFastPhasee = false;
    float slowSpeed = 0.5f;

    public IEnumerator Encounter()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.GetComponent<CameraController>().enabled = false;
        fadeImage = GetComponent<Image>();

        StartCoroutine(CameraApproachProduction());

        yield return new WaitForSeconds(slowDuration);

        yield return StartCoroutine(FadeToBattle());

        cameraTransform.GetComponent<CameraController>().enabled = true;
    }

    IEnumerator FadeToBattle()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsedTime <= fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1;
        fadeImage.color = fadeColor;
    }

    IEnumerator CameraApproachProduction()
    {
        Time.timeScale = 0.1f;
        float elapsedTime = 0f;
        while (elapsedTime <= productionDuration)
        {
            elapsedTime += Time.deltaTime;
            if(!isFastPhasee)
            {
                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, playerTransform.position, ref velocity, slowDuration, slowSpeed);
            }
            else
            {
                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, playerTransform.position, ref velocity, (productionDuration - slowDuration));
            }

            if(elapsedTime > slowDuration)
            {
                isFastPhasee = true;
                Time.timeScale = 1.0f;
            }
            yield return null;
        }

        // cameraTransform.position = playerTransform.position;
    }
}
