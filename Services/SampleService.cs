using Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services
{
    internal class SampleService
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

        #endregion "Membres"

        #region "Constructor"

        private SampleService()
        {
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
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Value = sample.Article.Id;
            if (sample.Quantity.HasValue) cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = sample.Quantity;
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
                Connection = ServiceDB.Instance.SqlConnection
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
            cmd.Connection = ServiceDB.Instance.SqlConnection;

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
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = sample.Id;
            cmd.Parameters.Add("@ArticleId", SqlDbType.VarChar).Value = sample.Article.Id;
            if (sample.Quantity.HasValue) cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = sample.Quantity;
            cmd.Parameters.Add("@SerialNumber", SqlDbType.Float).Value = sample.SerialNumber;

            cmd.ExecuteNonQuery();
        }

        #endregion "Methods"
    }
}