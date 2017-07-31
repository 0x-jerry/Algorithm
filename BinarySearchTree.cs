using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        public BinarySearchTree() : base()
        {
        }
        public BinarySearchTree(BinaryTreeNode<T> node) : base(node)
        {
        }

        public override BinaryTreeNode<T> GetNode(T data)
        {
            BinaryTreeNode<T> node = Root;

            while (true)
            {
                if (node == null) return null;
                else if (node.Data.CompareTo(data) < 0) node = node.RChild;
                else if (node.Data.CompareTo(data) > 0) node = node.LChild;
                else return node;
            }
        }

        private BinaryTreeNode<T> FindMinNode(BinaryTreeNode<T> root)
        {
            var minNode = root;
            while (minNode.LChild != null) minNode = minNode.LChild;
            return minNode;
        }

        private BinaryTreeNode<T> FindMaxNode(BinaryTreeNode<T> root)
        {
            var maxNode = root;
            while (maxNode.RChild != null) maxNode = maxNode.RChild;
            return maxNode;
        }

        private void RotateRight(BinaryTreeNode<T> node)
        {
            var pivot = node.LChild;
            if (node.Parent != null)
            {
                if (node.Parent.LChild == node)
                {
                    node.Parent.LChild = pivot;
                }
                else
                {
                    node.Parent.RChild = pivot;
                }
            }
            else
            {
                this.root = pivot;
                pivot.Parent = null;
            }
            node.LChild = pivot.RChild;
            pivot.RChild = node;
        }

        private void RotateLeft(BinaryTreeNode<T> node)
        {
            var pivot = node.RChild;
            if (node.Parent != null)
            {
                if (node.Parent.LChild == node)
                {
                    node.Parent.LChild = pivot;
                }
                else
                {
                    node.Parent.RChild = pivot;
                }
            }
            else
            {
                this.root = pivot;
                pivot.Parent = null;
            }
            node.RChild = pivot.LChild;
            pivot.LChild = node;
        }

        public void AddNode(T data)
        {
            BinaryTreeNode<T> node = Root;
            BinaryTreeNode<T> insertNode = new BinaryTreeNode<T>(data);

            if (node == null)
            {
                CreateTree(insertNode);
                return;
            }

            while (true)
            {
                if (node.Data.CompareTo(data) < 0)
                {
                    if (node.RChild == null)
                    {
                        node.RChild = insertNode;
                        break;
                    }
                    else
                    {
                        node = node.RChild;
                    }
                }
                else if (node.Data.CompareTo(data) > 0)
                {
                    if (node.LChild == null)
                    {
                        node.LChild = insertNode;
                        break;
                    }
                    else
                    {
                        node = node.LChild;
                    }
                }
                else break;
            }

            while (insertNode.Parent != null)
            {
                insertNode = insertNode.Parent;
                BalanceNode(insertNode);
            }
        }

        private void BalanceNode(BinaryTreeNode<T> node)
        {
            if (node.GetBalance() == 2)
            {
                if (node.LChild.GetBalance() > 0) RotateRight(node);
                else
                {
                    RotateLeft(node.LChild);
                    RotateRight(node);
                }
            }
            else if (node.GetBalance() == -2)
            {
                if (node.RChild.GetBalance() < 0) RotateLeft(node);
                else
                {
                    RotateRight(node.RChild);
                    RotateLeft(node);
                }
            }
        }

        public override void Delete(T data)
        {
            Delete(GetNode(data));
        }

        protected override void Delete(BinaryTreeNode<T> node)
        {
            if (node == null) return;

            BinaryTreeNode<T> breakBalanceNode = null;

            if (node.RChild != null)
            {
                BinaryTreeNode<T> minNode = FindMinNode(node.RChild);

                breakBalanceNode = minNode.Parent;

                node.Data = minNode.Data;

                if (breakBalanceNode == node)
                {
                    node.RChild = minNode.RChild;
                }
                else
                {
                    minNode.Parent.LChild = minNode.RChild;
                }
            }
            else
            {
                if (node.Parent != null)
                {
                    breakBalanceNode = node.Parent;

                    if (node.Parent.RChild == node) node.Parent.RChild = node.LChild;
                    else node.Parent.LChild = node.LChild;
                }
                else
                    root = node.LChild;

            }

            while (breakBalanceNode != null)
            {
                BalanceNode(breakBalanceNode);
                breakBalanceNode = breakBalanceNode.Parent;
            }
        }

        private void Delete(BinaryTreeNode<T> node, T data)
        {
            if (node == null) return;
            else if (data.CompareTo(node.Data) < 0) Delete(node.LChild, data);
            else if (data.CompareTo(node.Data) > 0) Delete(node.RChild, data);
            else if (node.LChild != null && node.RChild != null)
            {
                BinaryTreeNode<T> minNode = FindMinNode(node.RChild);
                node.Data = minNode.Data;
                Delete(node.RChild, node.Data);
            }
            else
            {
                if (node.LChild != null && node.RChild == null)
                {
                    BinaryTreeNode<T> minNode = FindMaxNode(node.LChild);
                    node.Data = minNode.Data;
                    Delete(node.LChild, node.Data);
                }
                else if (node.LChild == null && node.RChild != null)
                {
                    BinaryTreeNode<T> minNode = FindMinNode(node.RChild);
                    node.Data = minNode.Data;
                    Delete(node.RChild, node.Data);
                }
                else
                {
                    if (node.Parent != null)
                    {
                        if (node.Parent.LChild == node) node.Parent.LChild = null;
                        else node.Parent.RChild = null;
                    }
                    node = null;
                }
            }

            if (node != null)
                BalanceNode(node);
        }

        public T[] ToArray()
        {
            List<T> list = new List<T>();
            Traverse((item) =>
            {
                list.Add(item.Data);
            }, TraverseType.InOrder);
            return list.ToArray();
        }
    }
}
