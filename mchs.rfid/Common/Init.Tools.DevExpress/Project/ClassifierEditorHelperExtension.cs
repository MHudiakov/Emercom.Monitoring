// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassifierEditorHelperExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для элементов управления, отображающих справочники
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Расширение для элементов управления, отображающих справочники
    /// </summary>
    public static class ClassifierEditorHelperExtension
    {
        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип справочника
        /// </typeparam>
        /// <param name="edit">
        /// Элемен управления
        /// </param>
        /// <param name="loadItems">
        /// Метод получения списка элементов
        /// </param>
        /// <param name="saveItem">
        /// Метод сохранения элемента
        /// </param>
        /// <param name="editItem">
        /// Метод редактирования элемента
        /// </param>
        /// <param name="createItem">
        /// Метод создания элемена
        /// </param>
        /// <param name="setItemsCollection">
        /// Метод установки отображаемой коллекции элементов контролу
        /// </param>
        /// <returns>
        /// Хелпер для управления расши расширением
        /// </returns>
        public static ClassifierEditorHelper<T> InlineAddExtension<T>(
            this PopupBaseEdit edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem,
            Func<T> createItem,
            Action<List<T>> setItemsCollection)
            where T : class, new()
        {
            if (edit == null)
                throw new ArgumentNullException("edit");
            if (loadItems == null)
                throw new ArgumentNullException("loadItems");
            if (saveItem == null)
                throw new ArgumentNullException("saveItem");
            if (editItem == null)
                throw new ArgumentNullException("editItem");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (setItemsCollection == null)
                throw new ArgumentNullException("setItemsCollection");

            return new ClassifierEditorHelper<T>(edit, loadItems, saveItem, editItem, createItem, setItemsCollection);
        }

        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип справочника
        /// </typeparam>
        /// <param name="edit">
        /// Элемен управления
        /// </param>
        /// <param name="loadItems">
        /// Метод получения списка элементов
        /// </param>
        /// <param name="saveItem">
        /// Метод сохранения элемента
        /// </param>
        /// <param name="editItem">
        /// Метод редактирования элемента
        /// </param>
        /// <returns>
        /// Хелпер для управления расши расширением
        /// </returns>
        public static ClassifierEditorHelper<T> InlineAddExtension<T>(
            this ComboBoxEdit edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem)
            where T : class, new()
        {
            return InlineAddExtension(edit, loadItems, saveItem, editItem, () => new T());
        }

        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип справочника
        /// </typeparam>
        /// <param name="edit">
        /// Элемен управления
        /// </param>
        /// <param name="loadItems">
        /// Метод получения списка элементов
        /// </param>
        /// <param name="saveItem">
        /// Метод сохранения элемента
        /// </param>
        /// <param name="editItem">
        /// Метод редактирования элемента
        /// </param>
        /// <param name="createItem">
        /// Метод создания элемена
        /// </param>
        /// <returns>
        /// Хелпер для управления расши расширением
        /// </returns>
        public static ClassifierEditorHelper<T> InlineAddExtension<T>(
            this ComboBoxEdit edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem,
            Func<T> createItem)
            where T : class, new()
        {
            if (edit == null)
                throw new ArgumentNullException("edit");
            if (loadItems == null)
                throw new ArgumentNullException("loadItems");
            if (saveItem == null)
                throw new ArgumentNullException("saveItem");
            if (editItem == null)
                throw new ArgumentNullException("editItem");
            if (createItem == null)
                throw new ArgumentNullException("createItem");

            return new ClassifierEditorHelper<T>(
                edit,
                loadItems,
                saveItem,
                editItem,
                createItem,
                list =>
                {
                    edit.Properties.Items.Clear();
                    edit.Properties.Items.AddRange(list);
                });
        }

        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип справочника
        /// </typeparam>
        /// <param name="edit">
        /// Элемен управления
        /// </param>
        /// <param name="loadItems">
        /// Метод получения списка элементов
        /// </param>
        /// <param name="saveItem">
        /// Метод сохранения элемента
        /// </param>
        /// <param name="editItem">
        /// Метод редактирования элемента
        /// </param>
        /// <returns>
        /// Хелпер для управления расши расширением
        /// </returns>
        public static ClassifierEditorHelper<T> InlineAddExtension<T>(
            this LookUpEditBase edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem)
            where T : class, new()
        {
            return InlineAddExtension(edit, loadItems, saveItem, editItem, () => new T());
        }

        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">
        /// Тип справочника
        /// </typeparam>
        /// <param name="edit">
        /// Элемен управления
        /// </param>
        /// <param name="loadItems">
        /// Метод получения списка элементов
        /// </param>
        /// <param name="saveItem">
        /// Метод сохранения элемента
        /// </param>
        /// <param name="editItem">
        /// Метод редактирования элемента
        /// </param>
        /// <param name="createItem">
        /// Метод создания элемена
        /// </param>
        /// <returns>
        /// Хелпер для управления расши расширением
        /// </returns>
        public static ClassifierEditorHelper<T> InlineAddExtension<T>(
            this LookUpEditBase edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem,
            Func<T> createItem)
            where T : class, new()
        {
            if (edit == null)
                throw new ArgumentNullException("edit");
            if (loadItems == null)
                throw new ArgumentNullException("loadItems");
            if (saveItem == null)
                throw new ArgumentNullException("saveItem");
            if (editItem == null)
                throw new ArgumentNullException("editItem");
            if (createItem == null)
                throw new ArgumentNullException("createItem");

            return new ClassifierEditorHelper<T>(edit, loadItems, saveItem, editItem, createItem, list => edit.Properties.DataSource = list);
        }
    }
}