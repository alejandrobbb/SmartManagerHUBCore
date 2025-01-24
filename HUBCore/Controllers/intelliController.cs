using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HUBCore.Models;
using HUBCore.Services;
using System.Collections.ObjectModel;
using System;

namespace HUBCore.Controllers
{
    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("[controller]")]
    public class intelliController : ControllerBase
    {

        [HttpGet("get")]
        public Collection<intelliModel> getCollectionIntelli()
        {
            intelliService _service = new ();
            try
            {
                dynamic responseJson = _service.getCollectionIntelli();
                return responseJson;
            }
            catch (Exception)
            {
                return new Collection<intelliModel>();
            }
        }

        [HttpPost("create")]
        public IActionResult insertIntelliRecord([FromBody] intelliModel model)
        {
            intelliService _service = new();
            try
            {
                bool isInserted = _service.insertIntelliRecord(model);
                if (isInserted)
                {
                    return Ok(new { message = "Registro insertado exitosamente" });
                }
                else
                {
                    return BadRequest(new { message = "Error al insertar el registro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

    }
}