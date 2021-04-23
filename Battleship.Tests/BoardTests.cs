using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Battleship.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Board_When_Positive_Dimensions_And_Fits_Ship_Size_Then_Create_Successfully()
        {
            int width = 1;
            int lenght = 1;
            int shipSize = 1;

            var board = new Board(width, lenght, shipSize, "name");

            Assert.IsInstanceOfType(board, typeof(Board));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Board_When_Negative_Dimensions_Throw_Exception()
        {
            int width = -1;
            int lenght = 1;
            int shipSize = 1;

            var board = new Board(width, lenght, shipSize, "name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Board_When_Ship_Doesnt_Fit_Board_Throw_Exception()
        {
            int width = 1;
            int lenght = 1;
            int shipSize = 2;

            var board = new Board(width, lenght, shipSize, "name");
        }

    }
}
