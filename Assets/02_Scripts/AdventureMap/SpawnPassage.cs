using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPassage : MonoBehaviour
{
    [SerializeField]
    GameObject passage;
    RandomPassage randomPassage;
    private void Start() {
        randomPassage = passage.GetComponent<RandomPassage>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            // other.gameObject.transform.parent.GetComponent<Transform>().localPosition = Vector3.zero;
            randomPassage.DeletePassage();
            randomPassage.AddPassage();
        }
    }
}
