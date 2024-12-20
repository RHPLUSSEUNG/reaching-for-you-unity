using System.Collections;
using UnityEngine;

public class SlideEffect : MonoBehaviour
{
    RectTransform _object;
    public float duration = 0.3f;
    public float pauseDuration = 1.0f;
    public bool isSlide;
    Vector2 startPos;
    Vector2 endPos;

    CameraController cameraController;
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public IEnumerator SetSlideElement()
    {
        _object = gameObject.GetComponent<RectTransform>();
        // Initialize UI Position
        startPos = new Vector2(Screen.width, _object.anchoredPosition.y);
        endPos = new Vector2(0, _object.anchoredPosition.y);

        gameObject.SetActive(true);
        isSlide = true;

        if (Managers.Battle.currentCharacter != null)
        {
            // Camera Movement
            cameraController.ChangeFollowTarget(Managers.Battle.currentCharacter, true);
            cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
            Managers.BattleUI.cameraMode = CameraMode.Follow;
            //cameraController.ChangeOffSet(-2, 1.5f, -2, 20,45);
            Debug.Log("Camera Set");
        }

        yield return StartCoroutine(SlideAnimation(_object, startPos, endPos, duration));

        yield return new WaitForSeconds(pauseDuration + duration);

        gameObject.SetActive(false);
        isSlide = false;

        if(Managers.Battle.currentCharacter == null)
        {
            Managers.Battle.BattleStart();
            yield break;
        }

        if (Managers.Battle.currentCharacter.CompareTag("Player"))
        {
            Managers.Battle.PlayerTurn();
        }
        else
        {
            Managers.Battle.EnemyTurn();
        }
    }

    IEnumerator SlideAnimation(RectTransform _object, Vector2 start, Vector2 end, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _object.anchoredPosition = Vector2.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _object.anchoredPosition = end;
    }
}
