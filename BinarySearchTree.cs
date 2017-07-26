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

        public void Insert(T data)
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
        }

        public void Delete(T data)
        {
            BinaryTreeNode<T> delNode = GetNode(data);
            if (delNode == null) return;

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
    }
}
