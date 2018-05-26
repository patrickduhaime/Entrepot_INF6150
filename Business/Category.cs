using System;
using System.ComponentModel;

namespace Business
{
    public class Category : IDataErrorInfo, INotifyPropertyChanged
    {
        #region "Members"

        //Membre privé représentant l'id de la catégorie
        private int m_Id;

        //Membre privé représentant le nom de la catégorie
        private string m_Name;

        //Membre privé représentant l'event de la modification d'une property
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion "Members"

        #region "Properties"

        /// <summary>
        /// Property représente l'id de la catégorie
        /// </summary>
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        /// <summary>
        /// Property représente le nom de la catégorie
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
            }
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
                }
                return errorMsg;
            }
        }

        #endregion "Properties"

        #region "Events"

        /// <summary>
        /// Event se leve au changement des properties
        /// </summary>
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion "Events"
    }
}