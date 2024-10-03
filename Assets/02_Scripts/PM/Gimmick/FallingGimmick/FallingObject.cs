using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private EntityStat target;
    private Coroutine decreaseCoroutine;

    public float decreaseDuration = 5f; // HP 감소 기간 (초)
    public float decreaseAmount = 50; // 감소할 HP 양

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Transform parent = other.gameObject.transform;
            if(parent != null) 
            {
                target = parent.GetComponentInChildren<EntityStat>();
                if(target != null) 
                {
                    decreaseCoroutine = StartCoroutine("DecreaseHPOverTime");
                }
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            if (decreaseCoroutine != null) {
                StopCoroutine(decreaseCoroutine);
            }
        }
    }


    IEnumerator DecreaseHPOverTime()
    {
        float elapsedTime = 0f;
        float initialHP = target.Hp;
        float targetHP = target.Hp - decreaseAmount;

        while (elapsedTime < decreaseDuration && target.Hp > targetHP)
        {
            float newHP = Mathf.Lerp(initialHP, targetHP, elapsedTime / decreaseDuration);
            target.Hp = Mathf.RoundToInt(newHP);
            Debug.Log(target.Hp);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.Hp = Mathf.RoundToInt(targetHP); // 보간이 완료되면 목표 HP로 설정
    }
}
