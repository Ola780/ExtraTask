using Microsoft.EntityFrameworkCore;
using ZadanieDodatkowe.Data;
using ZadanieDodatkowe.DTOs;
using ZadanieDodatkowe.Exceptions;
using ZadanieDodatkowe.Models;

namespace ZadanieDodatkowe.Services;

public interface IDbService
{
   
    public Task<EventDto> CreateEvent (CreateEventDto dto);
    public Task AssignSpeakersToEvent(int eventId,AssignSpeakerDto dto);
    public Task RegisterParticipant(int eventId, int participantId);
    public Task CancelParticipantRegistration(int eventId, int participantId);
    public Task<List<EventGetDto>> GetEvents();
    public Task<List<ParticipantReportDto>> GetParticipantReport(int participantId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<EventDto> CreateEvent(CreateEventDto dto)
    {
        if (dto.Date <= DateTime.UtcNow)
            throw new NotFoundException("Events date cannot be from the past");

        var newEvent = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            MaxNumberParticipants = dto.MaxNumberParticipants
        };
        data.Events.Add(newEvent);
        await data.SaveChangesAsync();

        return new EventDto()
        {
            Id = newEvent.Id,
            Title = newEvent.Title,
            Description = newEvent.Description,
            Date = newEvent.Date,
            MaxNumberParticipants = newEvent.MaxNumberParticipants
        };
    }


    public async Task AssignSpeakersToEvent(int eventId, AssignSpeakerDto dto)
    {
        var assignedEvent = await data.Events.FirstOrDefaultAsync(ev => ev.Id == eventId);
        if (assignedEvent == null) throw new NotFoundException($"Event with id={eventId} not found.");

        foreach (var speakerId in dto.SpeakerIds)
        {
            if (await data.Events.FirstOrDefaultAsync(s => s.Id == speakerId) is null)
            {
                throw new NotFoundException($"Speaker with id={speakerId} not found.");
            }

            var hasConflict = await data.EventSpeakers
                .AnyAsync(es =>
                    es.SpeakerId == speakerId &&
                    es.Event.Date.Date == assignedEvent.Date.Date);

            if (hasConflict) throw new NotFoundException($"Speaker with id={speakerId} already has event at this date");


            var assignment = new EventSpeaker
            {
                SpeakerId = speakerId,
                EventId = assignedEvent.Id
            };
            await data.EventSpeakers.AddAsync(assignment);
            await data.SaveChangesAsync();

        }
    }




    public async Task RegisterParticipant(int eventId, int participantId)
        {
            var ev = await data.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (ev == null)
                throw new NotFoundException("Event does not exist");

            var participant = await data.Participants
                .FirstOrDefaultAsync(p => p.Id == participantId);

            if (participant == null) throw new NotFoundException("Participant does not exist");

            var alreadyRegistered = await data.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);


            if (alreadyRegistered != null)
                throw new NotFoundException("Participant is already registered");
            
            int registeredCount = await data.EventParticipants
                .CountAsync(ep => ep.EventId == eventId);

            if (registeredCount >= ev.MaxNumberParticipants)
                throw new NotFoundException("In this event there is already achieved the max number of participants");


            var registration = new EventParticipant
            {
                EventId = eventId,
                ParticipantId = participantId
            };

            data.EventParticipants.Add(registration);
            await data.SaveChangesAsync();
        }
    
    
    public async Task CancelParticipantRegistration(int eventId, int participantId)
    {
        var ev = await data.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        if (ev == null) throw new NotFoundException("Event does not exist");
        
        var now = DateTime.UtcNow;
        if ((ev.Date - now).TotalHours < 24)
            throw new NotFoundException("Attempt of cancelling the registration when there is less than 24 hours to the event");

        var assigned = await data.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);
        if (assigned == null)
            throw new NotFoundException("Participant is not registered for this event");

        data.EventParticipants.Remove(assigned);
        await data.SaveChangesAsync();
    }

    public async Task<List<EventGetDto>> GetEvents()
    {
        var now = DateTime.UtcNow;

        var events = await data.Events
            .Where(e => e.Date > now)
            .Select(e => new EventGetDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Speakers = e.EventSpeakers
                    .Select(es => es.Speaker.Name + " " + es.Speaker.Surname)
                    .ToList(),
                RegisteredParticipants = e.EventParticipants.Count,
                FreePlaces = e.MaxNumberParticipants - e.EventParticipants.Count
            })
            .ToListAsync();

        return events;
        
    }
    
    
    public async Task<List<ParticipantReportDto>> GetParticipantReport(int participantId)
    {
        var participantExists = await data.Participants
            .AnyAsync(p => p.Id == participantId);

        if (!participantExists)
            throw new NotFoundException("Participant not found.");

        var events = await data.EventParticipants
            .Where(ep => ep.ParticipantId == participantId)
            .Select(ep => new ParticipantReportDto
            {
                eventId = ep.EventId,
                eventTitle = ep.Event.Title,
                eventDate = ep.Event.Date,
                speakerSurnames = ep.Event.EventSpeakers.Select(es => es.Speaker.Surname)
                    .ToList(),
            })
            .ToListAsync();

      return events;
    }
    
}
    