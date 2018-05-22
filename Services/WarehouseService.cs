
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
    public class WarehouseService : IDisposable
    {
        #region "Constants"

        private const string STR_CREATEWAREHOUSEPROC = "";
        private const string STR_DELETEWAREHOUSEPROC = "";
        private const string STR_READWAREHOUSEPROC = "";
        private const string STR_UPDATEWAREHOUSEPROC = "";

        #endregion "Constants"

        #region "Membres"

        //Membre privé représente l'instance de ressource
        private static WarehouseService m_Instance;

        //Membre privé représente la connection à la base de donnée
        private SqlConnection m_SqlConnection;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #endregion "Membres"

        #region "Constructor"

        private WarehouseService()
        {
            m_SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AppUser"].ConnectionString);
            m_SqlConnection.Open();
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Permet d'instancer et retourner Ressource
        /// </summary>
        public static WarehouseService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new WarehouseService();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// Permet de créer un entrepot dans la base de données et setter son ID générer
        /// </summary>
        /// <param name="warehouse"></param>
        public void Create(Warehouse warehouse)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_CREATEWAREHOUSEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            if (!String.IsNullOrEmpty(warehouse.Name)) cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = warehouse.Name;
            cmd.Parameters.Add("@Address", SqlDbType.Float).Value = warehouse.Address;
            cmd.Parameters.Add("@RowTotal", SqlDbType.Float).Value = warehouse.RowTotal;

            warehouse.Id = (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Permet de suppriemer un entrepot de la base de données
        /// </summary>
        /// <param name="warehouse"></param>
        public void Delete(Warehouse warehouse)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_DELETEWAREHOUSEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = warehouse.Id;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Permet de lire un ou plusieur entrepots de la base de données
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Warehouse> Read(String filter)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            IList<Warehouse> warehouses = new List<Warehouse>();

            cmd.CommandText = STR_CREATEWAREHOUSEPROC;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = m_SqlConnection;

            cmd.Parameters.Add("@Filter", SqlDbType.VarChar).Value = filter;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {                 /*
                Warehouse warehouse = new Warehouse();
                {
                    Id = (int)reader["id"],
                    Name = (String)reader["name"],
                    Address = (String)reader["address"],    
                    RowTotal = (int)reader["rowtotal"],
                    ColumnTotal = (int)reader["ColumnTotal"],
                    FloorTotal = (int)reader["FloorTotal"]


                };
                warehouses.Add(warehouse);      */
            }

            return warehouses;
        }

        /// <summary>
        /// Permet de mettre à jours un warehouse dans la base de données
        /// </summary>
        /// <param name="warehouse"></param>
        public void Update(Warehouse warehouse)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = STR_UPDATEWAREHOUSEPROC,
                CommandType = CommandType.StoredProcedure,
                Connection = m_SqlConnection
            };

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = warehouse.Id;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = warehouse.Name;
            cmd.Parameters.Add("@Address", SqlDbType.Float).Value = warehouse.Address;
            cmd.Parameters.Add("@RowTotal", SqlDbType.Float).Value = warehouse.RowTotal;

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