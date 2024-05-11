using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PathFinder : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    Queue<SearchRequest> searchRequestQueue = new Queue<SearchRequest>();
    Queue<RandomLocRequest> randomLocRequestQueue = new Queue<RandomLocRequest>();

    RandomLocRequest currentRandomLocRequest;
    PathRequest currentPathRequest;
    SearchRequest currentSearchRequest;

    static PathFinder instance;
    PathFinding pathFinding;

    bool isProcessingPath;
    bool isProcessingSearch;
    bool isProcessingRandomLoc;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 start, Vector3 end, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newPathRequest = new PathRequest(start, end, callback);
        instance.pathRequestQueue.Enqueue(newPathRequest);
        instance.TryProcessNextPathRequest();
    }
    public static void RequestSearch(Vector3 start, int radius, string tag, UnityAction<Vector3, GameObject, bool> callback)
    {
        SearchRequest newSearchRequest = new SearchRequest(start, radius, tag, callback);
        instance.searchRequestQueue.Enqueue(newSearchRequest);
        instance.TryProcessNextSearchRequest();
    }
    public static void RequestRandomLoc(Vector3 start, int radius, UnityAction<Vector3> callback)
    {
        RandomLocRequest newRandomLocRequest = new RandomLocRequest(start, radius, callback);
        instance.randomLocRequestQueue.Enqueue(newRandomLocRequest);
        instance.TryProcessNextRandomLocRequest();
    }

    void TryProcessNextPathRequest()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0) 
        {
            isProcessingPath = true;
            currentPathRequest = pathRequestQueue.Dequeue();
            pathFinding.StartFindPath(currentPathRequest.start, currentPathRequest.end);
        }
    }
    void TryProcessNextSearchRequest()
    {
        if (!isProcessingSearch && searchRequestQueue.Count > 0)
        {
            isProcessingSearch = true;
            currentSearchRequest = searchRequestQueue.Dequeue();
            pathFinding.StartSearch(currentSearchRequest.start, currentSearchRequest.radius, currentSearchRequest.tag);
        }
    }
    void TryProcessNextRandomLocRequest()
    {
        if (!isProcessingRandomLoc && randomLocRequestQueue.Count > 0)
        {
            isProcessingSearch = true;
            currentRandomLocRequest = randomLocRequestQueue.Dequeue();
            pathFinding.StartRandomLoc(currentRandomLocRequest.start, currentRandomLocRequest.radius);
        }
    }

    public void FinishProcessingPath(Vector3[] path, bool succsess)
    {
        currentPathRequest.callback(path, succsess);
        isProcessingPath=false;
        TryProcessNextPathRequest();
    }
    public void FinishProcessingSearch(Vector3 targetPos, GameObject targetObj,bool succsess)
    {
        currentSearchRequest.callback(targetPos, targetObj, succsess);
        isProcessingSearch = false;
        TryProcessNextRandomLocRequest();
    }

    public void FinishProcessingRandomLoc(Vector3 targetPos)
    {
        currentRandomLocRequest.callback(targetPos);
        isProcessingRandomLoc = false;
        TryProcessNextRandomLocRequest();
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
struct SearchRequest
{
    public Vector3 start;
    public int radius;
    public string tag;
    public UnityAction<Vector3, GameObject, bool> callback;
    public SearchRequest(Vector3 _start, int _radius, string _tag, UnityAction<Vector3, GameObject, bool> _callback)
    {
        start = _start;
        radius = _radius;
        tag = _tag;
        callback = _callback;
    }
}
struct RandomLocRequest
{
    public Vector3 start;
    public int radius;
    public UnityAction<Vector3> callback;
    public RandomLocRequest(Vector3 _start, int _radius, UnityAction<Vector3> _callback)
    {
        start = _start;
        radius = _radius;
        callback = _callback;
    }
}
