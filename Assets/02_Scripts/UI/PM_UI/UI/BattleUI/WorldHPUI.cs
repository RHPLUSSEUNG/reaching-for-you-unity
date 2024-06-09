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

    public override void Init()
    {
        Bind<GameObject>(typeof(WorldHpBar));

        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        character = transform.parent;
        stat = character.GetComponent<EntityStat>();
    }

    // TODO : ratio설정 Update문 사용 안하기 도전
    private void Update()
    {
        // transform.position = character.position + Vector3.up * (character.GetComponentInChildren<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = stat.Hp / (float)stat.MaxHp;
        SetHpRatio(ratio);
    }

    private void SetHpRatio(float ratio)
    {
        GetObject((int)WorldHpBar.HpBar).GetComponent<Slider>().value = ratio;
    }
}
