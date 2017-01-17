// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationParameterInspector.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Инспектор параметров. Для логирования вызовов функций.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web
{
    using System;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Инспектор параметров. Для логирования вызовов функций.
    /// </summary>
    public class OperationParameterInspector : IParameterInspector
    {
        /// <summary>
        /// Событие, наступающее после завершения метода
        /// </summary>
        public event Action<string, object[], object, TimeSpan> OnMethodEnd;

        /// <summary>
        /// Событие, наступающее по началу метода
        /// </summary>
        public event Action<string, object[]> OnMethodBegin;

        /// <summary>
        /// Реализует callback после начала вызова метода
        /// </summary>
        /// <param name="operationName">
        /// Название операции
        /// </param>
        /// <param name="outputs">
        /// Выходные данные
        /// </param>
        /// <param name="returnValue">
        /// Возвращаемое значение
        /// </param>
        /// <param name="correlationState">
        /// Состояние корреляции 
        /// </param>
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            if (this.OnMethodEnd != null)
                this.OnMethodEnd(operationName, outputs, returnValue, DateTime.Now - (DateTime)correlationState);
        }

        /// <summary>
        /// Реализует callback перед началом вызова метода
        /// </summary>
        /// <param name="operationName">
        /// Название операции
        /// </param>
        /// <param name="inputs">
        /// Входные данные
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object BeforeCall(string operationName, object[] inputs)
        {
            if (this.OnMethodBegin != null)
                this.OnMethodBegin(operationName, inputs);
            return DateTime.Now;
        }
    }
}
