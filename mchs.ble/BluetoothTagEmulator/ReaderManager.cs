using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BluetoothTagEmulator
{

    public class ReaderManager
    {
        private static Random _rnd = new Random();

        public static List<Reader> Readers;

        public Thread ReRandomListThread;

        public ReaderManager()
        {
            Readers = new List<Reader>();
            for (int i = 1; i <= 5; i++)
            {
                var reader = new Reader { Id = i };

                var tagList = new List<Tag>();

                for (int j = 1; j <= _rnd.Next(1, MacList.Count / 2); j++)
                {
                    tagList.Add(new Tag { Id = j, Mac = GetRandomMacAddress(tagList), SignalLevel = _rnd.Next(1, 100) });
                }

                reader.Tags = tagList.ToList();
                reader.StoreTags = tagList.ToList();

                Readers.Add(reader);
            }

            this.ReRandomListThread = new Thread(ReRandomList) { IsBackground = true };
            this.ReRandomListThread.Start();
        }

        private void ReRandomList()
        {
            while (true)
            {
                Thread.Sleep(30000);

                lock (Readers)
                {
                    var list = Readers.ToList();

                    foreach (var reader in list)
                    {
                        var tags = reader.Tags.ToList();
                        var storeTags = reader.StoreTags.ToList();

                        if (tags.Count > 5)
                        {
                            var countForRemove = _rnd.Next(1, tags.Count / 2);
                            for (int i = 0; i < countForRemove; i++)
                            {
                                tags.RemoveAt(_rnd.Next(0, tags.Count - 1));
                            }
                        }

                        if (tags.Count < MacList.Count - 5)
                        {
                            var countForAdding = _rnd.Next(1, (MacList.Count - tags.Count) / 2);
                            for (int i = 0; i < countForAdding; i++)
                            {
                                var tag = new Tag { Mac = GetRandomMacAddress(tags) };
                                var storeTag = storeTags.FirstOrDefault(x => x.Mac == tag.Mac);
                                if (storeTag == null)
                                {
                                    tag.Id = storeTags.Max(x => x.Id) + 1;
                                    storeTags.Add(tag);
                                }
                                else
                                {
                                    tag.Id = storeTag.Id;
                                }
                                tags.Add(tag);
                            }
                        }

                        foreach (var tag in tags)
                        {
                            tag.SignalLevel = _rnd.Next(1, 100);
                        }
                        reader.Tags = tags.ToList();
                        reader.StoreTags = storeTags.ToList();
                    }
                }
                Globals.DisplayData(Globals.MessageType.Normal, "Список тегов успешно обновился!");
            }
        }

        private static string GetRandomMacAddress(List<Tag> tagList)
        {
            while (true)
            {
                var item = MacList[_rnd.Next(0, MacList.Count - 1)];
                if (tagList.Any(x => x.Mac == item))
                    continue;

                return item;
            }
        }

        public static List<string> MacList = new List<string>()
        {
            "AABBCCDDEE01",
            "AABBCCDDEE02",
            "AABBCCDDEE03",
            "AABBCCDDEE04",
            "AABBCCDDEE05",
            "AABBCCDDEE06",
            "AABBCCDDEE07",
            "AABBCCDDEE08",
            "AABBCCDDEE09",
            "AABBCCDDEE10",
            "AABBCCDDEE11",
            "AABBCCDDEE12",
            "AABBCCDDEE13",
            "AABBCCDDEE14",
            "AABBCCDDEE15",
            "AABBCCDDEE16",
            "AABBCCDDEE17",
            "AABBCCDDEE18",
            "AABBCCDDEE19",
            "AABBCCDDEE20",
            "AABBCCDDEE21",
            "AABBCCDDEE22",
            "AABBCCDDEE23",
            "AABBCCDDEE24",
            "AABBCCDDEE25",
            "AABBCCDDEE26",
            "AABBCCDDEE27",
            "AABBCCDDEE28",
            "AABBCCDDEE29",
            "AABBCCDDEE30",
            "AABBCCDDEE31",
            "AABBCCDDEE32",
            "AABBCCDDEE33",
            "AABBCCDDEE34",
            "AABBCCDDEE35",
            "AABBCCDDEE36",
            "AABBCCDDEE37",
            "AABBCCDDEE38",
            "AABBCCDDEE39",
            "AABBCCDDEE40",
            "AABBCCDDEE41",
            "AABBCCDDEE42",
            "AABBCCDDEE43",
            "AABBCCDDEE44",
            "AABBCCDDEE45",
            "AABBCCDDEE46",
            "AABBCCDDEE47",
            "AABBCCDDEE48",
            "AABBCCDDEE49",
            "AABBCCDDEE50"
        };
    }

    public class Reader
    {
        public int Id { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Tag> StoreTags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Mac { get; set; }
        public int SignalLevel { get; set; }
    }


}
