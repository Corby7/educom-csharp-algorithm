using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTest
    {
        [Test]
        public void testSortTwoElements()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 32, 3 };
            IComparer<int> comparer = new Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Has.Exactly(2).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 3, 32 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 32, 3 }));
        }
    }
}
