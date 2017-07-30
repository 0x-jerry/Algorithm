using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class BinaryTree<T>
    {
        public enum TraverseType
        {
            PreOrder,
            InOrder,
            PostOrder,
            LevelOrder
        }

        public delegate void VisitNode(BinaryTreeNode<T> node);

        protected BinaryTreeNode<T> root;

        public BinaryTree()
        {
            root = null;
        }

        public BinaryTree(BinaryTreeNode<T> node)
        {
            root = node;
        }

        /// <summary>
        /// Create a binary tree with a node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if root is null </returns>
        public bool CreateTree(BinaryTreeNode<T> node)
        {
            bool created = false;
            if (root == null)
            {
                root = node;
                created = true;
            }
            return created;
        }

        public BinaryTreeNode<T> Root { get => root; }

        public bool IsEmpty()
        {
            return Root == null ? true : false;
        }

        public int GetDepth()
        {
            return Root.GetTreeDepth();
        }

        public int GetNodeNumber()
        {
            return Root.GetNodeNumber();
        }

        public virtual BinaryTreeNode<T> GetNode(T data)
        {
            Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                BinaryTreeNode<T> node = queue.Dequeue();
                if (node.Data.Equals(data))
                    return node;
                if (node.LChild != null) queue.Enqueue(node.LChild);
                if (node.RChild != null) queue.Enqueue(node.RChild);
            }

            return null;
        }

        public void Insert(T data, BinaryTreeNode<T>.ChildType type, BinaryTreeNode<T> insert)
        {
            BinaryTreeNode<T> node = GetNode(data);
            if (node != null)
            {
                if (type == BinaryTreeNode<T>.ChildType.Left)
                {
                    insert.LChild = node.LChild;
                    node.LChild = insert;
                }
                else
                {
                    insert.RChild = node.RChild;
                    node.RChild = insert;
                }
            }
        }

        public void Insert(T data, BinaryTreeNode<T>.ChildType type, T insertData)
        {
            BinaryTreeNode<T> node = GetNode(data);
            if (node != null)
            {
                BinaryTreeNode<T> insert = new BinaryTreeNode<T>(insertData);
                if (type == BinaryTreeNode<T>.ChildType.Left)
                {
                    insert.LChild = node.LChild;
                    node.LChild = insert;
                }
                else
                {
                    insert.RChild = node.RChild;
                    node.RChild = insert;
                }
            }
        }

        public virtual void Delete(T data)
        {
            Delete(GetNode(data));
        }

        protected virtual void Delete(BinaryTreeNode<T> delNode)
        {
            if (delNode == null) return;
            if (delNode.LChild == null && delNode.RChild == null)
            {
                if (delNode.Parent == null)
                    root = null;
                else
                {
                    if (delNode.Parent.LChild == delNode) delNode.Parent.LChild = null;
                    else delNode.Parent.RChild = null;
                }
                return;
            }

            BinaryTreeNode<T> delParentNode = delNode.Parent;
            BinaryTreeNode<T> maxNode = delNode.LChild;

            if (maxNode == null)
            {
                maxNode = delNode.RChild;
            }
            else
            {
                if (maxNode.RChild != null)
                {
                    while (maxNode.RChild != null)
                    {
                        maxNode = maxNode.RChild;
                    }
                }

                if (maxNode.Parent.LChild == maxNode) maxNode.Parent.LChild = maxNode.LChild;
                else maxNode.Parent.RChild = maxNode.LChild;
            }

            maxNode.RChild = delNode.RChild;
            maxNode.LChild = delNode.LChild;

            if (delParentNode == null)
            {
                root = maxNode;
            }
            else
            {
                if (delParentNode.LChild == delNode) delParentNode.LChild = maxNode;
                else delParentNode.RChild = maxNode;
            }
        }

        #region Traverse

        private void PreOrderTraverse(BinaryTreeNode<T> node, VisitNode visit)
        {
            if (node != null)
            {
                visit(node);
                PreOrderTraverse(node.LChild, visit);
                PreOrderTraverse(node.RChild, visit);
            }
        }

        private void InOrderTraverse(BinaryTreeNode<T> node, VisitNode visit)
        {
            if (node != null)
            {
                InOrderTraverse(node.LChild, visit);
                visit(node);
                InOrderTraverse(node.RChild, visit);
            }
        }

        private void PostOrderTraverse(BinaryTreeNode<T> node, VisitNode visit)
        {
            if (node != null)
            {
                PostOrderTraverse(node.LChild, visit);
                PostOrderTraverse(node.RChild, visit);
                visit(node);
            }
        }

        private void LevelOrderTraverse(VisitNode visit)
        {
            Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();

            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                BinaryTreeNode<T> node = queue.Dequeue();
                visit(node);
                if (node.LChild != null) queue.Enqueue(node.LChild);
                if (node.RChild != null) queue.Enqueue(node.RChild);
            }
        }

        public void Traverse(VisitNode visit, TraverseType type)
        {
            switch (type)
            {
                case TraverseType.PreOrder: PreOrderTraverse(Root, visit); break;
                case TraverseType.InOrder: InOrderTraverse(Root, visit); break;
                case TraverseType.PostOrder: PostOrderTraverse(Root, visit); break;
                case TraverseType.LevelOrder: LevelOrderTraverse(visit); break;
            }
        }

        #endregion
    }
}
