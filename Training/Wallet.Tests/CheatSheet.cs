/*namespace Wallet.Tests;

public class CheatSheet
{
    /*
    STRINGS
#1#
    Assert.Equal(expectedString, actualString);
    Assert.StartsWith(expectedString, stringToCheck);
    Assert.EndsWith(expectedString, stringToCheck);

// Some can also take optional params
    Assert.Equal(expectedString, actualString, ignoreCase: true);
    Assert.StartsWith(expectedString, stringToCheck, StringComparison.OrdinalIgnoreCase);

/*
    COLLECTIONS
#1#
    Assert.Contains(expectedThing, collection);
// Overload method for contains
    Assert.Contains(collection, item => item.Contains(thingToCheck));
    Assert.DoesNotContain(expectedThing, collection);
    Assert.Empty(collection);
    Assert.All(collection, item => Assert.False(string.IsNullOrWhiteSpace(item)));

/*
    NUMBERS
#1#
    Assert.InRange(thingToCheck, lowRange, highRange);

/*
    EXCEPTIONS
#1#
    Assert.Throws<T>(() => sut.Method());

/*
    TYPES
#1#
    Assert.IsType<T>(thing);
    Assert.IsAssignableFrom<T>(thing);
    Assert.Same(obj1, obj2);
    Assert.NotSame(obj1, obj2);

/*
    COOL XUNIT STUFF
#1#

// Inherit from the DataAttribute from xunit.sdk
    public class CustomData : DataAttribute
    {
        // Needs a method that returns an IEnumerable<object[]>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { data1, data2, data3 };
            yield return new object[] { data4, data5, data6 };
        }
    }

    public class TestClass
    {
        [Theory]
        [CustomData]
        public void TestMethod(param1, param2, param3)
        {
            // Test using the 3 params
        }
    }

// Create the fixture to share
    public class TestSetup : IDisposable
    {
        // Some stuff here
    }

    public class TestClass : IClassFixture<TestSetup>
    {
        public TestClass(TestSetup setup)
        {
            // Initialise setup to share across test methods
        }
    }

// Create collection to share across test classes
    [CollectionDefinition("Name of Collection")]
    public class TestCollection : ICollectionFixture<TestSetup>
    {
    }

    [Collection("Name of Collection")]
    public class TestClass2
    {
        public TestClass2(TestSetup setup)
        {
            // Initialise setup
        }
    }
}*/