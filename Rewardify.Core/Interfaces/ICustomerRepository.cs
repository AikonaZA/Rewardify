using Rewardify.Core.Entities;

namespace Rewardify.Core.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByIdAsync(int customerId);

    Task AddCustomerAsync(Customer customer);

    Task UpdateCustomerAsync(Customer customer);
}