//using Microsoft.AspNetCore.Identity;
//using TaskManager.Application.Interfaces;
//using TaskManager.Domain.Entities;
//using TaskManager.Domain.Repositories;

//namespace TaskManager.Application.Services
//{
//    public class UserService(IUserRepository userRepository) : IUserService
//    {
//        public async Task<User?> CreateAsync(User user)
//        {
//            if (user == null)
//                throw new ArgumentNullException(nameof(user), "User object cannot be null.");

//            if (user.PasswordHash == null)
//                throw new InvalidOperationException($"Password cannot be null.");

//            var existingUser = await userRepository.GetByEmailAsync(user.Email);
//            if (existingUser != null)
//                throw new InvalidOperationException($"A user with the email '{user.Email}' already exists.");
                        
//            user.Id = Guid.NewGuid();            

//            var passwordHasher = new PasswordHasher<User>();
//            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);

//            try
//            {
//                return await userRepository.AddAsync(user);
//            }
//            catch (Exception ex)
//            {
//                throw new ApplicationException("Error creating the user", ex);
//            }
//        }

//        public async Task<bool> DeleteAsync(Guid id)
//        {
//            var user = await userRepository.GetByIdAsync(id) 
//                ?? throw new ApplicationException("User delete failed. User not found.");

//            await userRepository.DeleteAsync(user);

//            return true;
//        }

//        public Task<IEnumerable<User>> GetAllAsync()
//        {
//            return userRepository.GetAllAsync();
            
//        }

//        public Task<User?> GetByIdAsync(Guid id)
//        {
//            var user = userRepository.GetByIdAsync(id);
//            return user ?? throw new ApplicationException("User not found.");
//        }

//        public async Task<User?> UpdateAsync(User user)
//        {
//            _ = await userRepository.GetByIdAsync(user.Id)
//                ?? throw new ApplicationException("User update failed. User not found.");
//            try
//            {
//                var userUpdated = await userRepository.UpdateAsync(user);
//                return userUpdated;
//            }
//            catch (Exception ex)
//            {
//                throw new ApplicationException("Error updating the user", ex);
//            }            
//        }
//    }
//}
