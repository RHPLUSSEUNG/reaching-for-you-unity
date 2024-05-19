using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCombatMap : MonoBehaviour
{
    public Maze mazePrefab;

	private Maze mazeInstance;
    

    private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            // Space Bar Click 시 맵 새로 생성
			RestartGame();
		}
	}

	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}
