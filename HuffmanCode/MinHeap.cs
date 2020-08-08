using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCode
{
    public class MinHeap<T> : IEnumerable<T>
    {
        private List<T> heap;
        private IComparer<T> comparer;
        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;

        public MinHeap(IComparer<T> comparer)
        {
            heap = new List<T>();
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public MinHeap()
        {
            heap = new List<T>();
            this.comparer = Comparer<T>.Default;
        }

        
        public void Clear()
        {
            heap = new List<T>();
        }

        public void Add(T val)
        {
            heap.Add(val);
            HeapifyUp(heap.Count - 1);
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException("Items cannot be null.");
            }

            foreach (var item in items)
            {
                Add(item);
            }
        }

        public T Pop()
        {
            if (heap.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty.");
            }
            var returnVal = heap[0];
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return returnVal;
        }

        public IEnumerable<T> PopAll()
        {
            List<T> list = new List<T>();

            while (!IsEmpty)
            {
                list.Add(Pop());
            }

            return list;
        }

        private void HeapifyUp(int index)
        {
            if (index <= 0)
            {
                return;
            }

            int parentIndex = (index - 1) / 2;

            if (comparer.Compare(heap[index], heap[parentIndex]) < 0)
            {
                T temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;

                HeapifyUp(parentIndex);
            }
        }

        private void HeapifyDown(int index)
        {
            if (index >= heap.Count)
            {
                return;
            }

            int leftChildIndex = index * 2 + 1;

            if (leftChildIndex >= heap.Count)
            {
                return;
            }

            int potentialSwapIndex = leftChildIndex;

            int rightChildIndex = index * 2 + 2;

            if (rightChildIndex < heap.Count && comparer.Compare(heap[rightChildIndex], heap[leftChildIndex]) < 0)
            {
                potentialSwapIndex = rightChildIndex;
            }

            if (comparer.Compare(heap[potentialSwapIndex], heap[index]) < 0)
            {
                T temp = heap[index];
                heap[index] = heap[potentialSwapIndex];
                heap[potentialSwapIndex] = temp;

                HeapifyDown(potentialSwapIndex);
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)heap).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)heap).GetEnumerator();
        }
    }
}
