using EntrepotScreen.Controler;
using Resources;
using System.Windows;

namespace EntrepotScreen.View
{
    /// <summary>
    /// Interaction logic for WindowCreateOrEditProduct.xaml
    /// </summary>
    public partial class WindowCreateOrEditProduct : Window
    {
        public WindowCreateOrEditProduct()
        {
            InitializeComponent();
            Localise();                                                              
        }             

        /// <summary>
        /// Method qui permet de traduire l'écran en cours
        /// </summary>
        public void Localise()
        {                                                                                             
            Title = (DataContext as CreateOrEditProductModel).Article.ToString();
            lblCategory.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_CATEGORY);
            lblDescription.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_DESCRIPTION);
            lblName.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_NAME);
            lblSpace.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_SPACE);   
            btnCalcel.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_CANCEL);
            btnOK.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_OK);
        }
    }
}