using System;
using System.Collections.Generic;

public static class ListExtensions
{
    /// <summary>
    /// Retruns if list is empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count == 0;
    }
}
