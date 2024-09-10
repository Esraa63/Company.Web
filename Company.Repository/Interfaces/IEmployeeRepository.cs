using Company.Data.Entitis;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepoistory<Employee>
    {
        IEnumerable<Employee> GetEmployeeByName (string name);
        IEnumerable<Employee> GetEmployeesByAddress (string address);
    }
}
