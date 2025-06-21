using Microsoft.AspNetCore.Mvc;
using ZadanieDodatkowe.DTOs;
using ZadanieDodatkowe.Exceptions;
using ZadanieDodatkowe.Services;

namespace ZadanieDodatkowe.Controllers;

[ApiController]
[Route("api")]
public class EventController(IDbService service) : ControllerBase
{
    
   
   [HttpPost("newEvent")]
   public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEventDto dto)
   {
       try
       {
           var created = await service.CreateEvent(dto);
           
           return CreatedAtAction(nameof(CreateEvent), new { id = created.Id },created);
       }
       catch (NotFoundException ex)
       {
           return NotFound(ex.Message);
       }
   }
   
   
   [HttpPut("assign/{id}")]
   public async Task<IActionResult> AssignSpeakers([FromRoute] int id, [FromBody] AssignSpeakerDto dto)
   {
       try
       {
           await service.AssignSpeakersToEvent(id, dto);
           return NoContent();
       }
       catch (NotFoundException e)
       {
           return NotFound(e.Message);
       }
   }
   
   
   [HttpPut("register/{eventId}/participant/{participantId}")]
   public async Task<IActionResult> RegisterParticipant([FromRoute] int eventId, [FromRoute] int participantId)
   {
       try
       {
           await service.RegisterParticipant(eventId, participantId);
           return NoContent();
       }
       catch (NotFoundException e)
       {
           return NotFound(e.Message);
       }
   }
   
   
   [HttpDelete("cancellation/{participantId}/event/{eventId}")]
   public async Task<IActionResult> CancelRegistration([FromRoute] int participantId, int eventId)
   {
       try
       {
           await service.CancelParticipantRegistration(eventId, participantId);
           return Ok("Registration has been cancelled");
       }
       catch (NotFoundException e)
       {
           return NotFound(e.Message);
       }
   }
   
   [HttpGet("events")]
   public async Task<ActionResult<List<EventGetDto>>> GetEvents()
   {
       var result = await service.GetEvents();
       return Ok(result);
   }
   
   
   [HttpGet("report/{participantId}")]
   public async Task<IActionResult> GetParticipantReport(int participantId)
   {
       try
       {
           var report = await service.GetParticipantReport(participantId);
           return Ok(report);
       }
       catch (NotFoundException ex)
       {
           return NotFound(ex.Message);
       }
   }


   
}