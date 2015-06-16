using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace KataSecretSanta.Tests
{
    [TestClass]
    public class ExtensionText
    {
        [TestMethod]
        public void TestSwap()
        {
            var list = new List<string>{"a", "b", "c"};
            list.Swap(0, 2);
            list[0].ShouldBe("c");
            list[2].ShouldBe("a");
        }

        [TestMethod]
        public void TestShuffle()
        {
            var list = new List<string> { "a", "b", "c", "d" };
            list.Shuffle();
            Trace.TraceInformation(String.Join(",", list));
        }

        [TestMethod]
        public void TestToIndexDictionary()
        {
            var list = new List<string> { "a", "b", "c", "d" };
            var dict = list.ToIndexDictionary();
            dict["a"].ShouldBe(0);
            dict["b"].ShouldBe(1);
            dict["c"].ShouldBe(2);
            dict["d"].ShouldBe(3);
        }
    }
}
