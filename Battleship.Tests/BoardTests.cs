using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Battleship.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Board_When_Positive_Dimensions_Create_Successfully()
        {
            int width = 1;
            int lenght = 1;

            var board = new Board(width, lenght);

            Assert.IsInstanceOfType(board, typeof(Board));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Board_When_Negative_Dimensions_Throw_Exception()
        {
            int width = -1;
            int lenght = 1;

            var board = new Board(width, lenght);
        }
    }
}
