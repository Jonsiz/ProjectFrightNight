using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading; //only needed if multithreading

public class PathRequestManager : MonoBehaviour
{
    /* IMPORTANT
    This section is commented out to test multithreading. Uncomment this if you need to run the requests in a queue as normal.

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    bool isProcessingPath;

    */

    Queue<PathResult> results = new Queue<PathResult>();

    static PathRequestManager instance;
    Pathfinding pathfinding;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock(results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }
    public static void RequestPath(PathRequest request) //Original parameters: (Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        /* Uncomment if not using multithreading
        
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
        */

        ThreadStart threadStart = delegate
        {
            instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    /* IMPORTANT
    This section is commented out to test multithreading. Uncomment this if you need to run the requests in a queue as normal.

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            // Dequeue will get the first item in the Queue and also take it out of the Queue.
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }
    */

    public void FinishedProcessingPath(PathResult result) //Original parameters: "Vector3[] path, bool success"
    {
        /* Uncomment if not using multithreading
         * 
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
        */

        lock(results)
        {
            results.Enqueue(result);
        }
    }

    /* Moved outside the class (below) so it can be accessed from elsewhere
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
    */
}

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}
public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }
}
