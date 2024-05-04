using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    Node[,] grid;

    [SerializeField]
    LayerMask unwalkableMask;
    [SerializeField]
    Vector2 gridWorldSize;
    [SerializeField]
    int nodesize = 1;

    int gridsizeX;
    int gridsizeY;

    void Start()
    {
        gridsizeX = Mathf.RoundToInt(gridWorldSize.x / nodesize);
        gridsizeY = Mathf.RoundToInt(gridWorldSize.y / nodesize);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridsizeX, gridsizeY];
        Vector3 mapBottomLeft = transform.position - (Vector3.right * gridsizeX / 2 * nodesize) - (Vector3.forward * gridsizeY / 2 * nodesize);
        Vector3 wordlPoint;


        for (int x = 0; x < gridsizeX; x++) 
        {
            for (int y = 0; y < gridsizeY; y++)
            {
                wordlPoint = mapBottomLeft + Vector3.right * (x * nodesize + (float)nodesize / 2) + Vector3.forward * (y * nodesize + (float)nodesize / 2);
                bool walkable = true;
                // raycast 또는 checkbox로 오브젝트 검사 후 walkble 수정
                grid[x, y] = new Node(walkable, wordlPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neigbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x==0 && y==0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridsizeX && checkY >= 0 && checkY < gridsizeY)
                {
                    neigbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neigbours;
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridsizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridsizeY - 1) * percentY);
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridsizeX, nodesize, gridsizeY));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, new Vector3(nodesize, nodesize, nodesize));
            }
        }
    }
}
