using BulkyWeb.Data;
using BulkyWeb.Models;
using Dapper;

namespace BulkyWeb.Repository
{
    public class CategoryRepository(AppDbContext context) : IRepository<Category>
    {
        private readonly AppDbContext _context = context;

        public async Task Create(Category entity)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            INSERT INTO Categories (Name, DisplayOrder, CreatedDateTime)
            VALUES (@Name, @DisplayOrder, @CreatedDateTime)
            """;
            await connection.ExecuteAsync(sql, entity);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            DELETE FROM Categories
            WHERE Id = @Id;
            """;
            await connection.ExecuteAsync(sql, new { Id = id });

        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = """
            SELECT * FROM Categories;
            """;
            return await connection.QueryAsync<Category>(sql);
        }

        public async Task<Category?> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            SELECT * FROM Categories 
            WHERE Id = @Id;
            """;
            return await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
        }

        public async Task Update(Category entity)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            UPDATE Categories 
            SET Name = @Name, DisplayOrder = @DisplayOrder, CreatedDateTime = @CreatedDateTime 
            WHERE Id = @Id;
            """;
            await connection.ExecuteAsync(sql, entity);

        }
    }
}
