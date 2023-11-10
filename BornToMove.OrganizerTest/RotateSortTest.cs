using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTest
    {
        [Test]
        public void testSortEmpty()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(0).Items);
            Assert.That(result, Is.EquivalentTo(Array.Empty<int>()));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(Array.Empty<int>()));
        }

        [Test]
        public void testSortOneElement()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 7 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(1).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 7 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 7 }));
        }

        [Test]
        public void testSortTwoElements()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 32, 3 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(2).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 3, 32 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 32, 3 }));
        }

        [Test]
        public void testSortThreeEqual()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 4, 4, 4 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 4, 4, 4 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 4, 4, 4 }));
        }

        [Test]
        public void testSortUnsortedArray()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 3, 1, 2 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 1, 2, 3 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 3, 1, 2 }));
        }
    }
}