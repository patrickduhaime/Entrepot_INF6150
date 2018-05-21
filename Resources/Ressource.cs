using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Resources
{
    public class Ressource
    {
        #region "Membres"                      

        //Membre privé représente la culture actuel de l'application
        private CultureInfo m_CultureInfo;

        //Membre privé représente l'instance de ressource
        private static Ressource m_Instance;

        //Membre privé représente le ressoruce manager
        private ResourceManager m_ResourceManager;

        #endregion "Membres"

        #region "Constructor"

        private Ressource()
        {
            m_CultureInfo = CultureInfo.InstalledUICulture;
            if (m_CultureInfo == null) m_CultureInfo = new CultureInfo("en-CA");
            //m_Assembly = Assembly.Load("Entrepot_INF6150");
            m_ResourceManager = new ResourceManager("Resources.Resource", Assembly.GetExecutingAssembly());
        }

        #endregion "Constructor"

        #region "Property"

        /// <summary>
        /// Property représente la langue dy systeme
        /// </summary>
        public CultureInfo CultureInfo
        {
            get { return m_CultureInfo; }
            set { m_CultureInfo = value; }
        }

        #endregion "Property"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static Ressource Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Ressource();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Permet de changer la langue de l'application
        /// </summary>
        /// <param name="language"></param>
        public void ChangeLanguage(string language)
        {
            m_CultureInfo = new CultureInfo(language);
        }

        /// <summary>
        /// Permet de traduire le string entré en parametre en langue du systeme
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetTraduction(string str)
        {
            return m_ResourceManager.GetString(str);
        }

        #endregion "Methods"
    }
}