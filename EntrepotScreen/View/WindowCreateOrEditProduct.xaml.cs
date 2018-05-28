using EntrepotScreen.Controler;
using Resources;
using System.Windows;

namespace EntrepotScreen.View
{
    /// <summary>
    /// Interaction logic for WindowCreateOrEditProduct.xaml
    /// </summary>
    public partial class WindowCreateOrEditProduct : Window, IWindow
    {
        public WindowCreateOrEditProduct()
        {
            InitializeComponent();
            //Localise();
            (DataContext as CreateOrEditProductModel).window = this as IWindow;
        }

        /// <summary>
        /// Method qui permet d'acceder du model à la view
        /// </summary>
        /// <returns></returns>
        public Window Window()
        {
            return this; 
        }

        /// <summary>
        /// Method qui permet de traduire l'écran en cours
        /// </summary>
        public void Localise()
        {
            Title = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);

            this.lblCategory.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
            this.lblDescription.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
            this.lblName.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
            this.lblSpace.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
            this.lblTitle.Content = Ressource.Instance.GetTraduction(ResourcesConstant.STR_ARTICLENAMEERROR);
        }
    }
}