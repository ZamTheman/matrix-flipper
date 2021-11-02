using MatrixFlipperAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatrixFlipperAPI.DAL
{
    public interface IMatrixDataAccess
    {
        Task<Matrix> GetMatrixAsync(string name);
        Task<IEnumerable<Matrix>> GetAllMatricesAsync();
        Task UpdateMatrixAsync(Matrix matrix);
        Task CreateMatrixAsync(Matrix matrix);
        Task DeleteMatrixAsync(string name);
    }
}
