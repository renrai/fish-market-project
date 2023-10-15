using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fish_market_project.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FishController : Controller
    {
        private readonly IFishService _serviceFish;
        public FishController(IFishService serviceFish)
        {
            _serviceFish = serviceFish;

        }
        /// <summary>
        /// Get all Fishs
        /// </summary>
        /// <response code="200">List of all fishs</response>
        /// <response code="404">No fish found</response>
        /// <response code="500">Error to get all fishs</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _serviceFish.Get();

            if (clients == null || clients.Count() == 0)
                return NotFound(new { message = "Fishs not found." });
            return Ok(clients);
        }
        /// <summary>
        /// Upload a photo for a fish
        /// </summary>
        /// <param name="id">Fish Id</param>
        /// <response code="200">Photo Uploaded</response>
        /// <response code="404">No fish found</response>
        /// <response code="500">Error to upload photo</response>
        [HttpPut("upload-photo/{id}")]
        public async Task<IActionResult> UploadPhoto(IFormFile file,Guid id)
        {
            var result = await _serviceFish.UploadPhoto(file,id);

            if (result is false)
                return NotFound(new { message = "Fish not found" });

            return Ok(result);
        }

        /// <summary>
        /// Creates a fish
        /// </summary>
        /// <param name="fish">Fish data</param>
        /// <returns>Fish successfully created</returns>
        /// <response code="200">Fish object</response>
        /// <response code="400">Fish invalid data</response>
        /// <response code="401">Unathorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] FishRequest fish)
        {
            var response = await _serviceFish.Post(fish);
            return Ok(response);
        }
        /// <summary>
        /// Update a client
        /// </summary> 
        /// <param name="id">Client id to be updated</param>
        /// <param name="Client">Client data</param>
        /// <response code="200">Client successfully updated</response>
        /// <response code="400">Client invalid data</response>
        /// <response code="401">Unathorized</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Error to update client</response>
        [HttpPut]
        public async Task<IActionResult> UpdateFish([FromBody] FishRequest Client)
        {
            var response = await _serviceFish.Put(Client);

            if (response == null)
                return NotFound(new { message = "Client not found." });

            return Ok(response);
        }
        /// <summary>
        /// Delete a fish
        /// </summary>
        /// <param name="id">Fish id</param>
        /// <response code="200">Fish successfully deleted</response>
        /// <response code="401">Unathorized</response>
        /// <response code="404">Fish not found</response>
        /// <response code="500">Error to delete fish</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var response = await _serviceFish.Delete(id);
            if (!response)
                return NotFound(new { message = "Fish not found." });
            return Ok(true);
        }
    }
}
