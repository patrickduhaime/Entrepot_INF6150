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
    class SampleService : IDisposable
    {
        #region "Constants"

        private const string STR_CREATESAMPLEPROC = "";
        private const string STR_DELETESAMPLEPROC = "";
        private const string STR_READSAMPLEPROC = "";
        private const string STR_UPDATESAMPLEPROC = "";

        private const string STR_FILTERARTICLE = "where id = {0}";

        #endregion "Constants"

        #region "Membres"

        //Membre privé représente l'instance de ressource
        private static SampleService m_Instance;

        //Membre privé représente la connection à la base de donnée
        private SqlConnection m_SqlConnection;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion "Membres"

        #region "Constructor"

        private SampleService()
        {
            m_SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            m_SqlConnection.Open();
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static SampleService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SampleService();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Permet de créer un éxemplaire dans la base de données et setter son ID générer
        /// </summary>
        /// <param name="sample"></param>
        public void Create(Sample sample)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_CREATESAMPLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Value = sample.Article.Id;
            if(sample.Quantity.HasValue) cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = sample.Quantity;
            cmd.Parameters.Add("@SerialNumber", SqlDbType.Float).Value = sample.SerialNumber;

            sample.Id = (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Permet de suppriemer un article de la base de données
        /// </summary>
        /// <param name="article"></param>
        public void Delete(Sample sampe)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_DELETESAMPLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = sampe.Id;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permet de lire un ou plusieur éxemplaire de la base de données
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Sample> Read(String filter)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            IList<Sample> samples = new List<Sample>();

            cmd.CommandText = STR_CREATESAMPLEPROC;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = m_SqlConnection;

            cmd.Parameters.Add("@Filter", SqlDbType.VarChar).Value = filter;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Sample sample = new Sample
                {
                    Article = ArticleService.Instance.Read(String.Format(STR_FILTERARTICLE, reader["articleid"]))[0],
                    Id = (int)reader["id"],
                    Quantity = (int?)reader["Quantity"],
                    SerialNumber = (string)reader["SerialNumber"]
                };
                samples.Add(sample);
            }

            return samples;
        }

        /// <summary>
        /// Permet de mettre à jours un éxemplaire dans la base de données
        /// </summary>
        /// <param name="sample"></param>
        public void Update(Sample sample)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_UPDATESAMPLEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = sample.Id;
            cmd.Parameters.Add("@ArticleId", SqlDbType.VarChar).Value = sample.Article.Id;
            if(sample.Quantity.HasValue) cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = sample.Quantity;
            cmd.Parameters.Add("@SerialNumber", SqlDbType.Float).Value = sample.SerialNumber;

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
