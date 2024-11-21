using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    AGrid grid;
    PathFinder pathfinder;

    private void Awake()
    {
        pathfinder = GetComponent<PathFinder>();
        grid = GetComponent<AGrid>();
    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        FindPath(startPos, targetPos, true);
    }
    public void StartSearch(Vector3 startPos, int radius, RangeType type, string tag)
    {
        Search(startPos, radius, type, tag);
    }
    public void StartRandomLoc(Vector3 startPos, int radius)
    {
        RandomLoc(startPos, radius);
    }
    public void StartSkillRange(Vector3 startPos, int radius, RangeType type)
    {
        switch (type)
        {
            case RangeType.Normal:
                SkillRange(startPos, radius);
                break;
            case RangeType.Move:
                MoveRange(startPos, radius);
                break;
            case RangeType.Cross: 
                CrossRange(startPos, radius);
                break;
        }
    }
    List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos, bool isCallback)
    {
        List<Vector3> path = new();
        bool success = false;

        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Node targetNode = grid.GetNodeFromWorldPosition(targetPos);

        //if (targetNode.walkable) 
        {
            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost || (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    if (!targetNode.walkable)
                    {
                        closedList.Remove(currentNode);
                    }
                    success = true;
                    break;
                }

                foreach (Node node in grid.GetNeighbours(currentNode, 1, RangeType.Normal))
                {
                    if (!node.walkable && node != targetNode || closedList.Contains(node)) continue;

                    int newGCost = currentNode.gCost + GetDistanceCost(currentNode, node);

                    if (newGCost < node.gCost || !openList.Contains(node))
                    {
                        if (grid.dontCrossCorner)   //모서리 이동 금지 설정 시
                        {
                            int gridXdiff = currentNode.gridX - node.gridX;
                            int gridYdiff = currentNode.gridY - node.gridY;
                            if (!(gridXdiff == 0) && !(gridYdiff == 0))   //대각 이동
                            {
                                if (!grid.CheckWalkable(currentNode.gridX - gridXdiff, currentNode.gridY) || !grid.CheckWalkable(currentNode.gridX, currentNode.gridY - gridYdiff)) //반대 두 대각위치 노드 검사 
                                {
                                    continue;
                                }
                            }
                        }
                        node.gCost = newGCost;
                        node.hCost = GetDistanceCost(node, targetNode);
                        node.parent = currentNode;

                        if (!openList.Contains(node))
                        {
                            openList.Add(node);
                        }
                    }
                }
            }
        }
        if (success)
            path = GetPath(startNode, targetNode);
        else
            path = null;

        if (isCallback)
            pathfinder.FinishProcessingPath(path, success);

            return path;
    }

    List<Vector3> GetPath(Node startNode, Node endNode)
    {
        List<Node> nodeList = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            nodeList.Add(currentNode);
            currentNode = currentNode.parent;
        }

        List<Vector3> path = NodeListToVector3(nodeList);
        path.Reverse();

        return path;
    }

    List<Vector3> NodeListToVector3(List<Node> nodeList)
    {
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < nodeList.Count; i++)
        {
            path.Add(nodeList[i].worldPosition);
        }
        return path;
    }
    int GetDistanceCost(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

    public bool CheckWalkable(Vector3 target)
    {
        Node targetNode = grid.GetNodeFromWorldPosition(target);
        return grid.CheckWalkable(targetNode.gridX, targetNode.gridY);
    }
    void Search(Vector3 startPos, int radius, RangeType type, string tag)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        bool success = false;
        Vector3 targetPos = startPos;
        GameObject targetObj = null;
        int distance = 0;

        switch (type)
        {
            case RangeType.Normal:
                for (int i = 1; i <= radius; i++)
                {
                    foreach (Node node in grid.GetNeighbours(startNode, i, RangeType.Normal))
                    {
                        targetObj = grid.CheckTag(node.gridX, node.gridY, tag);
                        if (targetObj != null)
                        {
                            success = true;
                            targetPos = node.worldPosition;
                            distance = i;
                            break;
                        }
                    }
                    if (success)
                        break;
                }
                break;
            case RangeType.Cross:
                for (int i = 1; i <= radius; i++)
                {
                    foreach (Node node in grid.GetNeighbours(startNode, i, RangeType.Cross))
                    {
                        targetObj = grid.CheckTag(node.gridX, node.gridY, tag);
                        if (targetObj != null)
                        {
                            success = true;
                            targetPos = node.worldPosition;
                            distance = i;
                            break;
                        }
                    }
                    if (success)
                        break;
                }
                break;
        }
        pathfinder.FinishProcessingSearch(targetPos, targetObj, distance, success);
    }
    void RandomLoc(Vector3 startPos, int radius)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Vector3 target = startPos;
        int i = 0;
        while (true)
        {
            if (i > 1000)
            {
                pathfinder.FinishProcessingRandomLoc(startPos);
                return;
            }

            int x = UnityEngine.Random.Range(-radius, radius + 1);
            int y = UnityEngine.Random.Range(-radius, radius + 1);

            if (x != 0 || y != 0)
            {
                int gridX = startNode.gridX + x;
                int gridY = startNode.gridY + y;
                if (grid.CheckGridBound(gridX, gridY))
                {
                    if (grid.CheckWalkable(gridX, gridY))
                    {
                        target = grid.GetWorldPositionFromNode(gridX, gridY);
                        pathfinder.FinishProcessingRandomLoc(target);
                        return;
                    }
                }
            }
        }
    }
    void SkillRange(Vector3 startPos, int radius)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Vector3 targetPos = startPos;
        List<GameObject> targetTiles = new();
        GameObject tile;
        for (int i = 1; i <= radius; i++)
        {
            foreach (Node node in grid.GetNeighbours(startNode, i, RangeType.Normal))
            {
                tile = grid.GetTileFromNode(node);
                if (tile != null)
                {
                    targetTiles.Add(grid.GetTileFromNode(node));
                }
            }
        }
        pathfinder.FinishProcessingSkillRange(targetTiles);
    }
    void MoveRange(Vector3 startPos, int radius)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        List<GameObject> targetTiles = new();

        for (int i = 1; i <= radius; i++)
        {
            foreach (Node node in grid.GetNeighbours(startNode, i, RangeType.Normal))
            {
                if (!node.walkable)
                    continue;
                else
                {
                    List<Vector3> path = FindPath(startPos, grid.GetWorldPositionFromNode(node.gridX, node.gridY), false);

                    if (path.Count != 0 && path.Count <= radius)
                    {
                            GameObject tile = grid.GetTileFromNode(node);
                            if (tile != null)
                                targetTiles.Add(tile);
                    }
                }
                   
            }
        }
        pathfinder.FinishProcessingSkillRange(targetTiles);
    }

    void CrossRange(Vector3 startPos, int radius)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Vector3 targetPos = startPos;
        List<GameObject> targetTiles = new();
        for (int i = 1; i <= radius; i++)
        {
            foreach (Node node in grid.GetNeighbours(startNode, i, RangeType.Cross))
            {
                targetTiles.Add(grid.GetTileFromNode(node));
            }
        }
        pathfinder.FinishProcessingSkillRange(targetTiles);
    }
}
