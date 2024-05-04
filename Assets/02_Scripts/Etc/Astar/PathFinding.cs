using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    AGrid grid;
    PathFinder pathfinder;

    private void Awake()
    {
        pathfinder =GetComponent<PathFinder>();
        grid = GetComponent<AGrid>();
    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
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

                foreach (Node node in grid.GetNeighbours(currentNode))
                {
                    if (!node.walkable || closedList.Contains(node)) continue;

                    int newGCost = currentNode.gCost + GetDistanceCost(currentNode, node);

                    if (newGCost < node.gCost || !openList.Contains(node))
                    {
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
        for (int i = 1; i < nodeList.Count; i++) 
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
}
