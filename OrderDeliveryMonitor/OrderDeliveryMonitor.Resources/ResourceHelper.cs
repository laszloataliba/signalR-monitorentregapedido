using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace OrderDeliveryMonitor.Resources
{
    public class ResourceHelper
    {
        private static ResourceManager _resourceManager;

        public ResourceHelper()
        {
        }

        static ResourceHelper()
        {
            ResourceHelper._resourceManager = 
                new ResourceManager($"{typeof(ResourceHelper).Assembly.GetName().Name}.Resource", typeof(ResourceHelper).Assembly);
        }

        /// <summary>
        /// Sets the thread current culture.
        /// pt-BR - Portuguese(Brazil)
        /// en-US - English USA
        /// es-ES - Spanish(Spain)
        /// </summary>
        /// <param name="pCulture">Selected culture.</param>
        public static void SetCurrentCulture(string pCulture = "pt-BR")
        {
            switch (pCulture)
            {
                case "en-US":
                case "es-ES":
                case "pt-BR":
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(pCulture);
                    break;

                default:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                    break;
            }
        }

        /// <summary>
        /// Recovers a specific text given its parameter.
        /// </summary>
        /// <param name="pKey">Key parameter.</param>
        /// <returns>Returns a specific text given its parameter.</returns>
        public static string GetResourceValue(string pKey)
        {
            string _value = ResourceHelper._resourceManager.GetString(pKey, CultureInfo.CurrentCulture);

            return _value ?? pKey;
        }

        /// <summary>
        /// Retorna um string formatada com as chaves passadas como parâmetro
        /// Recovers a string specifically formatted.
        /// </summary>
        /// <param name="pKey">Mother key.</param>
        /// <param name="pKeys">Child keys.</param>
        /// <returns>Returns a specifically formatted text.</returns>
        public static string GetResourceValue(string pKey, params object[] pKeys)
        {
            try
            {
                return
                    string.Format
                        (
                            ResourceHelper._resourceManager.GetString(pKey, CultureInfo.CurrentCulture) ?? pKey,
                            pKeys.Select(x => ResourceHelper._resourceManager.GetString(x.ToString(), CultureInfo.CurrentCulture) ?? x)
                            .ToArray()
                        );
            }
            catch
            {
                return pKey;
            }
        }
    }
}
