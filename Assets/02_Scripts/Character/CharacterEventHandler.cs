using UnityEngine;

public class CharacterEventHandler : MonoBehaviour
{
    PlayerController controller;
    void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    void HitEventEnd()
    {
        controller.ChangeActive();
    }
}
