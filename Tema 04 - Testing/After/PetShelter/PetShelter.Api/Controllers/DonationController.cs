using Microsoft.AspNetCore.Mvc;
using PetShelter.BusinessLayer;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Tests;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DonationController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;
    private readonly IDonationService _donationService;

    public DonationController(IDonationService donationService, ILogger<DonationController> logger)
    {
        _donationService = donationService;
        _logger = logger;
    }

    [HttpPost("AddDonation")]
    public async Task<IActionResult> AddDonation([FromBody] AddDonationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _donationService.AddDonation(request);
            return Ok();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error adding donation: {Message}", ex.Message);
            return BadRequest();
        }
       
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Donation>> Get(int id)
    {
        var foundDonation = await _donationService.GetDonation(id);
        if (foundDonation is null)
        {
            return NotFound();
        }
        return Ok(foundDonation);
    }

    [HttpPut("UpdateDonation/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateDonation(int id, [FromBody] UpdateDonationRequest updateDonationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _donationService.UpdateDonation(id, updateDonationRequest);
            return Ok();
        }
        catch(ArgumentException ex)
        {
            return BadRequest("Invalid id for resource");
        }
    }

    

    [HttpGet("GetAllDonations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<Donation>>> GetAllDonations()
    {
        return Ok(await _donationService.GetAllDonations());
    }

    [HttpOptions]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Options()
    {
        Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
        return Ok();
    }
}
