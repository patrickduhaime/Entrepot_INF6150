using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Business
{
    public class Article : IDataErrorInfo, INotifyPropertyChanged
    {
        #region "Members"

        //Membre privé représentant la catégorie de l'article
        private Category m_Category;

        //Membre privé représentant la description de l'article
        private string m_Description;

        //Membre privé représentant l'id de l'article
        private int m_Id;

        //Membre privé représentant le nom de l'article
        private string m_Name;

        //Membre privé représentant l'event de la modification d'une property
        public event PropertyChangedEventHandler PropertyChanged;

        //Membre privé représentant la liste des éxemplaire de l'article
        private IList<Sample> m_Samples;

        //Membre privé représentant l'éspace necessaire pour stocker une unité de l'article
        private float m_Space;

        //Membre privé représentant la liste des erreurs de validation
        private IList<String> m_Erros;
       
        public Article()
        {
            m_Erros = new List<String>();
        }
        #endregion "Members"

        #region "Properties"

        /// <summary>
        /// Property représente la catégorie de l'article
        /// </summary>
        public Category Category
        {
            get { return m_Category; }
            set
            {
                m_Category = value;
                OnPropertyChanged("Category");
            }
        }

        /// <summary>
        /// Property représente la description de l'article
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set
            {
                m_Description = value;
                OnPropertyChanged("Description");
            }
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
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Property représente la liste des éxemplarie d'un article
        /// </summary>
        public IList<Sample> Samples
        {
            get { return m_Samples; }
            set { m_Samples = value; }
        }

        /// <summary>
        /// Property représente l'éspace necessaire pour stocker une unité de l'article
        /// </summary>
        public float Space
        {
            get { return m_Space; }
            set
            {
                m_Space = value;
                OnPropertyChanged("Space");
            }
        }

        /// <summary>
        /// Property qui permet de valider l'objet
        /// </summary>
        public bool IsValid
        {
            get { return m_Erros.Count == 0; }
        }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Retourn l'erreur de l'objet s'il a une regle brisé
        /// </summary>
        public string Error
        {           
            get
            {
                string errorMsg = String.Empty;
                foreach(String str in m_Erros)
                {
                    if (String.IsNullOrEmpty(errorMsg)) errorMsg = str;
                    else errorMsg += Environment.NewLine + str;
                }
                return errorMsg;
            }
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
                        {
                            errorMsg = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
                            if (!m_Erros.Any(str => str.Equals(errorMsg))) m_Erros.Add(errorMsg);
                        }
                        else m_Erros.Remove(Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR));
                        break;

                    case "Space":
                        if (Space <= 0)
                        {
                            errorMsg = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLESPACEERROR);
                            if (!m_Erros.Any(str => str.Equals(errorMsg))) m_Erros.Add(errorMsg);
                        }
                        else m_Erros.Remove(Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLESPACEERROR));
                        break;

                    case "Category":
                        if (Category == null)
                        {
                            errorMsg = String.Format(Ressource.Instance.GetTraduction(ResourcesConstant.STR_CANTBENOTHING), "Category");
                            if(!m_Erros.Any(str => str.Equals(errorMsg))) m_Erros.Add(errorMsg);
                        }
                        else m_Erros.Remove(String.Format(Ressource.Instance.GetTraduction(ResourcesConstant.STR_CANTBENOTHING), "Category"));
                        break;
                }
                return errorMsg;
            }
        }

        public override string ToString()
        {
            if (Id == 0) return GetType().Name;
            else return GetType().Name + Id + " - " + Name;
        }

        #endregion "Methods"

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