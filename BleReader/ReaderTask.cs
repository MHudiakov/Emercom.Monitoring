using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DAL.WCF;
using DAL.WCF.ServiceReference;
using Quartz;

namespace BleReader
{
    internal class ReaderTask : IJob
    {
        private List<string> _lastTagList;

        public void Execute(IJobExecutionContext context)
        {
            string urlAddress = "http://192.168.10.7/";

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
            // Если предыдущий список метки пустой, то создаем для каждой метки движение и добавляем на сервер
            if (_lastTagList == null)
            {
                _lastTagList = tags;

                foreach (var tag in tags)
                {
                    AddMovement(tag, true);
                }
            }

            // Сравниваем текущий список меток с предыдущим, смотрим, какие метки пришли
            foreach (var tag in tags)
            {
                if (!_lastTagList.Contains(tag))
                {
                    AddMovement(tag, true);
                }
            }

            // Смотрим, какие ушли
            foreach (var tag in _lastTagList)
            {
                if (!tags.Contains(tag))
                {
                    AddMovement(tag, false);
                }   
            }
        }

        private void AddMovement(string tag, bool isArrived)
        {
            Movement movement = new Movement();
            movement.IsArrived = isArrived;
            movement.UnitId = Settings.Default.UnitId;
            DalContainer.WcfDataManager.ServiceOperationClient.AddMovement(movement, tag);
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