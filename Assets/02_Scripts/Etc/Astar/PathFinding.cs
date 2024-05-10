using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        StartCoroutine(FindPath(startPos, targetPos));
    }
    public void StartSearch(Vector3 startPos, int radius, string tag)
    {
        StartCoroutine(Search(startPos, radius, tag));
    }
    public void StartRandomLoc(Vector3 startPos, int radius)
    {
        StartCoroutine(RandomLoc(startPos, radius));
    }
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] path = new Vector3[0];
        bool success = false;

        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Node targetNode = grid.GetNodeFromWorldPosition(targetPos);

        if (targetNode.walkable) 
        {
            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();
            openList.Add(startNode);

            while(openList.Count > 0)
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
                    success = true;
                    break;
                }

                foreach (Node node in grid.GetNeighbours(currentNode,1))
                {
                    if (!node.walkable || closedList.Contains(node)) continue;

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

        yield return null;

        if (success)
        {
            path = GetPath(startNode, targetNode);
        }
        pathfinder.FinishProcessingPath(path, success);
        yield break;
    }

    Vector3[] GetPath(Node startNode, Node endNode)
    {
        List<Node> nodeList = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            nodeList.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] path = NodeListToVector3(nodeList);
        Array.Reverse(path);

        return path;
    }

    Vector3[] NodeListToVector3(List<Node> nodeList)
    {
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < nodeList.Count; i++) 
        {
            path.Add(nodeList[i].worldPosition);
        }
        return path.ToArray();
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
    IEnumerator Search(Vector3 startPos, int radius, string tag)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        bool success = false;
        Vector3 targetPos = startPos;
        for (int i = 1; i <= radius; i++)
        {
            foreach (Node node in grid.GetNeighbours(startNode, i))
            {
                if (grid.CheckTag(node.gridX, node.gridY, tag))
                {
                    success = true;
                    targetPos = node.worldPosition;
                    break;
                }
            }
            if (success)
                break;
        }
        yield return null;
        pathfinder.FinishProcessingSearch(targetPos, success);
    }
    IEnumerator RandomLoc(Vector3 startPos, int radius)
    {
        Debug.Log("randomLoc");
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Vector3 target = startPos;
        while (true)
        {
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
                        yield break;
                    }
                }
            }
        }
    }
}
