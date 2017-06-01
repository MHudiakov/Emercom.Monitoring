// -----------------------------------------------------------------------
// <copyright file="MockExtAttribute.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.DbCore.Test.Mocks.Attributes
{
    using System.Collections.Generic;

    using Init.DbCore.Processing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MockExtAttribute : ExtProcessAttribute
    {
        public MockExtAttribute(string propertyName)
            : this(propertyName, new Dictionary<string, object>())
        {
            
        }

        public MockExtAttribute(string propertyName, Dictionary<string, object> args)
            : base(propertyName, args)
        {

        }
    }
}
