﻿using System;
using System.ComponentModel;

namespace Business
{
    public class T : IDataErrorInfo, INotifyPropertyChanged
    {
        #region "Members"

        //Membre privé représentant la description de l'article
        private string m_Description;

        //Membre privé représentant l'id de l'article
        private int m_Id;

        //Membre privé représentant le nom de l'article
        private string m_Name;

        //Membre privé représentant l'event de la modification d'une property
        private event PropertyChangedEventHandler M_PropertyChanged;

        //Membre privé représentant l'éspace necessaire pour stocker une unité de l'article
        private float m_Space;

        #endregion "Members"

        #region "Properties"

        /// <summary>
        /// Property représente la description de l'article
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        /// <summary>
        /// Property représente l'id de l'article
        /// </summary>
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        /// <summary>
        /// Property représente le nom de l'article
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// Property représente l'éspace necessaire pour stocker une unité de l'article
        /// </summary>
        public float Space
        {
            get { return m_Space; }
            set { m_Space = value; }
        }

        /// <summary>
        /// Retourn l'erreur de l'objet s'il a une regle brisé
        /// </summary>
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Permet de valider les properties par rapport au regles d'affaire
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string errorMsg = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (String.IsNullOrEmpty(Name) || Name.Length < 4 || Name.Length > 50)
                            errorMsg = Resources.Ressource.Instance.GetTraduction(Resources.ResourcesConstant.STR_ARTICLENAMEERROR);
                        break;

                    case "Space":
                        if (Space <= 0) errorMsg = Resources.Ressource.Instance.GetTraduction(Resources.ResourcesConstant.STR_ARTICLESPACEERROR);
                        break;
                }
                return errorMsg;
            }
        }

        #endregion "Properties"

        #region "Events"

        /// <summary>
        /// Event se leve au changement des properties
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { M_PropertyChanged += value; }

            remove { M_PropertyChanged -= value; }
        }

        #endregion "Events"
    }
}