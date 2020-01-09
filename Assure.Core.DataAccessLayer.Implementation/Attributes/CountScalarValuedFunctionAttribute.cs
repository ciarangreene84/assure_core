using System;

namespace Assure.Core.DataAccessLayer.Implementation.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class CountScalarValuedFunctionAttribute : Attribute
    {
        internal string FunctionName { get; private set; }

        internal CountScalarValuedFunctionAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}
