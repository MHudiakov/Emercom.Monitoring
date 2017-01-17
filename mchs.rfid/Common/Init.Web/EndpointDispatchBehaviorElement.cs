// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointDispatchBehaviorElement.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение разметки для декларативного добавления EnpointDispathcherBehavior
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web
{
    using System;
    using System.ServiceModel.Configuration;

    /// <summary>
    /// Расширение разметки для декларативного добавления EnpointDispathcherBehavior
    /// </summary>
    public class EndpointDispatchBehaviorElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            var inspector = new OperationParameterInspector();
            var handler = new DispatcherErrorHandler();
            return new EnpointDispathcherBehavior(inspector, handler);
        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/>.
        /// </returns>
        public override Type BehaviorType
        {
            get
            {
                return typeof(EnpointDispathcherBehavior);
            }
        }
    }
}