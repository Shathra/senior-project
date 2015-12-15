using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionQueue {

    protected List<Action> heap;

    public ActionQueue()
    {
        this.heap = new List<Action>();
    }

    public bool Empty()
    {
        return heap.Count == 0;
    }

	public void Insert( Action action)
    {
        heap.Add(action);

        int index = heap.Count - 1;
        bool ordered = false;
        while( index != 0 && !ordered)
        {
            int parentIndex = (index + 1) / 2 - 1;
            if( heap[index].priority <= heap[parentIndex].priority)
            {
                Action temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = heap[index];
                index = parentIndex;
            }

            else
            {
                ordered = true;
            }
        }
    }

    public Action Peek()
    {
        if( heap.Count != 0)
        {

            return heap[0];
        }

        return null;
    }

    public Action Remove()
    {
        if( heap.Count != 0)
        {
            Action result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            Heapify(0);

            return result;
        }

        return null;
    }

    protected void Heapify( int index)
    {
        int leftIndex = (index + 1) * 2 - 1;
        int rightIndex = (index + 1) * 2;
        if (leftIndex >= heap.Count)
            return;

        int minIndex;
        if (rightIndex >= heap.Count)
            minIndex = leftIndex;
        else
            minIndex = heap[leftIndex].priority < heap[rightIndex].priority ? leftIndex : rightIndex;

        if( heap[minIndex].priority <= heap[index].priority)
        {
            Action temp = heap[index];
            heap[index] = heap[minIndex];
            heap[minIndex] = temp;
            Heapify(minIndex);
        }
    }
}
