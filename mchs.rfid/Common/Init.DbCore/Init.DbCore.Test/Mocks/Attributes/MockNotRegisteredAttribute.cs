// -----------------------------------------------------------------------
// <copyright file="MockProcessAttribute.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.DbCore.Test.Mocks.Attributes
{
    using System;
    using System.Collections.Generic;

    using Init.DbCore.Processing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class MockNotRegisteredAttribute : ProcessAttribute
    {
        public MockNotRegisteredAttribute()
            : this( new Dictionary<string, object>())
        {}

        public MockNotRegisteredAttribute(Dictionary<string, object> args)
            : base(args)
        {}
    }
}
