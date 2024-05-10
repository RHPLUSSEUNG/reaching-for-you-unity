using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    Node[,] grid;

    [SerializeField]
    LayerMask unwalkableMask;
    [SerializeField]
    Vector2 gridWorldSize = new Vector2(10,10);
    [SerializeField]
    int nodesize = 1;

    public bool dontCrossCorner;

    Vector3 mapBottomLeft;

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
        mapBottomLeft = transform.position - (Vector3.right * gridsizeX / 2 * nodesize) - (Vector3.forward * gridsizeY / 2 * nodesize);
        Vector3 wordlPoint;


        for (int x = 0; x < gridsizeX; x++) 
        {
            for (int y = 0; y < gridsizeY; y++)
            {
                wordlPoint = GetWorldPositionFromNode(x, y);
                bool walkable = !Physics.CheckSphere(wordlPoint, nodesize/2, unwalkableMask);
                grid[x, y] = new Node(walkable, wordlPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node, int radius)  //radius 크기만큼 떨어져 있는 노드만 검색
    {
        List<Node> neigbours = new List<Node>();
        for (int x = -radius; x <= radius; x ++) 
        {
            for (int y = -radius; y <= radius; y ++)
            {
                if (Mathf.Abs(x) != radius && Mathf.Abs(y) != radius) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(CheckGridBound(checkX,checkY))
                {
                    neigbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neigbours;
    }
    public bool CheckWalkable(int gridX, int gridY)
    {
        return grid[gridX, gridY].walkable;
    }
    public bool CheckGridBound(int gridX, int gridY)
    {
        if (gridX >= 0 && gridX < gridsizeX && gridY >= 0 && gridY < gridsizeY)
        {
           return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckTag(int gridX, int gridY, string tag)  //노드 위치에 태그 매치되는 대상 유무 검사
    {
        Vector3 wordlPoint = GetWorldPositionFromNode(gridX, gridY);
        foreach (Collider col in Physics.OverlapSphere(wordlPoint, nodesize / 2))
        {
            if (col.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
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
    public Vector3 GetWorldPositionFromNode(int gridX, int gridY)
    {
        Vector3 wordlPoint = mapBottomLeft + Vector3.right * (gridX * nodesize + (float)nodesize / 2) + Vector3.forward * (gridY * nodesize + (float)nodesize / 2);
        return wordlPoint;
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
    public int GetGridSizeX()
    {
        return gridsizeX;
    }
    public int GetGridSizeY()
    {
        return gridsizeY;
    }
}
