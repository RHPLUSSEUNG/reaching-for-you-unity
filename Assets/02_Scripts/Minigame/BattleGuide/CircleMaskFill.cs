using UnityEngine;

public class CircleMaskFill : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform maskTransform; 
    public float fillSpeed = 1f;

    private Vector3 initialMaskScale;

    void Start()
    {
        initialMaskScale = maskTransform.localScale;
        maskTransform.localScale = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (maskTransform.localScale.x < initialMaskScale.x)
        {
            maskTransform.localScale += new Vector3(fillSpeed * Time.deltaTime, fillSpeed * Time.deltaTime, 0);
        }
        else
        {
            gameObject.GetComponentInParent<HitZone>().Activate();
        }
    }
}
