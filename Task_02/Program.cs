using LogerLib;
using System;
using System.IO;

namespace Task_02
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = @"G:\ANN\STEP\Sharp\Homework_09\Test\bin\Debug";
                //watcher.Path = @"C:\Users\Public\Documents";

                watcher.Filter = "*.doc";

                watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                string filePath = "tests.doc";
                string filePath2 = "test2.doc";
                string filePath3 = "test23.doc";

                //string filePath = @"C:\Users\Public\Documents\tests.doc";
                //string filePath2 = @"C:\Users\Public\Documents\test2.doc";
                //string filePath3 = @"C:\Users\Public\Documents\test23.doc";

                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath)) { }
                }

                if (!File.Exists(filePath2))
                {
                    using (FileStream fs = File.Create(filePath2)) { }
                }

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine("File1");
                }

                using (StreamWriter sw = new StreamWriter(filePath2, true))
                {
                    sw.WriteLine("File2");
                }

                File.Delete(filePath);

                if (!File.Exists(filePath3))
                {
                    File.Move(filePath2, filePath3);
                }

                Console.ReadKey();
            }
        }

        static void OnChanged(object source, FileSystemEventArgs e)
        {
            Loger.WriteLog($"Изменение файла/каталога {e.Name}. Путь {e.FullPath}.", nameof(Main), "Text mess");
        }

        static void OnCreated(object source, FileSystemEventArgs e)
        {
            Loger.WriteLog($"Создание файла/каталога {e.Name}. Путь {e.FullPath}.", nameof(Main), "Text mess");
        }

        static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Loger.WriteLog($"Удаление файла/каталога {e.Name}. Путь {e.FullPath}.", nameof(Main), "Text mess");
        }

        static void OnRenamed(object source, RenamedEventArgs e)
        {
            Loger.WriteLog($"Переименование файла/каталога {e.Name}. Путь {e.FullPath}.", nameof(Main), "Text mess");
        }

    }
}