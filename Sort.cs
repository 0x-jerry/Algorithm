using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Sort
    {
        public enum Sequence
        {
            Increase,
            Decrease
        }

        /// <summary>
        /// 冒泡排序 —— 稳定 —— 最坏O(n^2) —— 平均O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Bubble<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            bool swapped = false;
            for (int i = 0; i < arr.Length; i++)
            {
                swapped = false;
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (s == Sequence.Increase ? arr[j].CompareTo(arr[j + 1]) > 0 : arr[j].CompareTo(arr[j + 1]) < 0)
                    {
                        Tool.Swap(ref arr[j], ref arr[j + 1]);
                        if (!swapped) swapped = true;
                    }

                }
                if (!swapped) return;
            }
        }

        /// <summary>
        /// 鸡尾酒排序 —— 稳定 —— 最坏O(n^2) —— 平均O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Cocktail<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            int left = 0;
            int right = arr.Length - 1;
            bool swapped = true;

            while (swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (s == Sequence.Increase ? arr[i].CompareTo(arr[i + 1]) > 0 : arr[i].CompareTo(arr[i + 1]) < 0)
                    {
                        Tool.Swap(ref arr[i], ref arr[i + 1]);
                        if (!swapped) swapped = true;
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (s == Sequence.Increase ? arr[i - 1].CompareTo(arr[i]) > 0 : arr[i - 1].CompareTo(arr[i]) < 0)
                    {
                        Tool.Swap(ref arr[i - 1], ref arr[i]);
                        if (!swapped) swapped = true;
                    }
                }
                left++;
            }
        }

        /// <summary>
        /// 插入排序 —— 稳定 —— 最坏O(n^2) —— 平均O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Insertion<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            T insert;
            int j;

            for (int i = 1; i < arr.Length; i++)
            {
                insert = arr[i];
                j = i - 1;

                for (; j >= 0 && (s == Sequence.Increase ? insert.CompareTo(arr[j]) < 0 : insert.CompareTo(arr[j]) > 0); j--)
                    arr[j + 1] = arr[j];

                arr[j + 1] = insert;
            }
        }

        /// <summary>
        /// 希尔排序 —— 不稳定 —— 最坏O(n(logn)^2) —— 平均(根据选择的步长而不一样)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Shell<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            int gap = 1;
            while (gap < arr.Length / 3)
                gap = gap * 3 + 1;
            T insert;
            int j = 0;

            for (; gap > 0; gap /= 3)
            {
                for (int i = gap; i < arr.Length; i++)
                {
                    insert = arr[i];
                    j = i - gap;

                    for (; j >= 0 && (s == Sequence.Increase ? insert.CompareTo(arr[j]) < 0 : insert.CompareTo(arr[j]) > 0); j -= gap)
                        arr[j + gap] = arr[j];

                    arr[j + gap] = insert;
                }
            }

        }

        /// <summary>
        /// 选择排序 —— 不稳定 —— 最坏O(n^2) —— 平均O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Selection<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int swapIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (s == Sequence.Increase ? arr[swapIndex].CompareTo(arr[j]) > 0 : arr[swapIndex].CompareTo(arr[j]) < 0)
                        swapIndex = j;
                }
                if (swapIndex != i) Tool.Swap(ref arr[i], ref arr[swapIndex]);
            }
        }

        /// <summary>
        /// 快速排序（递归版） —— 不稳定 —— 最坏O(n^2) —— 平均O(nlogn)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Quick<T>(T[] arr, int left, int right, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            if (left >= right) return;

            T middle = arr[(left + right) / 2];

            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                if (s == Sequence.Increase)
                {
                    while (arr[++i].CompareTo(middle) < 0) ;    // i 表示 >= middle 的下标
                    while (arr[--j].CompareTo(middle) > 0) ;    // j 表示 <= middle 的下标
                }
                else
                {
                    while (arr[++i].CompareTo(middle) > 0) ;    // i 表示 <= middle 的下标
                    while (arr[--j].CompareTo(middle) < 0) ;    // j 表示 >= middle 的下标
                }
                if (i >= j) break;
                Tool.Swap(ref arr[i], ref arr[j]);
            }

            Quick(arr, left, i - 1, s);
            Quick(arr, j + 1, right, s);
        }

        /// <summary>
        /// 快速排序（迭代版）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Quick<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            if (arr.Length <= 0) return;
            Stack<Range> range = new Stack<Range>();
            range.Push(new Range(0, arr.Length - 1));

            while (range.Count > 0)
            {
                Range r = range.Pop();

                if (r.min >= r.max) continue;
                T mid = arr[r.max];
                int left = r.min;
                int right = r.max - 1;

                while (left < right)
                {
                    if (s == Sequence.Increase)
                    {
                        while (arr[left].CompareTo(mid) < 0 && left < right) left++;
                        while (arr[right].CompareTo(mid) >= 0 && left < right) right--;
                    }
                    else
                    {
                        while (arr[left].CompareTo(mid) > 0 && left < right) left++;
                        while (arr[right].CompareTo(mid) <= 0 && left < right) right--;
                    }

                    Tool.Swap(ref arr[left], ref arr[right]);
                }

                if (s == Sequence.Increase ? arr[left].CompareTo(arr[r.max]) >= 0 : arr[left].CompareTo(arr[r.max]) <= 0)
                    Tool.Swap(ref arr[left], ref arr[r.max]);
                else
                    left++;

                range.Push(new Range(r.min, left - 1));
                range.Push(new Range(left + 1, r.max));
            }
        }

        /// <summary>
        /// 计数排序 —— 稳定 —— 时间线性为 O(n + k) k = 待排序序列中最大值 - 最小值 + 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static int[] Counting(int[] arr, Sequence s = Sequence.Increase)
        {
            int max = arr[0], min = arr[0];
            foreach (var item in arr)
            {
                if (item.CompareTo(max) > 0) max = item;
                if (item.CompareTo(min) < 0) min = item;
            }

            int count = max - min + 1;

            int[] store = new int[count];

            for (int i = 0; i < arr.Length; i++)
            {
                store[arr[i] - min]++;
            }

            for (int i = 1; i < store.Length; ++i)
            {
                store[i] = store[i] + store[i - 1];
            }

            int[] sorted = new int[arr.Length];

            for (int i = arr.Length - 1; i >= 0; --i)
            {
                if (s == Sequence.Increase)
                    sorted[--store[arr[i] - min]] = arr[i];
                else
                    sorted[arr.Length - 1 - (--store[arr[i] - min])] = arr[i];
            }

            return sorted;
        }

        /// <summary>
        /// 归并排序（迭代版） —— 稳定 —— 最坏O(nlogn) —— 平均O(nlogn)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        public static void Merge<T>(T[] arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            T[] copy = new T[arr.Length];

            for (int seg = 1; seg < arr.Length; seg += seg)
            {
                for (int start = 0; start < arr.Length; start += seg + seg)
                {
                    int low = start;
                    int mid = Tool.Min(start + seg, arr.Length);
                    int high = Tool.Min(start + seg + seg, arr.Length);
                    int k = low;

                    int start1 = low, end1 = mid;
                    int start2 = mid, end2 = high;

                    while (start1 < end1 && start2 < end2)
                        copy[k++] = (s == Sequence.Increase ? arr[start1].CompareTo(arr[start2]) < 0 : arr[start1].CompareTo(arr[start2]) > 0) ?
                            arr[start1++] : arr[start2++];
                    while (start1 < end1)
                        copy[k++] = arr[start1++];
                    while (start2 < end2)
                        copy[k++] = arr[start2++];
                }
                Tool.Swap(ref arr, ref copy);
            }
        }

        /// <summary>
        /// 归并排序（递归版） —— 合并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Queue<T> MergeRecursive<T>(Queue<T> left, Queue<T> right, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            Queue<T> merge = new Queue<T>();

            while (left.Count > 0 && right.Count > 0)
                merge.Enqueue(
                    (s == Sequence.Increase ? left.Peek().CompareTo(right.Peek()) > 0 : left.Peek().CompareTo(right.Peek()) < 0) ?
                    right.Dequeue() : left.Dequeue());
            while (left.Count > 0) merge.Enqueue(left.Dequeue());
            while (right.Count > 0) merge.Enqueue(right.Dequeue());

            return merge;
        }

        /// <summary>
        /// 归并排序（递归版） —— 分割
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Queue<T> MergeRecursive<T>(Queue<T> arr, Sequence s = Sequence.Increase) where T : IComparable<T>
        {
            if (arr.Count < 2)
                return arr;

            Queue<T> left = new Queue<T>();
            Queue<T> right = new Queue<T>();

            int mid = arr.Count / 2;

            for (int i = 0; i < mid; i++) left.Enqueue(arr.Dequeue());
            while (arr.Count > 0) right.Enqueue(arr.Dequeue());

            left = MergeRecursive(left, s);
            right = MergeRecursive(right, s);

            return MergeRecursive(left, right, s);
        }
    }
}
