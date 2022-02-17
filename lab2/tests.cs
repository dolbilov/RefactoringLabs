using System;
using NuGet.Frameworks;
using NUnit.Framework;
using PPPP2_Sharp;

namespace ProjectTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestInfiniteSolutions()
        {
            var ans = Solver.SolveSystem(0, 0, 0, 0, 0, 0);
            Assert.AreEqual("5", ans.ToString());
        }

        [Test]
        public void TestConcreteSolutions()
        {
            var ans = Solver.SolveSystem(1, 2, 3, 4, 5, 6);
            Assert.AreEqual("2 -4 4.5", ans.ToString());

            ans = Solver.SolveSystem(1, 0, 2, 2, 0, 4);
            Assert.AreEqual("2 0 2", ans.ToString());

            ans = Solver.SolveSystem(-1, -1, 1, 0, 2, 0);
            Assert.AreEqual("2 0 -2", ans.ToString());
        }

        [Test]
        public void TestNoSolutions()
        {
            var ans = Solver.SolveSystem(1, 2, 2, 4, 5, 5);
            Assert.AreEqual("0", ans.ToString());
        }

        [Test]
        public void TestInfinityLineSolutions()
        {
            var ans = Solver.SolveSystem(1, 2, 1, 2, 5, 5);
            Assert.AreEqual("1 -0.5 2.5", ans.ToString());

            ans = Solver.SolveSystem(1, 1, 0, 0, 1, 0);
            Assert.AreEqual("1 -1 1", ans.ToString());

            ans = Solver.SolveSystem(0, 0, 1, 1, 0, 1);
            Assert.AreEqual("1 -1 1", ans.ToString());

            ans = Solver.SolveSystem(0, 0, 1, 1, 0, 1);
            Assert.AreEqual("1 -1 1", ans.ToString());

            ans = Solver.SolveSystem(1, -1, 0, 1, 0, 0);
            Assert.AreEqual("1 1 0", ans.ToString());

            ans = Solver.SolveSystem(0, 1, 1, 1, 0, 0);
            Assert.AreEqual("1 -1 0", ans.ToString());
        }

        [Test]
        public void TestInfinitySolutionsWithConcreteY()
        {
            var ans = Solver.SolveSystem(0, 1, 0, 1, 5, 5);
            Assert.AreEqual("4 5", ans.ToString());

            ans = Solver.SolveSystem(0, 0, 0, 2, 0, 2);
            Assert.AreEqual("4 1", ans.ToString());

            ans = Solver.SolveSystem(0, 4, 0, 0, 4, 0);
            Assert.AreEqual("4 1", ans.ToString());
            ans = Solver.SolveSystem(0, 0, 0, 4, 0, 0);
            Assert.AreEqual("4 0", ans.ToString());
        }

        [Test]
        public void TestInfinitySolutionsWithConcreteX()
        {
            var ans = Solver.SolveSystem(1, 0, 1, 0, 3, 3);
            Assert.AreEqual("3 3", ans.ToString());

            ans = Solver.SolveSystem(0, 0, 2, 0, 0, 2);
            Assert.AreEqual("3 1", ans.ToString());

            ans = Solver.SolveSystem(1, 0, 0, 0, 0, 0);
            Assert.AreEqual("3 0", ans.ToString());

            ans = Solver.SolveSystem(2, 0, 0, 0, 2, 0);
            Assert.AreEqual("3 1", ans.ToString());

            ans = Solver.SolveSystem(-1, 0, 1, 0, 0, 0);
            Assert.AreEqual("3 0", ans.ToString());
        }

        [Test]
        public void TestLastElse()
        {
            try
            {
                var ans = Solver.SolveSystem(1, 1, 1, -1, 0, 0);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Wrong program logic")
                {
                    Assert.IsTrue(true);
                    return;
                }
            }

            Assert.IsTrue(false);
        }
    }
}