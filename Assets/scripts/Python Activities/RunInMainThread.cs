using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInMainThread : MonoBehaviour
{
    private static readonly Queue<System.Action> queue = new Queue<System.Action>();
    private static readonly Queue<System.Action> Fixedqueue = new Queue<System.Action>();

    public static void ExecuteInUpdate(System.Action action)
    {
        lock (queue)
        {
            queue.Enqueue(action);
        }
    }

    public static void ExecuteInFixedUpdate(System.Action action)
    {
        lock (Fixedqueue)
        {
            Fixedqueue.Enqueue(action);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Execute all queued actions
        while (queue.Count > 0)
        {
            System.Action action = null;
            lock (queue)
            {
                if (queue.Count > 0)
                {
                    action = queue.Dequeue();
                }
            }

            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }

    void FixedUpdate()
    {
        // Execute all queued actions
        while (Fixedqueue.Count > 0)
        {
            System.Action action = null;
            lock (Fixedqueue)
            {
                if (Fixedqueue.Count > 0)
                {
                    action = Fixedqueue.Dequeue();
                }
            }

            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}
