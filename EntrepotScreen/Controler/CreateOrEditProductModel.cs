using Business;
using EntrepotScreen.Tools;
using Resources;
using System.Collections.Generic;

namespace EntrepotScreen.Controler
{
    internal class CreateOrEditProductModel : ViewModelBase
    {
        #region "Members"

        //Membre privé représentant l'article à créer ou modifier
        private Article m_Article;

        #endregion "Members"

        #region "Property"

        //Propertie pour représenter l'article à créer ou modifier
        public Article Article
        {
            get
            {
                if (m_Article == null) return new Article();
                return m_Article;
            }
            set
            {
                m_Article = value;
            }
        }

        //Propertie pour représenter la liste des catégories dans la base de donnée
        public IList<Category> Category { get; set; }

        //Propertie pour représenter la commande du boutton OK
        public RelayCommand OkCommand { get; set; }

        //Propertie pour représenter la commande du boutton Cancel
        public RelayCommand CancelCommand { get; private set; }

        #endregion "Property"

        #region "Constructor"

        public CreateOrEditProductModel()
        {
            Category = new List<Category>();
            //CategoryService.Instance.Read(null).ToList().ForEach(c => Category.Add(c));
            //JUSTE UN EXAMPLE.
            Category.Add(new Category
            {
                Id = 1,
                Name = "c1"
            });
            Category.Add(new Category
            {
                Id = 2,
                Name = "c2"
            });

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Methode qui s'éxecute au clique sur le OK
        /// Permet de créer ou sauvgarde l'article
        /// </summary>
        /// <param name="parameter"></param>
        public void Ok(object parameter)
        {
            if (!Article.IsValid)
            {
                System.Windows.MessageBox.Show(Article.Error, Ressource.Instance.GetTraduction(ResourcesConstant.STR_IMPOSSIBLETOSAVE)
                    , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            /* PAS ENCORE FAITE COTE BD
             * if (Article.Id == 0) ArticleService.Instance.Create(Article);
            else ArticleService.Instance.Update(Article);   */
            //TODO : UPDATE CALLER ARTICLE LIST

            CloseWindow();
        }

        /// <summary>
        /// Methode qui s'éxecute au clique sur le cancel
        /// Permet de fermer l'écran
        /// </summary>
        /// <param name="parameter"></param>
        public void Cancel(object parameter)
        {
            CloseWindow();
        }

        #endregion "Methods"
    }
}