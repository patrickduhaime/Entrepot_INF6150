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
    public class ArticleService
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

        #endregion "Membres"

        #region "Constructor"

        private ArticleService()
        {        
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
        public void Create(Article article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_CREATEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
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
        public void Delete(Article article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_DELETEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = article.Id;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permet de lire un ou plusieur article de la base de données
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Article> Read(String filter)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            IList<Article> articles = new List<Article>();

            cmd.CommandText = STR_CREATEARTICLEPROC;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ServiceDB.Instance.SqlConnection;

            cmd.Parameters.Add("@Filter", SqlDbType.VarChar).Value = filter;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Article article = new Article
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
        public void Update<Object>(Article article)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_UPDATEARTICLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = article.Id;
            if(!String.IsNullOrEmpty(article.Description)) cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = article.Description;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = article.Name;
            cmd.Parameters.Add("@Space", SqlDbType.Float).Value = article.Space;

            cmd.ExecuteNonQuery();
        }
              
        #endregion "Methods"
    }
}