using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NightOwl.Models;
using NightOwl.Views;
using NightOwl.Views.Terms;
using Xamarin.Forms;

namespace NightOwl.ViewModels.Terms
{
    public class TermsPageViewModel : BaseViewModel
    {
        private TermViewModel _selectedTerm;
        private ITermStore _termStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private bool _isRefreshing;

        public ObservableCollection<TermViewModel> Terms { get; private set; }
            = new ObservableCollection<TermViewModel>();

        public TermViewModel SelectedTerm
        {
            get { return _selectedTerm; }
            set { SetValue(ref _selectedTerm, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddTermCommand { get; private set; }
        public ICommand SelectTermCommand { get; private set; }
        public ICommand DeleteTermCommand { get; private set; }

        public TermsPageViewModel(ITermStore termStore, IPageService pageService)
        {
            _termStore = termStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddTermCommand = new Command(async () => await AddTerm());
            SelectTermCommand = new Command<TermViewModel>(async c => await SelectTerm(c));
            DeleteTermCommand = new Command<TermViewModel>(async c => await DeleteTerm(c));

            MessagingCenter.Subscribe<TermDetailViewModel, Term>
                (this, Events.TermAdded, OnTermAdded);

            MessagingCenter.Subscribe<TermDetailViewModel, Term>
            (this, Events.TermUpdated, OnTermUpdated);
        }

        private void OnTermAdded(TermDetailViewModel source, Term term)
        {
            Terms.Add(new TermViewModel(term));
        }

        private void OnTermUpdated(TermDetailViewModel source, Term term)
        {
            var termInList = Terms.Single(c => c.Id == term.Id);

            termInList.Id = term.Id;
            termInList.Title = term.Title;
            termInList.StartDate = term.StartDate;
            termInList.EndDate = term.EndDate;
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var contacts = await _termStore.GetTermsAsync();

            foreach (var contact in contacts)
                Terms.Add(new TermViewModel(contact));
        }

        private async Task AddTerm()
        {
            await _pageService.PushAsync(new TermDetailPage(new TermViewModel()));
        }

        private async Task SelectTerm(TermViewModel term)
        {
            if (term == null)
                return;

            SelectedTerm = null;
            await _pageService.PushAsync(new TermDetailPage(term));
        }

        private async Task DeleteTerm(TermViewModel termViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {termViewModel.Title}?", "Yes", "No"))
            {
                Terms.Remove(termViewModel);

                var contact = await _termStore.GetTerm(termViewModel.Id);
                await _termStore.DeleteTerm(contact);
            }
        }
    }
}
