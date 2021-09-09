using System.Collections.Generic;

namespace BillsManagement.Tests.Tests
{
    public class TestCase
    {
        public TestCase(object inputData, object requiredCase, object expectedResult)
        {
            InputData = inputData;
            RequiredCases = requiredCase;
            ExpectedResult = expectedResult;
        }
        public object InputData { get; private set; }

        public object RequiredCases { get; private set; }

        public object ExpectedResult { get; private set; }
        public List<object[]> Params
        {
            get
            {
                return new List<object[]> {
                        new object[] {
                            InputData,
                            RequiredCases,
                            ExpectedResult}
                    };
            }
        }
    }
}
