using log4net;
using System.Reflection;

namespace pr4
{
    public static class AppLogger
    {
        private static readonly ILog log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public static void LogMethod(string methodName, string description)
        {
            log.Info($"Вызов метода: {methodName} | Описание: {description}");
        }
    }
}