using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KebabCase;

namespace UnitTest {
    [TestClass]
    public class FooBarTests {
        private const string Expected = "foo-bar";

        [TestMethod]
        public void Space() {
            string aaa = null;
            var result = "Foo Bar".ToKebabCase();
            var a = aaa.ToKebabCase();
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void CamelCase() {
            var result = "fooBar".ToKebabCase();
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void Underscore() {
            var result = "__FOO_BAR__".ToKebabCase();
            Assert.AreEqual(Expected, result);
        }
    }
}
