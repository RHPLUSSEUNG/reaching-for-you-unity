using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField]
    LayerMask playerMask;

    BattleGuideManager bgManager;

    private void Awake()
    {
        bgManager = GameObject.Find("BattleGuideManager").GetComponent<BattleGuideManager>();
    }
    public void Activate()
    {
        if (Physics.CheckSphere(gameObject.transform.position, gameObject.transform.localScale.x / 2, playerMask))
        {
            bgManager.Hit();
        }
        bgManager.HitZoneDestroy();
        Destroy(gameObject);
    }
}
