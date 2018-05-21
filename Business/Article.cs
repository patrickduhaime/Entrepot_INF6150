using System.ComponentModel;

namespace Business
{
    internal class Article : IDataErrorInfo, INotifyPropertyChanged
    {
        #region "Members"

        //Membre privé représentant la description de l'article
        private string m_Description;

        //Membre privé représentant l'id de l'article
        private int m_Id;

        //Membre privé représentant le nom de l'article
        private string m_Name;

        //Membre privé représentant l'éspace necessaire pour stocker une unité de l'article
        private float m_Space;

        string IDataErrorInfo.this[string columnName] => throw new System.NotImplementedException();

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

        string IDataErrorInfo.Error => throw new System.NotImplementedException();

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new System.NotImplementedException();
            }

            remove
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion "Properties"
    }
}