// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavProperty.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Реализация навигационного свойства
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace com.Contact24.DbCore.Repository
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Реализация навигационного свойства
    /// </summary>
    /// <typeparam name="TParent">
    /// тип родителького объекта
    /// </typeparam>
    /// <typeparam name="TOwner">
    /// тип дочернего объекта (владельца всойства)
    /// </typeparam>
    /// <typeparam name="TKey">
    /// тип ключа родительского объекта объекта
    /// </typeparam>
    public class NavProperty<TParent, TOwner, TKey>
        where TParent : class
        where TOwner : class
    {
        /// <summary>
        /// метод полчения родителького объекта по идентификатору
        /// </summary>
        private readonly Func<TKey, TParent> _getParent;

        /// <summary>
        /// селектор ключа родительского объекта
        /// </summary>
        private readonly Func<TParent, TKey> _parentKeySelector;

        /// <summary>
        /// дочерний объект (владелец свойства)
        /// </summary>
        private readonly TOwner _owner;

        /// <summary>
        /// Метод чтения значения свойства-идентфикатора родительского объекта
        /// </summary>
        private readonly Func<TOwner, TKey> _parentPropertyGetter;

        /// <summary>
        /// Метод установки значения свойства-идентфикатора родительского объекта
        /// </summary>
        private readonly Action<TOwner, TKey> _parentPropertySetter;

        /// <summary>
        /// Реализация навигационного свойства
        /// </summary>
        /// <param name="owner">дочерний объект (владелец свойства)</param>
        /// <param name="parentPropertySelector">селектор свойства дочернего эелемента обозначающего ключ родительского элемента</param>
        /// <param name="getParent">метод получения родителького объекта</param>
        /// <param name="parentKeySelector">селектор ключа родительского объекта (по которому привязан дочерний)</param>
        public NavProperty(TOwner owner, Expression<Func<object, TKey>> parentPropertySelector, Func<TKey, TParent> getParent, Func<TParent, TKey> parentKeySelector)
        {
            if (getParent == null)
                throw new ArgumentNullException("getParent");
            if (parentKeySelector == null)
                throw new ArgumentNullException("parentKeySelector");
            if (parentPropertySelector == null)
                throw new ArgumentNullException("parentPropertySelector");
            if (owner == null)
                throw new ArgumentNullException("owner");

            this._getParent = getParent;
            this._parentKeySelector = parentKeySelector;
            this._owner = owner;

            var member = (PropertyInfo)((MemberExpression)parentPropertySelector.Body).Member;

            // generate _parentPropertyGetter
            var getMethod = new DynamicMethod("parentGetter", typeof(TKey), new[] { typeof(TOwner) });
            getMethod.DefineParameter(1, ParameterAttributes.None, "owner");
            var getGenerator = getMethod.GetILGenerator();
            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.EmitCall(OpCodes.Callvirt, member.GetGetMethod(), null);
            getGenerator.Emit(OpCodes.Ret);

            _parentPropertyGetter = (Func<TOwner, TKey>)getMethod.CreateDelegate(typeof(Func<TOwner, TKey>));

            // generate _parentPropertySetter
            var setMethod = new DynamicMethod("parentSetter", typeof(void), new[] { typeof(TOwner), typeof(TKey) });
            setMethod.DefineParameter(1, ParameterAttributes.None, "owner");
            setMethod.DefineParameter(2, ParameterAttributes.None, "key");
            var setGenerator = setMethod.GetILGenerator();
            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            setGenerator.EmitCall(OpCodes.Callvirt, member.GetSetMethod(), null);
            setGenerator.Emit(OpCodes.Ret);

            _parentPropertySetter = (Action<TOwner, TKey>)setMethod.CreateDelegate(typeof(Action<TOwner, TKey>));
        }

        /// <summary>
        /// сохраненная ссылка на родительский объект
        /// </summary>
        private TParent _value;

        /// <summary>
        /// Ссылка на родительский объект
        /// </summary>
        public TParent Value
        {
            get
            {
                // если  значение ключа с енилось, обнуляем ссылку
                if (_value != null && !_parentKeySelector(_value).Equals(_parentPropertyGetter(_owner)))
                    _value = null;

                // если ссылка пустая и значения ключа не является значением по умолчанию, загружаем ссылку
                if (_value == null && !_parentPropertyGetter(_owner).Equals(default(TKey)))
                    _value = _getParent(_parentPropertyGetter(_owner));

                return _value;
            }

            set
            {
                // если устанавливают ссылку на null, заполняем ключ значением по умолчанию
                this._parentPropertySetter(this._owner, value == null ? default(TKey) : this._parentKeySelector(value));

                _value = value;
            }
        }
    }
}