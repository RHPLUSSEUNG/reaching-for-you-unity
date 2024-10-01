using UnityEngine;
using UnityEngine.UI;

public class WorldHPUI : UI_Base
{
    enum WorldHpBar
    {
        HpBar
    }

    EntityStat stat;
    Transform character;
    Camera mainCamera;
    CapsuleCollider characterCollider;
    Slider bar;

    public override void Init()
    {
        Bind<GameObject>(typeof(WorldHpBar));

        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        character = transform.parent;
        stat = character.GetComponent<EntityStat>();

        mainCamera = Camera.main;
        characterCollider = character.GetComponentInChildren<CapsuleCollider>();
        bar = GetObject((int)WorldHpBar.HpBar).GetComponent<Slider>();
    }

    private void Update()
    {
        UpdateHealthBarPosition();

        float ratio = stat.Hp / (float)stat.MaxHp;
        SetHpRatio(ratio);
    }

    private void UpdateHealthBarPosition()
    {
        if (characterCollider != null)
        {
            float characterHeight = characterCollider.bounds.size.y;

            Vector3 healthBarPosition = character.position + Vector3.up * characterHeight;
            transform.position = healthBarPosition;
        }

        Vector3 lookAtPosition = transform.position + mainCamera.transform.forward;

        transform.LookAt(lookAtPosition);
    }

    private void SetHpRatio(float ratio)
    {
        bar.value = ratio;
    }
}