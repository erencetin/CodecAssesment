using Codec.WallE.Logic;
using NUnit.Framework;
using System;

namespace Codec.WallE.Tests
{
    public class NavigationTests
    {
        [TestCase("5x5", "FFRFLFLF", "1,4,West")]
        [TestCase("5x5", "RFFFLFFRRL", "4,3,East")]
        [TestCase("1x1", "FFFLLFF", "1,1,South")]
        [TestCase("8x3", "FFRFFFFFFFRFFFRFFFFFFFR", "1,1,North")]
        public void RunShouldProperResultWhenInputIsValid(string gridSize, string command, string expectedResult)
        {
            Navigation navigation = new Navigation(gridSize, command);
            var result = navigation.Run();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("","FF")]
        [TestCase("4x4", null)]
        public void RunShouldThrowArgumentNullExceptionWhenInputIsEmptyString(string gridSize, string command)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Navigation(gridSize,command));

        }

        [TestCase("VeryBigGrid", "FF")]
        [TestCase("4xxx", "LLR")]
        [TestCase("4x", "LLR")]
        [TestCase("4x5", "LLRX")]
        public void RunShouldThrowArgumentExceptionWhenInputIsNotProper(string gridSize, string command)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Navigation(gridSize, command));

        }

    }
}