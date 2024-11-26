using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HitZone : MonoBehaviour
{
    [SerializeField]
    LayerMask playerMask;

    BattleGuideUI battleGuideUI;
    RectTransform rect;
    //PolygonCollider2D collider;
    private void Awake()
    {
        battleGuideUI = GameObject.Find("BattleGuideUI(Clone)").GetComponent<BattleGuideUI>();
        rect = this.GetComponent<RectTransform>();
        //collider = gameObject.GetComponent<PolygonCollider2D>();
        //collider.enabled = false;
    }
        public void Activate()
    {
        //collider.enabled = true;
        battleGuideUI.CheckOverlap(rect);
        battleGuideUI.ReturnToPool(gameObject);
    }
}
