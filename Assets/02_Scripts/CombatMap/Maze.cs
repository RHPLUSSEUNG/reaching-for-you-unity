using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Maze : MonoBehaviour
{
	public GameObject cellPrefab;
    public GameObject wallPrefab;
	private MazeCell[,] cells;
    public float generationStepDelay;

	public IntVector2 size;
	
	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}
	
	public IEnumerator Generate () {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		for(int i = 0; i < size.x * size.z; i++) {
			yield return delay;
			DoNextGenerationStep(activeCells);
            
		}
        PlaceWallsInEmptySpaces();
	}

    private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		// activeCells.Add(CreateCell(RandomCoordinates));
	}
    

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			// activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				// neighbor = CreateCell(coordinates);
				// CreatePassage(currentCell, neighbor, direction);
                // CreateWall(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				// CreateWall(currentCell, neighbor, direction);
				// No longer remove the cell here.
			}
		}
		else {
			// CreateWall(currentCell, null, direction);
			// No longer remove the cell here.
		}
	}

    // private MazeCell CreateCell (IntVector2 coordinates) {
	// 	MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
	// 	cells[coordinates.x, coordinates.z] = newCell;
	// 	newCell.coordinates = coordinates;
	// 	newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
	// 	newCell.transform.parent = transform;
	// 	newCell.transform.localPosition =
	// 		new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
	// 	return newCell;
	// }

    private void PlaceWallsInEmptySpaces() {
        for (int x = 0; x < size.x; x++) {
            for (int z = 0; z < size.z; z++) {
                if (cells[x, z] == null) {
                    CreateWall(new IntVector2(x, z));
                }
            }
        }
    }

    private void CreateWall(IntVector2 coordinates) {
        Vector3 position = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 1f, coordinates.z - size.z * 0.5f + 0.5f);
        Instantiate(wallPrefab, position, Quaternion.identity).transform.parent = transform;
    }


    // private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
	// 	MazeWall wall = Instantiate(wallPrefab) as MazeWall;
	// 	wall.Initialize(cell, otherCell, direction);
	// 	if (otherCell != null) {
	// 		wall = Instantiate(wallPrefab) as MazeWall;
	// 		wall.Initialize(otherCell, cell, direction.GetOpposite());
	// 	}
	// }

    public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}
}
