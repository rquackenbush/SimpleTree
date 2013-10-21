using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTree
{
    /// <summary>
    /// Helper methods for dealing with trees and nodes.
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <remarks> 
        /// From StackOverflow:
        /// http://stackoverflow.com/a/1815600/232566
        /// </remarks>
        /// <returns></returns>
        public static IEnumerable<T> GetItems<T>(this IEnumerable<T> collection, Func<T, IEnumerable> selector)
        {
            Stack<IEnumerable<T>> stack = new Stack<IEnumerable<T>>();
            stack.Push(collection.OfType<T>());

            while (stack.Count > 0)
            {
                IEnumerable<T> items = stack.Pop();
                foreach (var item in items)
                {
                    yield return item;

                    IEnumerable<T> children = selector(item).OfType<T>();
                    stack.Push(children);
                }
            }
        }

        /// <summary>
        /// Gets all of the items in this branch in leaf to root order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetItemLineage<T>(this TreeNode<T> node) 
        {
            if (node == null)    
                throw new ArgumentNullException("node");

            var currentNode = node;

            while (currentNode != null)
            {
                var item = currentNode.Item;

                //Move up the parent
                currentNode = currentNode.Parent;

                yield return item;
            }
        }

        /// <summary>
        /// Gets all of the nodes in this branch in leaf to root order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode<T>> GetNodeLineage<T>(this TreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var currentNode = node;

            while (currentNode != null)
            {
                var nodeToReturn = currentNode;

                //Move up the parent
                currentNode = currentNode.Parent;

                yield return nodeToReturn;
            }
        }

        /// <summary>
        /// Flattens out all of the items in a tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<TreeNode<T>> nodes)
        {
            return nodes.GetItems(n => n.Nodes).Select(n => n.Item);
        }

        /// <summary>
        /// Takes the items out of the tree and returns them as a flat list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootItems"></param>
        /// <param name="allItems"></param>
        /// <param name="childSelector"></param>
        /// <returns></returns>
        public static Tree<T> CreateTree<T>(IEnumerable<T> rootItems, IEnumerable<T> allItems, Func<T, T, bool> childSelector )
        {
            var tree = new Tree<T>();

            AddItemsToTree(rootItems, allItems, tree.Nodes, childSelector);

            return tree;
        }

        /// <summary>
        /// Creates a tree from a flat list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allItems"></param>
        /// <param name="rootSelector"></param>
        /// <param name="childSelector"></param>
        /// <returns></returns>
        public static Tree<T> CreateTree<T>(this IEnumerable<T> allItems, Func<T, bool> rootSelector,
            Func<T, T, bool> childSelector)
        {
            var tree = new Tree<T>();

            var rootItems = allItems.Where(rootSelector);

            return CreateTree(rootItems, allItems, childSelector);
        }

        /// <summary>
        /// Recursive method to build a tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="allItems"></param>
        /// <param name="nodes"></param>
        /// <param name="childSelector"></param>
        private static void AddItemsToTree<T>(IEnumerable<T> items, IEnumerable<T> allItems, TreeNodes<T> nodes, Func<T, T, bool> childSelector)
        {
            foreach (var item in items)
            {
                var childNode = new TreeNode<T>(item);

                AddItemsToTree<T>(allItems.Where(i => childSelector(item, i)), allItems, childNode.Nodes, childSelector);

                nodes.Add(childNode);
            }
        }
    }
}
