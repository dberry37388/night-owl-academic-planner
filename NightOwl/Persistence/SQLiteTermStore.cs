using System.Collections.Generic;
using System.Threading.Tasks;
using NightOwl.Models;
using NightOwl.ViewModels.Terms;
using SQLite;

namespace NightOwl.Persistence
{
    public class SQLiteTermStore: ITermStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLiteTermStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Term>();
        }

        public async Task<IEnumerable<Term>> GetTermsAsync()
        {
            return await _connection.Table<Term>().ToListAsync();
        }

        public async Task DeleteTerm(Term term)
        {
            await _connection.DeleteAsync(term);
        }

        public async Task AddTerm(Term term)
        {
            await _connection.InsertAsync(term);
        }

        public async Task UpdateTerm(Term term)
        {
            await _connection.UpdateAsync(term);
        }

        public async Task<Term> GetTerm(int id)
        {
            return await _connection.FindAsync<Term>(id);
        }
    }
}
