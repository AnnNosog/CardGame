using System;
using System.IO;
using System.Text;

namespace LogerLib
{
    public static class Loger
    {
        private static string _text;

        public static string ReadIni(string filePath = "Config.ini")
        {
            using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                _text = streamReader.ReadToEnd();
                return _text;
            }
        }

        public static void WriteLog(string message, string method, string type, string filePath = "log.txt")
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                _text = ReadIni();

                DateTime dateTime = DateTime.Now;
                string date = dateTime.ToShortDateString();
                string time = dateTime.ToLongTimeString();

                _text = _text.Replace("[ДАТА]", $" {date} ");
                _text = _text.Replace("[ВРЕМЯ]", $" {time} ");
                _text = _text.Replace("[ТИП]", $" {type} ");
                _text = _text.Replace("[МЕТОД]", $" {method} ");
                _text = _text.Replace("[ТЕКСТ]", $" {message} ");

                streamWriter.WriteLine(_text);
            }
        }

    }
}
