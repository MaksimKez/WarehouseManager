using Microsoft.EntityFrameworkCore;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDatabaseContext _context;

        public EmployeeRepository(ApplicationDatabaseContext context)
        {
            _context = context ?? throw new ArgumentException("Context error", nameof(context));
        }

        public async Task<EmployeeEntity> GetByIdAsync(Guid id)
        {
            var entity = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                throw new ArgumentException("Employee with given Id is not found", nameof(id));
            
            return entity;
        }

        public async Task<EmployeeEntity> GetByEmailAsync(string email)
        {
            var entity = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
            if (entity == null)
                throw new ArgumentException("Employee with given email is not found", nameof(email));
            
            return entity;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
        {
            var entities = await _context.Employees.AsNoTracking().ToListAsync();
            if (entities == null || !entities.Any())
                throw new ArgumentException("No employees found");
            
            return entities;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByPositionAsync(PositionEnum position)
        {
            var entities = await _context.Employees.AsNoTracking()
                .Where(e => e.Position == position).ToListAsync();
            if (entities == null || !entities.Any())
                throw new ArgumentException("No employees found with the given position", nameof(position));
            
            return entities;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetBySurnameAsync(string surname)
        {
            var entities = await _context.Employees.AsNoTracking()
                .Where(e => e.Surname == surname).ToListAsync();
            if (entities == null || !entities.Any())
                throw new ArgumentException("No employees found with the given surname", nameof(surname));
            
            return entities;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByIsFiredStatusAsync(bool isFired)
        {
            var entities = await _context.Employees.AsNoTracking()
                .Where(e => e.IsFired == isFired).ToListAsync();
            if (entities == null || !entities.Any())
                throw new ArgumentException("No employees found with the given fired status", nameof(isFired));
            
            return entities;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate)
        {
            var entities = await _context.Employees.AsNoTracking()
                .Where(e => e.CreatedAt >= startDate && e.CreatedAt <= endDate).ToListAsync();
            if (entities == null || !entities.Any())
                throw new ArgumentException("No employees found in the given date range", nameof(startDate));
            
            return entities;
        }

        public async Task AddAsync(EmployeeEntity employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeEntity employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            var existingEmployee = await _context.Employees.
                AsNoTracking().FirstOrDefaultAsync(b => b.Id == employee.Id);
            if (existingEmployee == null)
                throw new ArgumentException("Employee with given Id is not found", nameof(employee.Id));

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (employee == null)
                throw new ArgumentException("Employee with given Id is not found", nameof(id));

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
