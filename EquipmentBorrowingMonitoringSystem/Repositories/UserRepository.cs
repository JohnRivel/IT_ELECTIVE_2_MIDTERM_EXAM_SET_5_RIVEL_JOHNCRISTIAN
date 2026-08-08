using EquipmentBorrowingMonitoringSystem.Models;

namespace EquipmentBorrowingMonitoringSystem.Repositories
{
    public class UserRepository
    {
        private static readonly List<User> users = new();

        private static int nextId = 1;

        public void Add(User user)
        {
            user.Id = nextId++;
            users.Add(user);
        }

        public User? GetByUsername(string username)
        {
            return users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateLogin(string username, string password)
        {
            return users.Any(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);
        }
    }
}