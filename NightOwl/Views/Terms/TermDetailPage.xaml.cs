using NightOwl.Persistence;
using NightOwl.ViewModels;
using NightOwl.ViewModels.Terms;
using Xamarin.Forms;

namespace NightOwl.Views.Terms
{
    public partial class TermDetailPage : ContentPage
    {
        public TermDetailPage(TermViewModel viewModel)
        {
            InitializeComponent();
            var termStore = new SQLiteTermStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            Title = (viewModel.Title == null) ? "New Term" : "Edit Term";
            BindingContext = new TermDetailViewModel(viewModel ?? new TermViewModel(), termStore, pageService);
        }
    }
}
