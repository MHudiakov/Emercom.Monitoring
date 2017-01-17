namespace Maps.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
	using System.Text;
	using System.Net;
	using Init.Tools;

    public class Tile : Image
    {
        static MapSettings Settings { get { return MapDesigner.MapSettings; } }
        public new BackgroundMap Parent { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Tile(BackgroundMap parent, int x, int y)
        {
            this.Parent = parent;
            this.X = x;
            this.Y = y;
            this.IsHitTestVisible = false;

            this.SnapsToDevicePixels = true;

            this.Width = 256;
            this.Height = 256;

            LoadStack.Add(this);
        }

        public class LoadStack
        {
            public static List<Tile> Tiles = new List<Tile>();
            static Object LockObject = new Object();
            public static int WebExcecuteCountBefore = 0;
            public static int WebExcecuteCountAfter = 0;
            public static void Clear()
            {
                lock (LockObject)
                {
                    Tiles.Clear();
                }
            }

            public static void Add(Tile tile)
            {
                lock (LockObject)
                {
                    if (Tiles.Exists(tt => Equals(tt, tile)))
                        return;

                    var existsTile = Tiles.FirstOrDefault(tt => tt.X == tile.X && tt.Y == tile.Y);
                    if (existsTile != null)
                        Tiles.Remove(existsTile);

                    var filename = tile.GetFileName();
                    if (!File.Exists(filename) || (DateTime.Now - File.GetCreationTime(filename)).TotalDays > 5)
                    {
                        if (File.Exists(filename))
                            tile._setSource(filename);
                    }
                    else
                    {
                        tile._setSource(filename);
                        return;
                    }

                    Tiles.Add(tile);
                    if (!Started)
                    {
                        Thread.Start();
                        Started = true;
                    }
                }
            }

            static bool Started = false;
            static Thread Thread = new Thread(new ThreadStart(Execute)) { IsBackground = true };
            public static int poolThread = 0;

            public class TileThread
            {
                public TileThread(Thread thread, Tile tile)
                {
                    this.Thread = thread;
                    this.Tile = tile;
                }
                public Thread Thread;
                public Tile Tile;
            }

            static List<TileThread> ThreadList = new List<TileThread>();
            static void AddThread(Thread thread, Tile tile)
            {
                lock (ThreadList)
                {
                    ThreadList.Add(new TileThread(thread, tile));
                }
            }
            static void DelThread(Thread thread)
            {
                lock (ThreadList)
                {
                    ThreadList.RemoveAll(t => t.Thread == thread);
                }
            }

            public static void StopThreads()
            {
                lock (ThreadList)
                {
                    var threads = ThreadList.ToList();
                    ThreadList.Clear();

                    foreach (var t in threads)
                    {
                        try
                        {
                            if (t.Tile.HttpWebRequest != null)
                                t.Tile.HttpWebRequest.Abort();
                            t.Tile.IsLoadAbort = true;
                        }
                        catch { }
                    }
                }
            }

            static void Execute()
            {
                try
                {
                    while (true)
                    {

                        if (poolThread > 20)
                        {
                            Thread.Sleep(50);
                            continue; ;
                        }
                        Tile t = GetTile();
                        if (t == null)
                            break;

                        poolThread++;

                        var th = new Thread(new ParameterizedThreadStart(LoadPicture));
                        th.IsBackground = true;

                        th.Start(t);
                        AddThread(th, t);

                    }
                    lock (LockObject)
                    {
                        Thread = new Thread(new ThreadStart(Execute)) { IsBackground = true };
                        Started = false;
                    }
                }
                catch (Exception ex)
                {
                    Log.AddException("Обвалились tiles", ex);
                }
            }

            private static void LoadPicture(object t)
            {
                try
                {
                    (t as Tile).LoadPicture();
                }
                finally
                {
                    poolThread--;
                    DelThread(Thread.CurrentThread);
                }
            }

            private static Tile GetTile()
            {
                lock (LockObject)
                {
                    if (Tiles.Count == 0)
                        return null;
                    var t = Tiles.LastOrDefault();
                    Tiles.Remove(t);
                    return t;
                }
            }
        }

        static Object LockObject = new Object();
        HttpWebRequest HttpWebRequest;
        private void LoadPicture()
        {
            var filename = this.GetFileName();
            try
            {
                // lock (LockObject)
                {

                    try
                    {
                        if (!File.Exists(filename) || (DateTime.Now - File.GetCreationTime(filename)).TotalDays > 2)
                        {
                            if (File.Exists(filename))
                                this.Dispatcher.Invoke(new setSourceDelegate(this._setSource), filename);

                            var tileSource = Settings.TileSampleURL; // "http://otile4.mqcdn.com/tiles/1.0.0/osm/{2}/{0}/{1}.png";
                            //var tileSource = "http://a.tile.openstreetmap.org/{2}/{0}/{1}.png";
                            tileSource = string.Format(tileSource, this.X, this.Y, this.Parent.Parent.Z_Level);
                            //var uri = new Uri(tileSource);
                            LoadStack.WebExcecuteCountBefore++;
                            this.HttpWebRequest = WebRequest.Create((string)tileSource) as HttpWebRequest;
                            this.HttpWebRequest.ServicePoint.ConnectionLimit = 40;
                            if (this.IsLoadAbort)
                                return;

                            try
                            {
                                var res = this.HttpWebRequest.GetResponse();
                                if (this.IsLoadAbort)
                                    return;
                                var stream = res.GetResponseStream();
                                LoadStack.WebExcecuteCountAfter++;
                                var len = (int)res.ContentLength;
                                var buf = new byte[len];
                                var xx = stream.Read(buf, 0, len);
                                int pos = xx;
                                int loaded = xx;
                                while (pos < len)
                                {
                                    xx = stream.Read(buf, pos, len - loaded);
                                    pos += xx;
                                    loaded += xx;
                                }
                                if (this.IsLoadAbort)
                                    return;
                                File.Delete(filename);
                                File.WriteAllBytes(filename, buf);
                                if (this.IsLoadAbort)
                                    return;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (this.IsLoadAbort)
                    return;
                this.Dispatcher.Invoke(new setSourceDelegate(this._setSource), filename);
                /*
                logo.UriSource = new Uri(string.Format("http://a.tile.openstreetmap.org/16/{0}/{1}.png",
                    38597 + X,
                    20801 + Y));
                logo.EndInit();*/
            }
            catch (Exception ex)
            {

            }
        }

        delegate void setSourceDelegate(string filename);
        void _setSource(string filename)
        {
            try
            {
                if (this.IsLoadAbort)
                    return;
                BitmapImage logo = new BitmapImage();
                if (File.Exists(filename))
                {
                    if (this.IsLoadAbort)
                        return;
                    logo.BeginInit();
                    logo.UriSource = new Uri(filename);
                    logo.EndInit();
                }
                this.Source = logo;
            }
            catch (Exception ex)
            {
                string msg = "Error: " + ex.Message + Environment.NewLine + ex.StackTrace;
                Log.Add("MapLog", msg);
            }
        }

        private string GetFileName()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path += "\\Contact24";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "\\map";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "\\cash";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "\\" + Settings.TileCashPrefix;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "\\Z" + this.Parent.Parent.Z_Level;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filename = path + string.Format("\\{0}_{1}.png", this.X, this.Y);

            return filename;
        }

        public bool IsLoadAbort = false;
    }
}
