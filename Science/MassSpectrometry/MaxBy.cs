//-----------------------------------------------------------------------
// Copyright 2018 Eli Lilly and Company
//
// Licensed under the Apache License, Version 2.0 (the "License");
//
// you may not use this file except in compliance with the License.
//
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2008-2011 Jonathan Skeet. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;


public static partial class MoreEnumerable
{
    /// <summary>
    /// Returns the maximal element of the given sequence, based on
    /// the given projection.
    /// </summary>
    /// <remarks>
    /// If more than one element has the maximal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current maximal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <returns>The maximal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        return source.MaxBy(selector, Comparer<TKey>.Default);
    }

    /// <summary>
    /// Returns the maximal element of the given sequence, based on
    /// the given projection and the specified comparer for projected values. 
    /// </summary>
    /// <remarks>
    /// If more than one element has the maximal projected value, the first
    /// one encountered will be returned. This overload uses the default comparer
    /// for the projected type. This operator uses immediate execution, but
    /// only buffers a single result (the current maximal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <param name="comparer">Comparer to use to compare projected values</param>
    /// <returns>The maximal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
    /// or <paramref name="comparer"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                TSource temp = default(TSource); //Instead of returning an exception trying to get a default value of a particular type
                return temp;
                //throw new InvalidOperationException("Sequence was empty"); 
            }
            TSource max = sourceIterator.Current;
            TKey maxKey = selector(max);
            while (sourceIterator.MoveNext())
            {
                TSource candidate = sourceIterator.Current;
                TKey candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) > 0)
                {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }
            return max;
        }
    }

    public static Type GetItemType<T>(this IEnumerable<T> enumerable)
    {
        return typeof(T);
    }


    public static object d { get; set; }
}

