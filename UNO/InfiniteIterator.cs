using System;
using System.Collections.Generic;

public class InfiniteIterator<T>
{
    private readonly List<T> items;
    private int currentIndex;

    public InfiniteIterator(IEnumerable<T> items)
    {
        this.items = new List<T>(items);
        currentIndex = 0;
    }

    public T Current => items[currentIndex];

    public void MoveNext()
    {
        currentIndex = (currentIndex + 1) % items.Count;
    }

    public void MovePrevious()
    {
        currentIndex = (currentIndex - 1 + items.Count) % items.Count;
    }

}