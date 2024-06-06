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

    public float decreaseDuration = 5f; // HP 감소 기간 (초)
    public float decreaseAmount = 50f; // 감소할 HP 양

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
            FallingObjectTransform.gameObject.SetActive(true);
            faliingRigid.isKinematic = false;
        }
    }
}
