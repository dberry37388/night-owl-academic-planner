using System;
using NightOwl.Models;

namespace NightOwl.ViewModels.Terms
{
    public class TermViewModel: BaseViewModel
    {
        public int Id { get; set; }

        public TermViewModel() {}

        public TermViewModel(Term term)
        {
            Id = term.Id;
            _title = term.Title;
            _startDate = term.StartDate;
            _endDate = term.EndDate;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetValue(ref _title, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetValue(ref _startDate, value);
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                SetValue(ref _endDate, value);
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string DateRange
        {
            get { return StartDate.ToString("d") + " - " + EndDate.ToString("d"); }
        }


    }
}
