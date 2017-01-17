// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базоый класс объекта БД. релизует логику копирования/клонирования, получения ключа, сравнения
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;

    using Init.DbCore.Metadata;
    using Init.Tools;

    /// <summary>
    /// Базоый класс объекта БД. релизует логику копирования/клонирования, получения ключа, сравнения
    /// </summary>
    [DataContract]
    public class DbObject : IEquatable<DbObject>, ICloneable
    {
        /// <summary>
        /// Кеш метаданных DbObject
        /// </summary>
        private static readonly Dictionary<Type, DbObjectMetadata> s_metadata = new Dictionary<Type, DbObjectMetadata>();

        /// <summary>
        /// Возвращает экземпляр метаданных для объекта
        /// </summary>
        /// <returns>DbObjectMetadata объекта</returns>
        private DbObjectMetadata GetMetadata()
        {
            DbObjectMetadata metadata;
            var type = this.GetType();
            s_metadata.TryGetValue(type, out metadata);
            if (metadata == null)
                lock (s_metadata)
                {
                    s_metadata.TryGetValue(type, out metadata);
                    if (metadata == null)
                    {
                        metadata = new DbObjectMetadata(type);
                        s_metadata.Add(type, metadata);
                    }
                }

            return metadata;
        }

        /// <summary>
        /// Выполняем инцыиализацию приватных свойств при десериализации объекта
        /// </summary>
        /// <param name="context">Контекст десериализации</param>
        [OnDeserialized]
        private void OnDeserialised(StreamingContext context)
        {
            this.InitCore();
        }

        /// <summary>
        /// Выполняем инициализацию приватных свойств при десериализации объекта
        /// </summary>
        private void InitCore()
        {
            var metadata = this.GetMetadata();

            this.Properties = metadata.Properties;
            this.Identity = metadata.Identity;
            this.Key = metadata.Key;
        }

        /// <summary>
        /// Свойство, отмеченное атрибутом <see cref="DbIdentityAttribute"/>
        /// </summary>
        public DbPropertyInfo Identity { get; private set; }

        /// <summary>
        /// Значение поля Identity
        /// </summary>
        public object IdentityValue
        {
            get
            {
                if (Identity == null)
                    throw new NotSupportedException(string.Format("Объект {0} не имеет автогенерируемого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", this.GetType().FullName));
                return Identity.Getter(this);
            }
        }

        /// <summary>
        /// Свойства, отмеченные атрубитом <see cref="DbKeyAttribute"/>
        /// </summary>
        public DbPropertyInfo Key { get; private set; }

        /// <summary>
        /// Свойства, отмеченные атрубитом <see cref="DataMemberAttribute"/>. Включают в себя коллекцию ключей
        /// </summary>
        public ReadOnlyDictionaryWrapper<string, DbPropertyInfo> Properties { get; private set; }

        /// <summary>
        /// Базоый класс объекта БД. релизует логику копирования/клонирования, получения ключа, сравнения
        /// </summary>
        public DbObject()
        {
            this.InitCore();
        }

        /// <summary>
        /// Вовзвращает значение ключа объекта.
        /// </summary>
        public object KeyValue
        {
            get
            {
                if (Key == null)
                    throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", this.GetType().FullName));
                return Key.Getter(this);
            }
        }

        /// <summary>
        /// Возвращает коллекцию значений всех полей объекта
        /// </summary>
        /// <returns>Коллекция значений всех полей объекта вида [название поля]=>[значение]</returns>
        public Dictionary<string, object> GetProperties()
        {
            return this.Properties
                .ToDictionary(pair => pair.Key, pair => pair.Value.Getter(this))
                .OrderBy(e => e.Key)
                .ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Выполняет сравнение ключей текущего объекта с ключами другого объекта
        /// </summary>
        /// <param name="targetKey">
        /// Набор ключей другого объекта
        /// </param>
        /// <returns>
        /// True, если последовательности ключей в точности совпадают
        /// </returns>
        public bool KeyEquals(object targetKey)
        {
            if (targetKey == null)
                throw new ArgumentNullException("targetKey");

            // если ключи не инициализированы
            // то мы не можем достоверно утверждать, что это копии одного и того же объекта
            // т.к. это станет известно только после присвоения каждой копии 
            // ключей их объединяющих
            if (!this.IsKeysInitialised())
                return false;

            return this.KeyValue.Equals(targetKey);
        }

        /// <summary>
        /// Выполняет сравнение объекта по указанному набору свойств
        /// </summary>
        /// <param name="targetProps">Набор свойств для стравнения</param>
        /// <returns>True, если свойства совпадают</returns>
        public bool PropEqualas(Dictionary<string, object> targetProps)
        {
            var sourceProps = this.GetProperties();
            foreach (var property in targetProps)
            {
                object targetValue;
                if (!sourceProps.TryGetValue(property.Key, out targetValue))
                    return false;
                if (property.Value == null)
                {
                    if (targetValue != null)
                        return false;
                }
                else if (!property.Value.Equals(targetValue))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:UseBuiltInTypeAlias", Justification = "Reviewed. Suppression is OK here.")]
        public bool Equals(DbObject other)
        {
            if (other == null) return false;

            // ReSharper disable once RedundantNameQualifier
            if (Object.ReferenceEquals(this, other))
                return true;

            if (other.GetType() != this.GetType())
                return false;

            if (this.GetHashCode() != other.GetHashCode())
                return false;

            // если ключи не инициализированы
            // то мы не можем достоверно утверждать, что это копии одного и того же объекта
            // т.к. это станет известно только после присвоения каждой копии 
            // ключей их объединяющих
            if (!other.IsKeysInitialised())
                return false;

            // проверяем совпадение по ключам
            return this.KeyEquals(other.KeyValue);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is DbObject)) return false;
            return this.Equals((DbObject)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            if (this.Identity != null)
                return this.Identity.Getter(this).GetHashCode();

            if (this.Key != null)
                return this.KeyValue.GetHashCode();
            return 0;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            var instance = (DbObject)Activator.CreateInstance(this.GetType());
            this.CopyTo(instance);
            return instance;
        }

        /// <summary>
        /// Проверяет, инициализрована ли коллекция клбючей
        /// </summary>
        /// <returns>True, все ключи имеет значение отличное от значения по умолчаню</returns>
        public virtual bool IsKeysInitialised()
        {
            if (Key == null)
                return false;

            if (KeyValue == null || KeyValue.Equals(GetDefaultValue(Key.PropertyInfo.PropertyType)))
                return false;
            return true;
        }

        /// <summary>
        /// Возвращает значение по умолчаанию для указанного типа
        /// </summary>
        /// <param name="type">Тип объекта</param>
        /// <returns>Boxed значение по умолчанию</returns>
        public static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        /// <summary>
        /// Копирует поля текущего объекта в указанный (копируюутся поля отмеченные атрибутом DataMember)
        /// </summary>
        /// <param name="target">Объект в который будет скопированы поля текущего</param>
        public void CopyTo(DbObject target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            foreach (var field in this.Properties.Select(e => e.Value))
                field.Setter(target, field.Getter(this));
        }

        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Строка вида [{key}={value}; ...]
        /// </returns>
        public override string ToString()
        {
            var fields = this.GetProperties();
            return fields.Aggregate(string.Empty, (current, field) => current + string.Format("{0}={1}; ", field.Key, field.Value ?? "null"), s => string.Format("[{0}]", s.Trim(';')));
        }
    }
}