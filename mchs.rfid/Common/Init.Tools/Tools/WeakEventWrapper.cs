// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakEventWrapper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Обертка для слабого события
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;

    /// <summary>
    /// Обертка для слабого события
    /// </summary>
    /// <typeparam name="TSource">
    /// Тип источника
    /// </typeparam>
    /// <typeparam name="TListener">
    /// Тип прослушивателя
    /// </typeparam>
    /// <typeparam name="TAction">
    /// Тип события
    /// </typeparam>
    public class WeakEventWrapper<TSource, TListener, TAction>
        where TListener : class
        where TAction : class
    {
        /// <summary>
        /// Слабая ссылка на слушатель
        /// </summary>
        private readonly WeakReference _weakReference;

        /// <summary>
        /// Метод удаления подписки
        /// </summary>
        private readonly Action<TSource, TAction> _removeMethod;

        /// <summary>
        /// Источник события
        /// </summary>
        private readonly TSource _source;

        /// <summary>
        /// Обработчик события
        /// </summary>
        private readonly TAction _handler;

        /// <summary>
        /// Обертка для слабого события
        /// </summary>
        /// <param name="source">
        /// Источник события
        /// </param>
        /// <param name="listner">
        /// Слушатель события
        /// </param>
        /// <param name="addMethod">
        /// Метод посписки на событие
        /// </param>
        /// <param name="removeMethod">
        /// Метод удаления подписки
        /// </param>
        /// <param name="eventHandler">
        /// Forwarding метод
        /// </param>
        public WeakEventWrapper(
            TSource source,
            TListener listner,
            Action<TSource, TAction> addMethod,
            Action<TSource, TAction> removeMethod,
            Func<WeakEventWrapper<TSource, TListener, TAction>, TAction> eventHandler)
        {
            // Запоминаем источник
            this._source = source;

            // Запоминаем спрослушиватель через слабую ссылку
            this._weakReference = new WeakReference(listner);

            // Валидация методов (не должно быть захвата переменных)
            if (!addMethod.Method.IsStatic)
                throw new ArgumentException("addMethod is incorrect");

            if (!removeMethod.Method.IsStatic)
                throw new ArgumentException("removeMethod is incorrect");

            if (!eventHandler.Method.IsStatic)
                throw new ArgumentException("eventHandler is incorrect");

            this._removeMethod = removeMethod;

            // получаем обработчик события
            this._handler = eventHandler(this);

            // подписываемся на событие
            addMethod(this._source, this._handler);
        }

        /// <summary>
        /// Прослушиватель
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Может возвращать null, если объект уже удален GC
        /// </exception>
        public TListener Listener
        {
            get
            {
                // если прослушивателя уже нет, то удаляем подписку
                if (!this._weakReference.IsAlive)
                    this.Deregiser();

                return this._weakReference.Target as TListener;
            }
        }

        /// <summary>
        /// Удаление подписки, по возсожности вызывать в финализаторе TListener
        /// </summary>
        public void Deregiser()
        {
            if (this._removeMethod != null)
                this._removeMethod(this._source, this._handler);
        }
    }
}
