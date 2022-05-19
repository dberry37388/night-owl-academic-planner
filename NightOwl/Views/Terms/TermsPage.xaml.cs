using NightOwl.Persistence;
using NightOwl.ViewModels;
using NightOwl.ViewModels.Terms;
using Xamarin.Forms;

namespace NightOwl.Views.Terms
{
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            var termStore = new SQLiteTermStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new TermsPageViewModel(termStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnTermSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectTermCommand.Execute(e.SelectedItem);
        }

        public TermsPageViewModel ViewModel
        {
            get { return BindingContext as TermsPageViewModel; }
            set { BindingContext = value; }
        }
    }
}
