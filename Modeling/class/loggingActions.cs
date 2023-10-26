using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeling.Constructors;
using System.IO;

using System.Web;
using System.Net;
using Modeling.generationFunctions;

namespace Modeling.Class
{
    internal class loggingActions
    {

        public static string textMessage = string.Empty;

        public void loggingFunction(string text, bool sendTgMessage)
        {
            textMessage = text;
            allConstruct allConstruct = MainWindow.allConstruct;
            generateFunc generateFunc = MainWindow.generateFunc;
            DateTime now = DateTime.Now;

            // Форматируем дату и время в нужный формат
            string formattedDateTime = now.ToString("[dd.MM.yyyy HH:mm:ss]");

            // Путь к директории, которую нужно создать
            string directoryPath = "NoEditor/Logs";

            // Проверяем, существует ли директория
            if (!Directory.Exists(directoryPath))
            {
                // Создаем директорию
                Directory.CreateDirectory(directoryPath);
            }

            // Путь к файлу, который нужно создать внутри директории
            string filePath = Path.Combine(directoryPath, "logs.log");

            string parametres = textMessage.Contains("Ошибка") || textMessage.Contains("ошибка") || textMessage.Contains("вне") ? "[ERROR]" : "[SUCCESS]";
            parametres += generateFunc.isAssembly ? " [ASSEMBLY]" : "";

            File.AppendAllText(filePath, $"{parametres} {formattedDateTime} [{allConstruct.machineName}] [{allConstruct.userName}] " + textMessage.Replace("\n", "") + Environment.NewLine);
            if(sendTgMessage)
                sendMessage();
        }

        public void sendMessage()
        {
            //Для получения chatID - https://api.telegram.org/bot6185088948:AAHxjV1yYX3gzbWjvAQ_OCD-06AbjsuZ19Y/getUpdates?offset=-10
            //tg - @fuzzyoctogiggle_bot
            //https://t.me/fuzzyoctogiggle_bot
            string[] chatId = new string[] { "800352865", "443638130" };

            string botToken = "6185088948:AAHxjV1yYX3gzbWjvAQ_OCD-06AbjsuZ19Y";
            for (int i = 0; i < chatId.Length; i++)
            {
                // Создание URL-адреса запроса с параметрами
                string apiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId[i]}&text={HttpUtility.UrlEncode(textMessage)}";

                // Создание объекта WebRequest для GET-запроса
                WebRequest request = WebRequest.Create(apiUrl);
                request.Method = "GET";
                try
                {
                    // Отправка запроса и получение ответа
                    using (WebResponse response = request.GetResponse())
                    {

                    }
                }
                catch (WebException ex)
                {
                    loggingFunction(ex.Message, false);
                }
            }
        }

    }
}
