using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Models;

namespace HeadHunterTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        List<User> Users { get; }

        /// <summary>
        /// Добавляет пользователя в хранилище
        /// </summary>
        /// <param name="userModel">Модель пользователя для добавления(скорей всего админ)</param>
        /// <returns>Id нового пользователя</returns>
        Task<Guid> AddAsync(UserRegisterModel userModel);

        /// <summary>
        /// Добавляет соискателя в хранилище
        /// </summary>
        /// <param name="jobSeekerModel">Модель соискателя для добавления</param>
        /// <returns></returns>
        Task<Guid> AddAsync(JobSeekerRegisterModel jobSeekerModel);

        /// <summary>
        /// Добавляет нанимателя в хранилище
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(EmployerRegisterModel employerModel);

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id">Id удаляемого пользователя</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAsync();
    }
}
