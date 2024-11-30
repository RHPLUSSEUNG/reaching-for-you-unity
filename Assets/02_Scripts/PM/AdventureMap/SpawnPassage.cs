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
            AdventureManager.StageCount++;
            Debug.Log(AdventureManager.StageCount);
            
            if((AdventureManager.StageCount % 5) != 0)
                gameObject.GetComponent<CameraZone>().spwanIndex = 1;
            else
                gameObject.GetComponent<CameraZone>().spwanIndex = 2;

            AdventureManager.adventure.SpawnPlane(other);
        }
    }
}
