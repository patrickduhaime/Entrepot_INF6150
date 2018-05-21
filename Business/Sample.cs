using System;
using System.ComponentModel;

namespace Business
{
    public class Sample : IDataErrorInfo, INotifyPropertyChanged
    {
        #region "Members"

        //Membre privé représentant l'article de l'exemplaire
        private Article m_Article;

        //Membre privé représentant l'id de l'exemplarie
        private int m_Id;

        //Membre privé représentant la quantité des unité que le conteneur contient
        private int? m_Quantity;

        //Membre privé représentant le code à bare de l'article
        private string m_SerialNumber;

        //Membre privé représentant l'event de la modification d'une property
        private event PropertyChangedEventHandler M_PropertyChanged;

        #endregion "Members"

        #region "Properties"

        /// <summary>
        /// Property représente l'article de l'éxemplaire
        /// </summary>
        public Article Article
        {
            get { return m_Article; }
            set { m_Article = value; }
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
        /// Property représente la quantité des unité que le conteneur contient
        /// </summary>
        public int? Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        /// <summary>
        /// Property représente le code à bare de l'article
        /// </summary>
        public string SerialNumber
        {
            get { return m_SerialNumber; }
            set { m_SerialNumber = value; }
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
                    case "SerialNumber":
                        if (String.IsNullOrEmpty(SerialNumber) || SerialNumber.Length < 4 || SerialNumber.Length > 50)
                            errorMsg = Resources.Ressource.Instance.GetTraduction(Resources.ResourcesConstant.STR_BARECODESIZEERROR);
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