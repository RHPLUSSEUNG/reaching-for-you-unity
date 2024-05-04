using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFinder : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathFinder instance;
    PathFinding pathFinding;

    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 start, Vector3 end, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(start, end, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0) 
        {
            isProcessingPath = true;
            currentPathRequest = pathRequestQueue.Dequeue();
            pathFinding.StartFindPath(currentPathRequest.start, currentPathRequest.end);
        }
    }

    public void FinishProcessingPath(Vector3[] path, bool succsess)
    {
        currentPathRequest.callback(path, succsess);
        isProcessingPath=false;
        TryProcessNext();
    }
}

struct PathRequest
{
    public Vector3 start;
    public Vector3 end;
    public UnityAction<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, UnityAction<Vector3[], bool> _callback)
    {
        start =_start;
        end = _end;
        callback = _callback;
    }
}
