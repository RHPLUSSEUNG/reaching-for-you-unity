using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

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
    Vector3 worldPoint;

    int gridsizeX;
    int gridsizeY;

    void Start()
    {
        gridsizeX = Mathf.RoundToInt(gridWorldSize.x / nodesize);
        gridsizeY = Mathf.RoundToInt(gridWorldSize.y / nodesize);
        CreateGrid();
    }
    private void FixedUpdate()
    {
        UpdateGrid();
    }
    public void UpdateGrid()
    {
        for (int x = 0; x < gridsizeX; x++)
        {
            for (int y = 0; y < gridsizeY; y++)
            {
                worldPoint = grid[x, y].GetPosition();
                bool walkable = !Physics.CheckSphere(worldPoint, nodesize / 2, unwalkableMask);
                grid[x, y].walkable = walkable;
            }
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridsizeX, gridsizeY];
        // mapBottomLeft = transform.position - (Vector3.right * gridsizeX / 2 * nodesize) - (Vector3.forward * gridsizeY / 2 * nodesize);
        mapBottomLeft = transform.position;


        for (int x = 0; x < gridsizeX; x++) 
        {
            for (int y = 0; y < gridsizeY; y++)
            {
                //worldPoint = mapBottomLeft + Vector3.right * (x * nodesize + (float)nodesize / 2) + Vector3.forward * (y * nodesize + (float)nodesize / 2);
                worldPoint = mapBottomLeft + Vector3.right * (x * nodesize + (float)nodesize / 2) + Vector3.forward * (y * nodesize + (float)nodesize / 2);
                bool walkable = !Physics.CheckSphere(worldPoint, nodesize/2, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node, int radius, RangeType type)  //radius 크기만큼 떨어져 있는 노드만 검색
    {
        List<Node> neigbours = new List<Node>();
        if (type == RangeType.Normal)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (Mathf.Abs(x) != radius && Mathf.Abs(y) != radius) 
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (CheckGridBound(checkX, checkY))
                    {
                        neigbours.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        else if (type == RangeType.Cross)
        {
            int checkX = node.gridX + radius;
            int checkY = node.gridY + radius;

            if (CheckGridBound(checkX, node.gridY))
                neigbours.Add(grid[checkX, node.gridY]);
            if (CheckGridBound(node.gridX, checkY))
                neigbours.Add(grid[node.gridX, checkY]);

            checkX = node.gridX - radius;
            checkY = node.gridY - radius;

            if (CheckGridBound(checkX, node.gridY))
                neigbours.Add(grid[checkX, node.gridY]);
            if (CheckGridBound(node.gridX, checkY))
                neigbours.Add(grid[node.gridX, checkY]);
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
    public GameObject CheckTag(int gridX, int gridY, string tag)  //노드 위치에 태그 매치되는 대상 유무 검사, 있으면 해당 오브젝트 반환
    {
        Vector3 worldPoint = GetWorldPositionFromNode(gridX, gridY);
        foreach (Collider col in Physics.OverlapSphere(worldPoint, nodesize / 2))
        {
            if (col.gameObject.CompareTag(tag))
            {
                return col.gameObject;
            }
        }
        return null;
    }
   
    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = ( worldPosition.x - mapBottomLeft.x) / gridWorldSize.x;
        float percentY = ( worldPosition.z - mapBottomLeft.z) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridsizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridsizeY - 1) * percentY);
        return grid[x, y];
    }
    public Vector3 GetWorldPositionFromNode(int gridX, int gridY)
    {
        return grid[gridX,gridY].GetPosition();
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
    public GameObject GetTileFromNode(Node node)
    {
        RaycastHit ray;
        GameObject Tile = null;
        int tileLayer = 1 << LayerMask.NameToLayer("Tile");
        if (Physics.Raycast(node.worldPosition, Vector3.down, out RaycastHit hit, nodesize, tileLayer))
        {
            Tile = hit.transform.gameObject;
        }
        return Tile;
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
