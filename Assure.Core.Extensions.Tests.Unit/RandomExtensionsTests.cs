using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Assure.Core.Extensions.Tests.Unit.System
{
    public class RandomExtensionsTests
    {
        private readonly Random _random;
        public RandomExtensionsTests()
        {
            _random = new Random();
        }

        [Fact]
        public void NextBool_True()
        {
            var result = _random.NextBool(1.0);
            Assert.True(result);
        }

        [Fact]
        public void NextBool_False()
        {
            var result = _random.NextBool(0.0);
            Assert.False(result);
        }

        [Fact]
        public void NextT_Empty()
        {
            var list = new List<string>();
            var result = _random.Next<string>(list);
            Assert.Null(result);
        }
        
        [Fact]
        public void NextT_OneItem()
        {
            var list = new List<string>();
            var item = "test";
            list.Add(item);
            var result = _random.Next<string>(list);
            Assert.Equal(item, result);
        }

        [Fact]
        public void NextT_OneItem_Thrice()
        {
            var list = new List<string>();
            var item = "test";
            list.Add(item);
            var result = _random.Next<string>(list, 3).ToList();
            Assert.Equal(3, result.Count);
        }
    }
}
