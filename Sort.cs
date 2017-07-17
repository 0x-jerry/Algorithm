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
                    if (s == Sequence.Increase)
                    {
                        if (arr[j].CompareTo(arr[j + 1]) > 0)
                        {
                            Tool.Swap(ref arr[j], ref arr[j + 1]);
                            if (!swapped) swapped = true;
                        }
                    }
                    else
                    {
                        if (arr[j].CompareTo(arr[j + 1]) < 0)
                        {
                            Tool.Swap(ref arr[j], ref arr[j + 1]);
                            if (!swapped) swapped = true;
                        }
                    }
                }
                if (!swapped) return;
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
                if (s == Sequence.Increase)
                    for (; j >= 0 && insert.CompareTo(arr[j]) < 0; j--)
                        arr[j + 1] = arr[j];
                else
                    for (; j >= 0 && insert.CompareTo(arr[j]) > 0; j--)
                        arr[j + 1] = arr[j];

                arr[j + 1] = insert;
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
                    if (s == Sequence.Increase)
                    {
                        if (arr[swapIndex].CompareTo(arr[j]) > 0) swapIndex = j;
                    }
                    else
                    {
                        if (arr[swapIndex].CompareTo(arr[j]) < 0) swapIndex = j;
                    }
                }
                if (swapIndex != i) Tool.Swap(ref arr[i], ref arr[swapIndex]);
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
                    if (s == Sequence.Increase)
                        for (; j >= 0 && insert.CompareTo(arr[j]) < 0; j -= gap)
                            arr[j + gap] = arr[j];
                    else
                        for (; j >= 0 && insert.CompareTo(arr[j]) > 0; j -= gap)
                            arr[j + gap] = arr[j];

                    arr[j + gap] = insert;
                }
            }

        }

        /// <summary>
        /// 快速排序 —— 不稳定 —— 最坏O(n^2) —— 平均O(nlogn)
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

            if (s == Sequence.Increase)
            {
                Quick(arr, left, i - 1);
                Quick(arr, j + 1, right);
            }
            else
            {
                Quick(arr, left, i - 1, Sequence.Decrease);
                Quick(arr, j + 1, right, Sequence.Decrease);
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
    }
}
