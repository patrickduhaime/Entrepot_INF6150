using Business;
using System;
using System.ComponentModel;

namespace EntrepotScreen.Model
{
    internal class CreateProductModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Article m_Article;

        public Article Article
        {
            get { return m_Article; }
            set
            {
                m_Article = value;
                OnPropertyChanged("Article");
            }
        }

        public CreateProductModel()
        {
            m_Article = new Article();
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}