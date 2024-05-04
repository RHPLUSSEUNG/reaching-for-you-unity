using System.Collections;
using UnityEngine;

public class EnemyAI_Base : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    float speed = 10;

    int targetIndex;
    Vector3[] path;
    bool ismoving;
    public void Awake()
    {

    }

    public void MoveToward()
    {
        if (ismoving)
        {
            Debug.Log("Object is Moving!");
            return;
        }
        PathFinder.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newpath, bool succsess)
    {
        if (succsess)
        {
            path = newpath;
            ismoving = true;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        targetIndex = 0;
        Vector3 currentWaypoint = path[targetIndex];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    ismoving = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}

