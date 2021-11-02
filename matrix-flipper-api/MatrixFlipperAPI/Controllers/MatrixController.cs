using MatrixFlipperAPI.Models;
using MatrixFlipperAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MatrixFlipperAPI.Controllers
{
    public class MatrixController : Controller
    {
        IMatrixService matrixService;
        public MatrixController(IMatrixService matrixService)
        {
            this.matrixService = matrixService;
        }

        [Route("matrix")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var allMatrices = await matrixService.GetAllMatricesAsync();
                if (allMatrices != null)
                    return Ok(allMatrices);
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("matrix/{name}")]
        [HttpGet]
        public async Task<ActionResult> Get(string name)
        {
            try
            {
                var matrix = await matrixService.GetMatrixByNameAsync(name);
                if (matrix != null)
                    return Ok(matrix);
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500, $"Internal error when trying to get matrix: { name }");
            }
        }

        [HttpPost]
        [Route("matrix")]
        public async Task<ActionResult> CreateOrUpdate([FromBody]Matrix matrix)
        {
            try
            {
                await matrixService.SaveMatrixAsync(matrix);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Failed to save matrix");
            }
        }

        [HttpDelete]
        [Route("matrix/{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            try
            {
                await matrixService.DeleteMatrixAsync(name);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Failed to delete matrix");
            }
        }
    }
}
