using Business;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Services
{
    public class ArticleService : IDisposable
    {
        #region "Constants"

        private const string STR_CREATEARTICLEPROC = "";
        private const string STR_DELETEARTICLEPROC = "";
        private const string STR_READARTICLEPROC = "";
        private const string STR_UPDATEARTICLEPROC = "";

        #endregion "Constants"

        #region "Membres"

        //Membre privé représente l'instance de ressource
        private static ArticleService m_Instance;

        //Membre privé représente la connection à la base de donnée
        private SqlConnection m_SqlConnection;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion "Membres"

        #region "Constructor"

        private ArticleService()
        {
            m_SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AppUser"].ConnectionString);
            m_SqlConnection.Open();
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static ArticleService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ArticleService();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Permet de créer un article dans la base de données et setter son ID générer
        /// </summary>
        /// <param name="article"></param>
        public void Create(T article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_CREATEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            if (!String.IsNullOrEmpty(article.Description)) cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = article.Description;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = article.Name;
            cmd.Parameters.Add("@Space", SqlDbType.Float).Value = article.Space;

            article.Id = (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Permet de suppriemer un article de la base de données
        /// </summary>
        /// <param name="article"></param>
        public void Delete(T article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_DELETEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = article.Id;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permet de lire un ou plusieur article de la base de données
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<T> Read(String filter)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            IList<T> articles = new List<T>();

            cmd.CommandText = STR_CREATEARTICLEPROC;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = m_SqlConnection;

            cmd.Parameters.Add("@Filter", SqlDbType.VarChar).Value = filter;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                T article = new T
                {
                    Description = reader["description"]!=null?(String)reader["description"]:String.Empty,
                    Id = (int)reader["id"],
                    Name = (String)reader["name"],
                    Space = (float)reader["space"]
                };
                articles.Add(article);
            }

            return articles;
        }

        /// <summary>
        /// Permet de mettre à jours un article dans la base de données
        /// </summary>
        /// <param name="article"></param>
        public void Update(T article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_UPDATEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = article.Id;
            if(!String.IsNullOrEmpty(article.Description)) cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = article.Description;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = article.Name;
            cmd.Parameters.Add("@Space", SqlDbType.Float).Value = article.Space;

            cmd.ExecuteNonQuery();
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