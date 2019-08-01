using System;
using System.Collections.Specialized;
using System.Configuration;

namespace CN88.Accounts.Shared.Configurations
{
    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection settingValues;

        internal static NameValueCollection SettingValues
        {
            get { return settingValues ?? ConfigurationManager.AppSettings; }

            set { settingValues = value; }
        }

        public static string StoredProcedurePath => GetConfigValue("StoredProcedurePath");

        public static string GetConfigValue(string configName) => SettingValues[configName];

        public static void Verify() => Require(StoredProcedurePath, nameof(StoredProcedurePath));

        private static void Require(string key, string keyName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ConfigurationErrorsException($"{keyName} is null or whitespace");
            }
        }
    }
}
