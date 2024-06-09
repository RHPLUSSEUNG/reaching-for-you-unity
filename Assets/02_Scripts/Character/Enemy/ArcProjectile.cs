using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ArcProjectile : MonoBehaviour
{
    [SerializeField]
    float duration = 1;
    [SerializeField]

    float rotateSpeed = 300;
    [SerializeField]
    float height = 2;
    [SerializeField]
    AnimationCurve curve;

    Transform target;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(Transform startpos, Transform targetpos)
    {
        target = targetpos;
        transform.position = startpos.position;
        gameObject.SetActive(true);

        StartCoroutine(Flight());
    }
    private IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = target.transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float _height = Mathf.Lerp(0.0f, height, heightT);

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0.0f, _height);
            transform.eulerAngles = new Vector3(0, 0, time* rotateSpeed);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
