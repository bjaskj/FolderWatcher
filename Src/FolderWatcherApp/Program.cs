using System;
using System.IO;
using System.Threading;

namespace FolderWatcherApp
{
    class Program
    {
        private static bool _exit;
        private const int ScanningInterval = 500;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("args", "Must have a folder specified on launch");
            }

            const string filter = "*.*";
            const NotifyFilters notificationFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            var path = args[0];

            var watcher = new FileSystemWatcher(path, filter)
            {
                NotifyFilter = notificationFilter
            };

            watcher.Changed += WatcherOnChanged;
            watcher.Renamed += WatcherOnRenamed;

            // start
            watcher.EnableRaisingEvents = true;

            while (!_exit)
            {
                Thread.Sleep(ScanningInterval);
            }
        }

        private static void WatcherOnRenamed(object sender, RenamedEventArgs renamedEventArgs)
        {
            //Console.WriteLine("WatcherOnRenamed {0} : {1}", renamedEventArgs.ChangeType, renamedEventArgs.FullPath);
            _exit = true;
        }

        private static void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            //Console.WriteLine("WatcherOnChanged {0} : {1}", fileSystemEventArgs.ChangeType, fileSystemEventArgs.FullPath);
            _exit = true;
        }
    }
}
