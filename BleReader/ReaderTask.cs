using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DAL.WCF.ServiceReference;
using Quartz;

namespace BleReader
{
    internal class ReaderTask : IJob
    {
        private List<string> _lastTagList;

        public void Execute(IJobExecutionContext context)
        {
            string urlAddress = "http://192.168.1.17:8595/";

            var request = (HttpWebRequest)WebRequest.Create(urlAddress);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    return;

                Stream receiveStream = response.GetResponseStream();

                if (receiveStream == null)
                    return;

                using (var readStream = response.CharacterSet == null ?
                    new StreamReader(receiveStream) :
                    new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)))
                {
                    string data = readStream.ReadToEnd();
                    var tags = PharseData(data);
                    DataProcessing(tags);
                }
            }
        }

        private void DataProcessing(List<string> tags)
        {
            if (_lastTagList == null)
            {
                _lastTagList = tags;

                Movement movement = new Movement();
                movement.IsArrived = true;
                movement.UnitId = Settings.Default.UnitId;

            }  
        }

        private List<string> PharseData(string data)
        {
            // Берем часть строки начиная с первого тега
            int indexOfFirstTag = data.IndexOf("+INQ", StringComparison.OrdinalIgnoreCase);

            if (indexOfFirstTag == -1)
                return null;

            var resultData = data.Substring(indexOfFirstTag);

            // Смотрим, есть ли статус ОК после списка тегов
            int indexOfOk = resultData.IndexOf("OK", StringComparison.OrdinalIgnoreCase);

            if (indexOfOk == -1)
                return null;

            // Оставляем только теги, удаляем ок
            resultData = resultData.Substring(0, indexOfOk);

            // Получаем список тегов
            var tmpTagList = resultData.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            tmpTagList = tmpTagList.Select(tag => tag.Insert(tag.Length, ";").Replace("+INQ:", "")).ToList();

            // Проверяем, что в каждой группе каждого тега количество цифр четное
            // если не четное, то добавляем 0
            var tagList = new List<string>();
            foreach (var tmpTag in tmpTagList)
            {
                string tag = string.Empty;
                string group = string.Empty;
                foreach (char c in tmpTag)
                {
                    char[] separateSymbols = { ',', ':', ';' };
                    if (!separateSymbols.Contains(c))
                    {
                        group += c;
                    }
                    else
                    {
                        if (group.Length % 2 == 1)
                        {
                            // Добавляем 0 в начало группы
                            tag += "0" + group;
                        }
                        else
                        {
                            tag += group;
                        }

                        group = string.Empty;

                        if (c != ';')
                        {
                            tag += c;
                        }
                    }
                }

                tagList.Add(tag);
            }

            return tagList;
        }
    }
}