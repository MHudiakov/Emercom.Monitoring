// --------------------------------------------------------------------------------------------------------------------
// <copyright file="xMsg.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Helper класс для отображения стандартных сообщений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    using Init.Tools.UI.Forms;

    /// <summary>
    /// Helper класс для отображения стандартных сообщений
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once InconsistentNaming
    public static class xMsg
    {
        /// <summary>
        /// Сообщение с заданным текстом
        /// </summary>
        /// <param name="text">Текст</param>
        public static void Msg(string text)
        {
            MessageBox.Show(text);
        }

        /// <summary>
        /// Сообщение об отсутствии данных для редактирования
        /// "Выберите запись для редактирования"
        /// </summary>
        public static void MsgEmptyEditData()
        {
            Msg("Выберите запись для редактирования");
        }

        /// <summary>
        /// Сообщение об отсутствии данных для просмотра
        /// "Выберите запись для просмотра"
        /// </summary>
        public static void MsgEmptyViewData()
        {
            Msg("Выберите запись для просмотра");
        }

        /// <summary>
        /// Сообщение об отсутствии данных для удаления
        /// "Выберите запись для удаления"
        /// </summary>
        public static void MsgEmptyDeleteData()
        {
            Msg("Выберите запись для удаления");
        }

        /// <summary>
        /// Сообщение об ошибке при редактировании данных
        /// "При редактировании произошла ошибка"
        /// </summary>
        public static void MsgErrorEdit()
        {
            Msg("При редактировании произошла ошибка");
        }

        /// <summary>
        /// Сообщение об ошибке при удалении данных
        /// "При удалении произошла ошибка"
        /// </summary>
        public static void MsgErrorDelete()
        {
            Msg("При удалении произошла ошибка");
        }

        /// <summary>
        /// Сообщение о подтверждении удаления выбранной записи
        /// "Удалить выбранную запись?"
        /// </summary>
        /// <returns>
        /// True, если пользователь подтвердил удаление
        /// </returns>
        public static bool MsgWithConfirmDelete()
        {
            return MsgWithConfirmCustomText("Удалить выбранную запись?", "Внимание!");
        }

        /// <summary>
        /// Сообщение с подтверждением с заданным текстом
        /// </summary>
        /// <param name="text">
        /// Текст
        /// </param>
        /// <returns>
        /// True, если пользователь подтвердил запрос
        /// </returns>
        public static bool MsgWithConfirmCustomText(string text)
        {
            return MsgWithConfirmCustomText(text, @"Внимание!");
        }

        /// <summary>
        /// Сообщение с подтверждением с заданным текстом и заголовком
        /// </summary>
        /// <param name="text">
        /// Текст сообщения
        /// </param>
        /// <param name="caption">
        /// Заголовок сообщения
        /// </param>
        /// <returns>
        /// True, если пользователь подтвердил запрос
        /// </returns>
        public static bool MsgWithConfirmCustomText(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }

        /// <summary>
        /// Запрос на сохранение перед продолжением
        /// </summary>
        /// <returns>True, если пользователь подтвердил сохранение.</returns>
        public static bool MsgWithConfirmSaveBeforeContinue()
        {
            return MsgWithConfirmCustomText("Для продолжения, необходимо сохранить изменения. Сохранить?");
        }

        /// <summary>
        /// Произвольный запрос
        /// </summary>
        /// <param name="text">
        /// Текст запроса
        /// </param>
        /// <returns>
        /// True, если пользователь подтвердил запрос
        /// </returns>
        public static bool MsgWithConfirmCustomQuestion(string text)
        {
            return MsgWithConfirmCustomQuestion(text, "Внимание");
        }

        /// <summary>
        /// Произвольный запрос с подтверждением
        /// </summary>
        /// <param name="text">
        /// Текст запроса
        /// </param>
        /// <param name="caption">
        /// Текст заголовка
        /// </param>
        /// <returns>
        /// True, если пользователь подтвердил запрос
        /// </returns>
        public static bool MsgWithConfirmCustomQuestion(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Сообщение о необходимости сохранения перед продолжением
        /// </summary>
        /// <param name="text">
        /// Текст запроса
        /// </param>
        /// <param name="caprion">
        /// Заголовок
        /// </param>
        public static void MsgNeedSaveBeforeContinue(string text, string caprion)
        {
            MessageBox.Show(text, caprion, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке</param>
        public static void Error(string message)
        {
            MessageBox.Show(message, @"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Сообщение об ошибке (расширенная форма)
        /// </summary>
        /// <param name="ex">Информация об ошибке</param>
        public static void Error(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            fmError.Show(ex);
        }

        /// <summary>
        /// Предупреждение
        /// </summary>
        /// <param name="message">Текст предупреждения</param>
        public static void Warning(string message)
        {
            MessageBox.Show(message, @"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Информация
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public static void Information(string message)
        {
            MessageBox.Show(message, @"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
