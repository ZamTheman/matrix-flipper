using MatrixFlipperAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatrixFlipperAPI.Services
{
    public interface IMatrixService
    {
        Task<Matrix> GetMatrixByNameAsync(string name);
        Task<IEnumerable<Matrix>> GetAllMatricesAsync();
        Task SaveMatrixAsync(Matrix matrix);
        Task DeleteMatrixAsync(string name);
    }
}
