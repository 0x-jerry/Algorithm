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
            Left,
            Right,
            None
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
            int lDepth = LChild == null ? 0 : LChild.GetTreeDepth();
            int rDepth = RChild == null ? 0 : RChild.GetTreeDepth();

            return (lDepth > rDepth ? lDepth : rDepth) + 1;
        }

        public int GetNodeNumber()
        {
            int num = 1;

            if (LChild != null) num += LChild.GetNodeNumber();
            if (RChild != null) num += RChild.GetNodeNumber();

            return num;
        }

        public BinaryTreeNode<T> GetSibing(ChildType type)
        {
            BinaryTreeNode<T> sibing = null;

            if (Parent != null) sibing = type == ChildType.Left ? Parent.LChild : Parent.RChild;

            return this == sibing ? null : sibing;
        }
    }
}
