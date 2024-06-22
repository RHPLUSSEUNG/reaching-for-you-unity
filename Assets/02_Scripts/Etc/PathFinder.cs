using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFinder : MonoBehaviour
{
    Queue<PathRequest> requestQueue = new Queue<PathRequest>();

    PathRequest currentRequest;

    static PathFinder instance;
    PathFinding pathFinding;

    bool isProcessing;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 start, Vector3 end, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newPathRequest = new PathRequest(start, end, callback);
        instance.requestQueue.Enqueue(newPathRequest);
        instance.TryProcessNextRequest();
    }
    public static void RequestSearch(Vector3 start, int radius, string tag, UnityAction<Vector3, GameObject, int, bool> callback)
    {
        PathRequest newSearchRequest = new PathRequest(start, radius, tag, callback);
        instance.requestQueue.Enqueue(newSearchRequest);
        instance.TryProcessNextRequest();
    }
    public static void RequestRandomLoc(Vector3 start, int radius, UnityAction<Vector3> callback)
    {
        PathRequest newRandomLocRequest = new PathRequest(start, radius, callback);
        instance.requestQueue.Enqueue(newRandomLocRequest);
        instance.TryProcessNextRequest();
    }
    public static void RequestSkillRange(Vector3 start, int radius, UnityAction<List<GameObject>> callback)
    {
        PathRequest newSkillRangeRequest = new PathRequest(start, radius, callback);
        instance.requestQueue.Enqueue(newSkillRangeRequest);
        instance.TryProcessNextRequest();
    }
    public static bool CheckWalkable(Vector3 target)
    {
        return instance.pathFinding.CheckWalkable(target);
    }

    void TryProcessNextRequest()
    {
        if (!isProcessing && requestQueue.Count > 0) 
        {
            isProcessing = true;
            currentRequest = requestQueue.Dequeue();

            if (currentRequest.callback!=null)
            {
                pathFinding.StartFindPath(currentRequest.start, currentRequest.end);
            }
            else if(currentRequest.searchCallback != null)
            {
                pathFinding.StartSearch(currentRequest.start, currentRequest.radius, currentRequest.tag);
            }
            else if(currentRequest.randomLocCallback != null)
            {
                pathFinding.StartRandomLoc(currentRequest.start, currentRequest.radius);
            }
            else if (currentRequest.skillRangeCallback != null)
            {
                pathFinding.StartSkillRange(currentRequest.start, currentRequest.radius);
            }
        }
    }

    public void FinishProcessingPath(Vector3[] path, bool succsess)
    {
        currentRequest.callback(path, succsess);
        isProcessing = false;
        TryProcessNextRequest();
    }
    public void FinishProcessingSearch(Vector3 targetPos, GameObject targetObj, int distance, bool succsess)
    {
        currentRequest.searchCallback(targetPos, targetObj, distance, succsess);
        isProcessing = false;
        TryProcessNextRequest();
    }

    public void FinishProcessingRandomLoc(Vector3 targetPos)
    {
        currentRequest.randomLocCallback(targetPos);
        isProcessing = false;
        TryProcessNextRequest();
    }
    public void FinishProcessingSkillRange(List<GameObject> targetObjects)
    {
        currentRequest.skillRangeCallback(targetObjects);
        isProcessing = false;
        TryProcessNextRequest();
    }
}
struct PathRequest
{
    public Vector3 start;
    public Vector3 end;
    public int radius;
    public string tag;

    public UnityAction<Vector3[], bool> callback;
    public UnityAction<Vector3, GameObject, int, bool> searchCallback;
    public UnityAction<Vector3> randomLocCallback;
    public UnityAction<List<GameObject>> skillRangeCallback;
    public PathRequest(Vector3 _start, Vector3 _end, UnityAction<Vector3[], bool> _callback)
    {
        start =_start;
        end = _end;
        callback = _callback;

        radius = 0;
        tag = null;
        searchCallback = null;
        randomLocCallback = null;
        skillRangeCallback = null;
    }
    public PathRequest(Vector3 _start, int _radius, string _tag, UnityAction<Vector3, GameObject, int, bool> _callback)
    {
        start = _start;
        radius = _radius;
        tag = _tag;
        searchCallback = _callback;

        end = Vector3.zero;
        callback = null;
        randomLocCallback = null;
        skillRangeCallback = null;
    }
    public PathRequest(Vector3 _start, int _radius, UnityAction<Vector3> _callback)
    {
        start = _start;
        radius = _radius;
        randomLocCallback = _callback;

        end = Vector3.zero;
        tag = null;
        searchCallback = null;
        callback = null;
        skillRangeCallback = null;
    }
    public PathRequest(Vector3 _start, int _radius, UnityAction<List<GameObject>> _callback)
    {
        start = _start;
        radius = _radius;
        skillRangeCallback = _callback;

        end = Vector3.zero;
        tag = null;
        searchCallback = null;
        callback = null;
        randomLocCallback = null;
    }
}
