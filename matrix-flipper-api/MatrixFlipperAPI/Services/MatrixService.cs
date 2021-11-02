using MatrixFlipperAPI.DAL;
using MatrixFlipperAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatrixFlipperAPI.Services
{
    public class MatrixService : IMatrixService
    {
        private IMatrixDataAccess matrixDataAccess;

        public MatrixService(IMatrixDataAccess matrixDataAccess)
        {
            this.matrixDataAccess = matrixDataAccess;
        }

        public async Task DeleteMatrixAsync(string name)
        {
            await matrixDataAccess.DeleteMatrixAsync(name);
        }

        public async Task<IEnumerable<Matrix>> GetAllMatricesAsync()
        {
            return await matrixDataAccess.GetAllMatricesAsync();
        }

        public async Task<Matrix> GetMatrixByNameAsync(string name)
        {
            return await matrixDataAccess.GetMatrixAsync(name);
        }

        public async Task SaveMatrixAsync(Matrix matrix)
        {
            if (await matrixDataAccess.GetMatrixAsync(matrix.Name) == null)
                await matrixDataAccess.CreateMatrixAsync(matrix);
            else
                await matrixDataAccess.UpdateMatrixAsync(matrix);
        }
    }
}
