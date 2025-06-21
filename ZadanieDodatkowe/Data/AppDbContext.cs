using Microsoft.EntityFrameworkCore;
using ZadanieDodatkowe.Models;

namespace ZadanieDodatkowe.Data;

public class AppDbContext :DbContext
{

    public DbSet<Participant> Participants { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Participant>().HasData(
            new Participant { Id = 1, Name = "Magda",  Surname = "Baczynska"},
            new Participant { Id = 2, Name = "Maks",   Surname = "Mazur"},
            new Participant { Id = 3, Name = "Czarek",Surname = "Krolikowski"},
            new Participant { Id = 4, Name = "Jan",Surname = "Sosna"}
        );

        modelBuilder.Entity<Speaker>().HasData(
            new Speaker { Id = 1, Name = "Alice",  Surname = "Bobrowska"},
            new Speaker { Id = 2, Name = "Mark",   Surname = "Malinowski"},
            new Speaker { Id = 3, Name = "Charles",Surname = "Krolik"},
            new Speaker { Id = 4, Name = "Bob",Surname = "Kopulski"}
        );

        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Title = "Konf. kosmiczna", Description = "Nauka o kosmosie", Date = new DateTime(2026,1,1), MaxNumberParticipants = 3 },
            new Event { Id = 2, Title = "Warsztat AI",     Description = "Sztuczna inteligencja", Date = new DateTime(2026,3,15), MaxNumberParticipants = 10 },
            new Event { Id = 3, Title = "Konf. bioTech",   Description = "Nowe terapie", Date = new DateTime(2026,5,20), MaxNumberParticipants = 10 }
        );

        modelBuilder.Entity<EventParticipant>().HasData(
            new EventParticipant { EventId = 1, ParticipantId = 1 },
            new EventParticipant { EventId = 1, ParticipantId = 2 },
            new EventParticipant { EventId = 2, ParticipantId = 3 },
            new EventParticipant { EventId = 3, ParticipantId = 4 }
        );

        modelBuilder.Entity<EventSpeaker>().HasData(
            new EventSpeaker { EventId = 1, SpeakerId = 1 },
            new EventSpeaker { EventId = 1, SpeakerId = 2 },
            new EventSpeaker { EventId = 2, SpeakerId = 3 },
            new EventSpeaker { EventId = 3, SpeakerId = 4 }
        );
    }

}