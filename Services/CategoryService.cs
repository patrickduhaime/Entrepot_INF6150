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
    public class CategoryService
    {
        #region "Constants"

        private const string STR_CREATECATEGORYPROC = "";
        private const string STR_DELETECATEGORYPROC = "";
        private const string STR_READCATEGORYPROC = "";
        private const string STR_UPDATECATEGORYPROC = "";

        #endregion "Constants"

        #region "Membres"

        //Membre privé représente l'instance de ressource
        private static CategoryService m_Instance;                                                                                                         

        #endregion "Membres"

        #region "Constructor"

        private CategoryService()
        {        
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static CategoryService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CategoryService();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Permet de créer une category dans la base de données et setter son ID générer
        /// </summary>
        /// <param name="category"></param>
        public void Create(Category category)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_CREATECATEGORYPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
            };
                                                                                                                                               
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = category.Name;

            category.Id = (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Permet de suppriemer une category de la base de données
        /// </summary>
        /// <param name="category"></param>
        public void Delete(Category category)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_DELETECATEGORYPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = category.Id;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permet de lire un ou plusieur category de la base de données
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Category> Read(String filter)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            IList<Category> categorys = new List<Category>();

            cmd.CommandText = STR_CREATECATEGORYPROC;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ServiceDB.Instance.SqlConnection;

            cmd.Parameters.Add("@Filter", SqlDbType.VarChar).Value = filter;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Category category = new Category
                {                                                                                         
                    Id = (int)reader["id"],
                    Name = (String)reader["name"]
                };
                categorys.Add(category);
            }

            return categorys;
        }

        /// <summary>
        /// Permet de mettre à jours une category dans la base de données
        /// </summary>
        /// <param name="category"></param>
        public void Update<Object>(Category category)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_UPDATECATEGORYPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = ServiceDB.Instance.SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = category.Id;                                                                         
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = category.Name;   

            cmd.ExecuteNonQuery();
        }
              
        #endregion "Methods"
    }
}