using System.Collections.Generic;
using System.Threading.Tasks;
using NightOwl.Models;

namespace NightOwl.ViewModels.Terms
{
    public interface ITermStore
    {
        Task<IEnumerable<Term>> GetTermsAsync();

        Task<Term> GetTerm(int id);

        Task AddTerm(Term term);

        Task UpdateTerm(Term term);

        Task DeleteTerm(Term term);
    }
}
