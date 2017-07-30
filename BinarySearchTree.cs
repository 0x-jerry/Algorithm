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

        private void RotateRight(BinaryTreeNode<T> root, BinaryTreeNode<T> pivot)
        {
            if (root.Parent != null)
            {
                if (root.Parent.LChild == root)
                {
                    root.Parent.LChild = pivot;
                }
                else
                {
                    root.Parent.RChild = pivot;
                }
            }
            else
            {
                this.root = pivot;
                pivot.Parent = null;
            }
            root.LChild = pivot.RChild;
            pivot.RChild = root;
        }

        private void RotateLeft(BinaryTreeNode<T> root, BinaryTreeNode<T> pivot)
        {
            if (root.Parent != null)
            {
                if (root.Parent.LChild == root)
                {
                    root.Parent.LChild = pivot;
                }
                else
                {
                    root.Parent.RChild = pivot;
                }
            }
            else
            {
                this.root = pivot;
                pivot.Parent = null;
            }
            root.RChild = pivot.LChild;
            pivot.LChild = root;
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

            Balance(insertNode);
        }

        private void Balance(BinaryTreeNode<T> insertNode)
        {
            BinaryTreeNode<T> balanceRoot = insertNode;
            BinaryTreeNode<T> balancePivot = balanceRoot;
            while (balanceRoot.Parent != null)
            {
                balancePivot = balanceRoot;
                balanceRoot = balanceRoot.Parent;
                if (balanceRoot.GetBalance() == 2 && balancePivot.GetBalance() == 1)
                {
                    RotateRight(balanceRoot, balancePivot);
                    break;
                }
                else if (balanceRoot.GetBalance() == -2 && balancePivot.GetBalance() == -1)
                {
                    RotateLeft(balanceRoot, balancePivot);
                    break;
                }
                else if (balanceRoot.GetBalance() == 2 && balancePivot.GetBalance() == -1)
                {
                    RotateLeft(balancePivot, balancePivot.RChild);
                    RotateRight(balanceRoot, balanceRoot.LChild);
                    break;
                }
                else if (balanceRoot.GetBalance() == -2 && balancePivot.GetBalance() == 1)
                {
                    RotateRight(balancePivot, balancePivot.LChild);
                    RotateLeft(balanceRoot, balanceRoot.RChild);
                    break;
                }
            }
        }

        public override void Delete(T data)
        {
            base.Delete(data);
        }
    }
}
