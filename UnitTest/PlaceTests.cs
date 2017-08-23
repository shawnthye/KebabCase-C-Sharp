using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KebabCase;

namespace UnitTest {
    [TestClass]
    public class PlaceTests {
        [TestMethod]
        public void PascalCase() {
            var result = "ParkCity".ToKebabCase();
            Assert.AreEqual("park-city", result);
        }

        [TestMethod]
        public void Number() {
            var result = "SS2".ToKebabCase();
            Assert.AreEqual("ss-2", result);
        }

        [TestMethod]
        public void ApostrophesS() {
            var result = "Abcd's".ToKebabCase();
            Assert.AreEqual("abcds", result);
        }
    }
}
