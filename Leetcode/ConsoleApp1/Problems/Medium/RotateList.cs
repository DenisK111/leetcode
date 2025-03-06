using Leeetcode.Problems.Easy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 61
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null!)
        {
            this.val = val;
            this.next = next;
        }

        public static ListNode Create(int val = 0, ListNode next = null!) => new(val, next);
    }

    internal class RotateList
    {
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null) return head!;
            var dictionary = new Dictionary<int, ListNode>(); // key -> position, value -> node

            int startPosition = 1;
            dictionary[startPosition++] = head;

            while(head.next != null)
            {
                head = head.next;
                dictionary[startPosition++] = head;                
            }

            var rotateNode = dictionary[startPosition - 1 - k % dictionary.Count];
            var lastNode = dictionary[startPosition -1];
            lastNode.next = dictionary[1];
            var newHead = rotateNode.next;
            rotateNode.next = null!;
            return newHead;         
        }
    }

    public class RotateListTests()
    {
        [Test]
        public void Test()
        {
            // Arrange
            var node = ListNode.Create(0);
            var node1 = ListNode.Create(1);
            var node2 = ListNode.Create(2);
            node.next = node1;
            node1.next = node2;
            var k = 4;

            //Act

            var result = new RotateList().RotateRight(node, k);

            //Assert

            result.ShouldBeEquivalentTo(node2);
            result.next.ShouldBeEquivalentTo(node);            
            result.next.next.ShouldBeEquivalentTo(node1);
            result.next.next.next.ShouldBe(null);            
        }
    }
}
