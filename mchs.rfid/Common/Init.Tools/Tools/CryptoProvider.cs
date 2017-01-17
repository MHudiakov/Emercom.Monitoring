// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CryptoProvider.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Криптопровайдер
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Криптопровайдер
    /// </summary>
    public sealed class CryptoProvider
    {    
        /// <summary>
        /// Инициализируем провайдер с ключом по умолчанию <see cref="CryptoProvider"/>.
        /// </summary>
        public CryptoProvider()
        {
            this._tripleDes.Key = TruncateHash(KEY, this._tripleDes.KeySize / 8);
            this._tripleDes.IV = TruncateHash(string.Empty, this._tripleDes.BlockSize / 8);
        }

        /// <summary>
        /// Инициализируем провайдер с указанным ключом <see cref="CryptoProvider"/>.
        /// </summary>
        /// <param name="key">
        /// Ключ
        /// </param>
        public CryptoProvider(string key)
        {
            this._tripleDes.Key = TruncateHash(key, this._tripleDes.KeySize / 8);
            this._tripleDes.IV = TruncateHash(string.Empty, this._tripleDes.BlockSize / 8);
        }

        /// <summary>
        /// Ссылка на объект, реализующий шифрование данных по алгоритму Triple DES
        /// </summary>
        private readonly TripleDESCryptoServiceProvider _tripleDes = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// Ключ шифрования
        /// </summary>
        private const string KEY = "fhd732nf8g9sczdaw231kgjkh9gb7xzas1j3j5n6mhiinc7dd0390ds2df35l690h8n";

        /// <summary>
        /// Получение усеченного хэша
        /// </summary>
        /// <param name="key">
        /// Ключ
        /// </param>
        /// <param name="length">
        /// Длина хэша
        /// </param>
        /// <returns>
        /// Усеченный до 8 битов хэш 
        /// </returns>
        private static byte[] TruncateHash(string key, int length)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                // Хэшируем ключ
                byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
                byte[] hash = sha1.ComputeHash(keyBytes);
                Array.Resize(ref hash, length);
                return hash;
            }
        }

        /// <summary>
        /// Шифрование данных
        /// </summary>
        /// <param name="plaintext">
        /// Текст для зашифровки
        /// </param>
        /// <returns>
        /// Зашифрованная строка
        /// </returns>
        public string EncryptData(string plaintext)
        {
            // Конвертируем текстовую строку в байт-массив
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Создаем поток
            using (var ms = new System.IO.MemoryStream())
            {
                // Создаем энкодер для записи в поток
                var encStream = new CryptoStream(ms, this._tripleDes.CreateEncryptor(), CryptoStreamMode.Write);

                // Используем крипт-поток для записи байт-массива в поток
                encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
                encStream.FlushFinalBlock();

                // Конвертируем зашифрованный поток в обычную строку
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Дешифрирование данных
        /// </summary>
        /// <param name="encryptedtext">
        /// Зашифрованный текст
        /// </param>
        /// <returns>
        /// Дешифрованная строка
        /// </returns>
        public string DecryptData(string encryptedtext)
        {
            // Конвертируем зашифрованную текстовую строку в байт-массив
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

            // Создаем поток
            using (var ms = new System.IO.MemoryStream())
            {
                // Создаем декодер для записи в поток
                var decStream = new CryptoStream(ms, this._tripleDes.CreateDecryptor(), CryptoStreamMode.Write);

                // Используем крипт-поток для записи байт-массива в поток
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                // Конвертируем текстовый поток в обычную строку
                return System.Text.Encoding.Unicode.GetString(ms.ToArray());
            }
        }
    }
}
