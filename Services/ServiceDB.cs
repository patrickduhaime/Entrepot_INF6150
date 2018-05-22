using Microsoft.Win32.SafeHandles;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Services
{
    /// <summary>
    /// Permet de gerer la connexion au serveur de la base de donner
    /// </summary>
    public class ServiceDB : IDisposable
    {
        #region "Membres"

        //Membre privé représente l'instance de ressource
        private static ServiceDB m_Instance;

        //Membre privé représente la connection à la base de donnée
        private SqlConnection m_SqlConnection;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion "Membres"

        #region "Properties"

        /// <summary>
        /// Permet d'avoir m_SqlConnection de la base de donnée
        /// </summary>
        public SqlConnection SqlConnection
        {
            get { return m_SqlConnection; }
            set { m_SqlConnection = value; }
        }

        #endregion "Properties"

        #region "Constructor"

        private ServiceDB()
        {
            m_SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AppUser"].ConnectionString);
            m_SqlConnection.Open();
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static ServiceDB Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ServiceDB();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                m_SqlConnection.Close();
            }

            disposed = true;
        }

        #endregion "Methods"
    }
}