using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObjectInteraction : MonoBehaviour
{
    [Header("FallingPrefabs")]
    public GameObject[] FallingPrefabs;
    public Transform FallingObjectTransform;

    private Rigidbody faliingRigid;

    int gimmickTurnCnt = 2; // 기믹 작동에 필요한 턴 개수
    int currentTurnCnt = -1; // 현재 총 턴 횟수

    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, FallingPrefabs.Length);
        
        // 초기화 작업
        GameObject fallObject = Instantiate(FallingPrefabs[randomIndex], FallingObjectTransform);
        fallObject.transform.localPosition = Vector3.zero;

        FallingObjectTransform.gameObject.SetActive(false);
        
        faliingRigid = FallingObjectTransform.GetComponent<Rigidbody>();
        if (faliingRigid == null)
            faliingRigid = fallObject.AddComponent<Rigidbody>();

        // 물리 작용 끔
        faliingRigid.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Enter the Gimmick!!");

            currentTurnCnt = Managers.Battle.totalTurnCnt;
            Debug.Log("현재 " + gimmickTurnCnt + " 이후 기믹 발동");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) 
        {
            int remainTurnCnt = Managers.Battle.totalTurnCnt - currentTurnCnt;

            if(remainTurnCnt > gimmickTurnCnt) 
            {
                FallingObjectTransform.gameObject.SetActive(true);
                faliingRigid.isKinematic = false;
            }
        }
    }
}
