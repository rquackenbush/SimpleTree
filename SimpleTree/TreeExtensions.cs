using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CaptiveAire.SimpleTree
{
    /// <summary>
    /// Helper methods for dealing with trees and nodes.
    /// </summary>
    public static class TreeExtensions
    {

        /// <summary>
        /// Performs an action on each item in the tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this Tree<T> tree, Action<TreeNode<T>> action)
        {
            foreach (var node in tree.Nodes.GetItems(n => n.Nodes))
            {
                action(node);
            }
        }

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
                throw new ArgumentNullException(nameof(node));

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

        /// <summary>
        /// Creates a tree from a hierarchy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootItems"></param>
        /// <param name="childSelector"></param>
        /// <returns></returns>
        public static Tree<T> CreateTreeFromHierarchy<T>(this IEnumerable<T> rootItems, Func<T, IEnumerable<T>> childSelector)
        {
            var tree = new Tree<T>();

            AddItemsToTreeFromHierarchy(tree.Nodes, rootItems, childSelector);

            return tree;
        }

        private static void AddItemsToTreeFromHierarchy<T>(TreeNodes<T> nodes, IEnumerable<T> items,
                                             Func<T, IEnumerable<T>> childSelector)
        {
            foreach (var item in items)
            {
                var node = new TreeNode<T>(item);

                nodes.Add(node);

                var childItems = childSelector(item);

                AddItemsToTreeFromHierarchy<T>(node.Nodes, childItems, childSelector);
            }
        }

        /// <summary>
        /// Transforms a tree from one type to another
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="inTree"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Tree<TOut> TransformTree<TIn, TOut>(this Tree<TIn> inTree, Func<TIn, TOut> transform)
        {
            var outTree = new Tree<TOut>();

            TransformNodes(inTree.Nodes, outTree.Nodes, transform);

            return outTree;
        }

        private static void TransformNodes<TIn, TOut>(TreeNodes<TIn> inNodes, TreeNodes<TOut> outNodes, Func<TIn, TOut> transform)
        {
            foreach (var inNode in inNodes)
            {
                var transFormedItem = transform(inNode.Item);

                var outNode = new TreeNode<TOut>(transFormedItem);

                outNodes.Add(outNode);

                TransformNodes(inNode.Nodes, outNode.Nodes, transform);

            }
        }
    }
}
