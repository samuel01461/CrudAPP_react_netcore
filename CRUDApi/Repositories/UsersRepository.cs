using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Repositories
{
    public class UsersRepository : IUsers
    {
        private CrudDbContext _context;
        public UsersRepository(CrudDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users.Include(u => u.UsersRoles).ToListAsync();
            return users;
        }
        public async Task<IEnumerable<User>> GetUserAsync(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).Include(u => u.UsersRoles).ToListAsync();
            return user;
        }
        public async Task<IEnumerable<User>> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).Include(u => u.UsersRoles).ToListAsync();
            return user;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return user;
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
