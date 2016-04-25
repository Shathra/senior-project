using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// It is action list which every enemy AI units have. It stores actions in a priority queue manner.
/// </summary>
public class ActionQueue {

    protected List<Action> heap;
    //From functions
    Action result;
    int index;
    int leftIndex;
    int rightIndex;
    int minIndex;
    Action temp;
    bool ordered;
    int parentIndex;

    /// <summary>
    /// Blank constructor, initializes member variables.
    /// </summary>
    public ActionQueue()
    {
        this.heap = new List<Action>();
    }

    /// <summary>
    /// Returns if action queue is empty
    /// </summary>
    /// <returns>True if empty, false otherwise</returns>
    public bool Empty()
    {
        return heap.Count == 0;
    }

    /// <summary>
    /// Obvious, adds an action.
    /// </summary>
    /// <param name="action"></param>
    public void Insert( Action action)
    {
        heap.Add(action);

        index = heap.Count - 1;
        ordered = false;
        while ( index != 0 && !ordered)
        {
            parentIndex = (index + 1) / 2 - 1;
            if ( heap[index].priority >= heap[parentIndex].priority)
            {
                temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;
                index = parentIndex;
            }
            else
            {
                ordered = true;
            }
        }
    }
    public void Display() {
        string str = "";
        for (int i = 0; i < heap.Count; i++) {
            str += i+"."+heap[i]+", ";
        }
        Debug.Log(str);
    }
    /// <summary>
    /// Returns action which has highest priority
    /// </summary>
    /// <returns>Action with the highest priority, null if ActionQueue empty.</returns>
    public Action Peek()
    {
        if( heap.Count != 0)
        {
            return heap[0];
        }

        return null;
    }

    /// <summary>
    /// Removes and returns highest priority action in the queue.
    /// </summary>
    /// <returns>Returns null if ActionQueue is empty</returns>

    public Action Remove()
    {
        if( heap.Count != 0)
        {
            result = heap[0]; 
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            Heapify(0);
            return result;
        }

        return null;
    }
    public Action Remove(Action action) {
        index = heap.IndexOf(action);
            
        if (index >= 0) {
            result = heap[index];
            heap[index] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            Heapify(index);
            return result;
        }

        return null;
    }
    protected void Heapify( int index)
    {
        leftIndex = (index + 1) * 2 - 1;
        rightIndex = (index + 1) * 2;
        if (leftIndex >= heap.Count)
            return;
        
        if (rightIndex >= heap.Count)
            minIndex = leftIndex;
        else 
            minIndex = heap[leftIndex].priority > heap[rightIndex].priority ? leftIndex : rightIndex;
        
        if( heap[minIndex].priority > heap[index].priority)
        {
            temp = heap[index];
            heap[index] = heap[minIndex];
            heap[minIndex] = temp;
            Heapify(minIndex);
        }
    }
}
