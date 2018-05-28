using Business;
using EntrepotScreen.Tools;
using System.Collections.ObjectModel;

namespace EntrepotScreen.Controler
{
    internal class CreateOrEditProductModel : ViewModelBase
    {
        #region "Property"

        //Propertie pour représenter l'article à créer ou modifier
        public Article Article { get; set; }

        //Propertie pour représenter la liste des catégories dans la base de donnée
        public ObservableCollection<Category> Category { get; set; }

        //Propertie pour représenter la commande du boutton OK
        public RelayCommand OkCommand { get; set; }

        //Propertie pour représenter la commande du boutton Cancel
        public RelayCommand CancelCommand { get; private set; }

        #endregion "Property"

        #region "Constructor"

        public CreateOrEditProductModel()
        {
            Article = new Article();
            Category = new ObservableCollection<Category>();
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