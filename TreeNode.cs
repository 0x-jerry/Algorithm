using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class BinaryTreeNode<T>
    {
        public enum ChildType
        {
            Left, Right
        }

        private BinaryTreeNode<T> parent;
        private BinaryTreeNode<T> lChild;
        private BinaryTreeNode<T> rChild;

        public T Data;

        public BinaryTreeNode<T> LChild
        {
            get => lChild;
            set
            {
                lChild = value;
                if (value != null) value.parent = this;
            }
        }

        public BinaryTreeNode<T> RChild
        {
            get => rChild;
            set
            {
                rChild = value;
                if (value != null) value.parent = this;
            }
        }

        public BinaryTreeNode<T> Parent { get => parent; }

        public BinaryTreeNode()
        {
            LChild = null;
            RChild = null;
            Data = default(T);
        }

        public BinaryTreeNode(T data)
        {
            LChild = null;
            RChild = null;
            this.Data = data;
        }

        public BinaryTreeNode(BinaryTreeNode<T> lChild, BinaryTreeNode<T> rChild, T data)
        {
            LChild = lChild;
            RChild = rChild;
            Data = data;
        }

        public BinaryTreeNode(BinaryTreeNode<T> parent, BinaryTreeNode<T> lChild, BinaryTreeNode<T> rChild, T data)
        {
            this.parent = parent;
            LChild = lChild;
            RChild = rChild;
            Data = data;
        }

        public int GetTreeDepth()
        {
            int lDepth = 0;
            int rDepth = 0;

            lDepth = LChild == null ? 0 : LChild.GetTreeDepth();

            rDepth = RChild == null ? 0 : RChild.GetTreeDepth();

            return lDepth > rDepth ? lDepth + 1 : rDepth + 1;
        }

        public BinaryTreeNode<T> GetSibing(ChildType type)
        {
            if (Parent == null || this == (type == ChildType.Left ? Parent.LChild : Parent.RChild))
                return null;

            return type == ChildType.Left ? Parent.lChild : Parent.RChild;
        }
    }

    public class BinaryTree<T>
    {
        public enum TraverseType
        {
            PreOrder,
            InOrder,
            PostOrder,
            LevelOrder
        }

        public delegate void VisitEveryNode(BinaryTreeNode<T> node);

        private BinaryTreeNode<T> root;

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

        public BinaryTreeNode<T> GetNode(T data)
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

        private void PreOrderTraverse(BinaryTreeNode<T> node, VisitEveryNode visit)
        {
            if (node != null)
            {
                visit(node);
                PreOrderTraverse(node.LChild, visit);
                PreOrderTraverse(node.RChild, visit);
            }
        }
        
        private void InOrderTraverse(BinaryTreeNode<T> node, VisitEveryNode visit)
        {
            if (node != null)
            {
                InOrderTraverse(node.LChild, visit);
                visit(node);
                InOrderTraverse(node.RChild, visit);
            }
        }
        
        private void PostOrderTraverse(BinaryTreeNode<T> node, VisitEveryNode visit)
        {
            if (node != null)
            {
                PostOrderTraverse(node.LChild, visit);
                PostOrderTraverse(node.RChild, visit);
                visit(node);
            }
        }
        
        private void LevelOrderTraverse(VisitEveryNode visit)
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

        public void Traverse(VisitEveryNode visit, TraverseType type)
        {
            switch (type)
            {
                case TraverseType.PreOrder: PreOrderTraverse(Root, visit); break;
                case TraverseType.InOrder: InOrderTraverse(Root, visit); break;
                case TraverseType.PostOrder: PostOrderTraverse(Root, visit); break;
                case TraverseType.LevelOrder: LevelOrderTraverse(visit); break;
            }
        }
    }
}
