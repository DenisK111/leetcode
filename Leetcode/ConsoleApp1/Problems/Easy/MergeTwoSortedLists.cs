using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    /// <summary>
    /// 21
    /// </summary>
    /// 

   
  public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null!)
        {
            this.val = val;
            this.next = next;
        } 
    }

    internal class MergeTwoSortedLists
    {
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {           

            if (list1 is null) return list2;
            if (list2 is null) return list1;

            var (smaller,larger) = list1.val < list2.val ? (list1,list2) : (list2, list1);

            var start = smaller;

            while (larger != null) 
            {
                if (smaller.next is null || larger.val <= smaller.next.val)
                {
                    var temp = smaller.next;
                    smaller.next = larger;
                    var temp2 = larger.next;
                    larger.next = temp!;
                    larger = temp2;
                    smaller = smaller.next;
                }
                else
                {
                    smaller = smaller.next;
                }
            
            }

            return start;            
        }

        [TestCase(new int[] {  - 10, -10, -9, -4, 1, 6, 6}, new int[] { -7 }, new int[] { -10, -10, -9, -7, -4, 1, 6, 6 })]
        [TestCase(new int[] { 1, 2, 4 }, new int[] { 1, 3, 4 }, new int[] { 1, 1, 2, 3, 4, 4 })]
        [TestCase(new int[] {  }, new int[] {  }, new int[] {  })]
        [TestCase(new int[] {  }, new int[] { 0 }, new int[] { 0 })]
        public void Test(int[] list1, int[] list2, int[] expectedRestult)
        {
            // Arrange

            var list1Nodes = list1.Select(num => new ListNode(num)).ToList();
            var list2Nodes = list2.Select(num => new ListNode(num)).ToList();
            var temp = list1Nodes.FirstOrDefault();

            foreach (var item in list1Nodes.Skip(1)) 
            {
                temp!.next = item;
                temp = item;
            }

            var temp2 = list2Nodes.FirstOrDefault();

            foreach (var item in list2Nodes.Skip(1))
            {
                temp2!.next = item;
                temp2 = item;
            }

            var resultArr = new List<int>();

            //Act

            var result = MergeTwoLists(list1Nodes.FirstOrDefault()!, list2Nodes.FirstOrDefault()!);            

            while(result != null)
            {
                resultArr.Add(result.val);
                result = result.next;
            }

            //Assert

            resultArr.ToArray().ShouldBeEquivalentTo(expectedRestult);
        }

        
    }


}
