namespace VeryCoolApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddRecipePage),typeof(AddRecipePage));
            Routing.RegisterRoute(nameof(AddIngridientPage), typeof(AddIngridientPage));
        }
    }
}
