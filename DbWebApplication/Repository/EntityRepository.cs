using DbWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbWebApplication.Models;

namespace DbWebApplication.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<IEnumerable<StudentModel>> FindByConditionAsync(Expression<Func<StudentModel, bool>> predicate)
        {
            return await _context.Set<StudentModel>()
                .Where(predicate) // Фільтруємо за лямбда-виразом
                .Include(s => s.Subjects) // Завантажуємо предмети
                .ThenInclude(sub => sub.LabWorks) // Завантажуємо лабораторні роботи для кожного предмета
                .ThenInclude(lab => lab.LabWorkGrades) // Завантажуємо оцінки лабораторних робіт
                .ToListAsync(); // Отримуємо результати
        }


        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}