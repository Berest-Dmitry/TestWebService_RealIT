using Newtonsoft.Json;

namespace TestWebService_RealIT
{
    /// <summary>
    /// класс работы с конфигами
    /// </summary>
    public static class SecretsManager
    {
        /// <summary>
        /// метод загрузки переменных окружения проекта
        /// </summary>
        /// <param name="filePath"></param>
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string text = File.ReadAllText(filePath);
            var settingsObject = JsonConvert.DeserializeObject<UserSecretsItem>(text, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            });

            foreach (var prop in typeof(UserSecretsItem).GetProperties())
            {
                Environment.SetEnvironmentVariable(prop.Name, Convert.ToString(prop.GetValue(settingsObject)));
            }

        }

        /// <summary>
        /// объект секретных настроек проекта
        /// </summary>
        internal class UserSecretsItem
        {
            public string APIToken { get; set; }
        }
    }

}
