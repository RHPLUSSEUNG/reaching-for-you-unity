using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HitZone : MonoBehaviour
{
    [SerializeField]
    LayerMask playerMask;

    BattleGuideUI battleGuideUI;
    PolygonCollider2D collider;
    private void Awake()
    {
        battleGuideUI = GameObject.Find("BattleGuideUI(Clone)").GetComponent<BattleGuideUI>();
        collider = gameObject.GetComponent<PolygonCollider2D>();
        collider.enabled = false;
    }
    public void Activate()
    {
        collider.enabled = true;
        battleGuideUI.CheckOverlap(this.GetComponent<RectTransform>());
        battleGuideUI.ReturnToPool(gameObject);
    }
}
