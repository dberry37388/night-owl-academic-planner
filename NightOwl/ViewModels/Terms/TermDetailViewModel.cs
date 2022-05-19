using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NightOwl.Models;
using Xamarin.Forms;

namespace NightOwl.ViewModels.Terms
{
    public class TermDetailViewModel
    {
        private readonly ITermStore _termStore;
        private readonly IPageService _pageService;

        public Term Term { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public TermDetailViewModel(TermViewModel viewModel, ITermStore termStore, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _termStore = termStore;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Term = new Term
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
            };
        }

        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Term.Title))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name for this term.", "OK");
                return;
            }

            if (Term.Id == 0)
            {
                await _termStore.AddTerm(Term);
                MessagingCenter.Send(this, Events.TermAdded, Term);
            }
            else
            {
                await _termStore.UpdateTerm(Term);
                MessagingCenter.Send(this, Events.TermUpdated, Term);
            }
            await _pageService.PopAsync();
        }
    }
}
