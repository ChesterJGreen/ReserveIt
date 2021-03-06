using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;
using Microsoft.EntityFrameworkCore;

namespace ReserveIt.Data
{
    public class MockDataLayer
    {
        public static void SeedEmptyDatabase()
        {
            // build the dependency and then create the EF context
            var options = new DbContextOptionsBuilder<ResContext>().UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ResDb;Integrated Security=True;").Options;
            ResContext context = new ResContext(options);

            var room = new ConferenceRoom()
            {
                Name = "Blue Room",
                ResourceType = Data.Reference.ResourceType.ConferenceRoom,
                BuildingName = "Taco Bell Arena",
                SeatingProvided = 30,
                ResourceTimeZone = ResourceTimeZone.MST,
                AvailableLectureDevices = new AssistedLectureDevices[] {
                    AssistedLectureDevices.microphone,
                    AssistedLectureDevices.speakers,
                    AssistedLectureDevices.podium
                    }
            };

            var room2 = new ConferenceRoom()
            {
                ResourceType = Data.Reference.ResourceType.ConferenceRoom,
                BuildingName = "Nampa Public Library",
                Name = "Lecture Hall",
                AvailableLectureDevices = new AssistedLectureDevices[] {
                    AssistedLectureDevices.projector,
                    AssistedLectureDevices.microphone,
                    AssistedLectureDevices.podium
                },
                ResourceTimeZone = ResourceTimeZone.MST,
                SeatingProvided = 50
                

            };
            var room3 = new ConferenceRoom()
            {

                BuildingName = "Stella's",
                Name = "Outdoor Patio",
                AvailableLectureDevices = new AssistedLectureDevices[]
               {
                    AssistedLectureDevices.speakers,
                    AssistedLectureDevices.microphone
               },
                ResourceTimeZone = ResourceTimeZone.MST,
                SeatingProvided = 20
            };
            var ef = context.ConferenceRooms.Add(room);
            var ef2 = context.ConferenceRooms.Add(room2);
            var ef3 = context.ConferenceRooms.Add(room3);

            var res = context.Reservations.Add(new Reservation()
            {
                ConferenceRoom = room,
                StartDateTime = DateTime.Now.AddHours(-1),
                EndDateTime = DateTime.Now,

            });
            var res2 = context.Reservations.Add(new Reservation()
            {
                ConferenceRoom = room2,
                StartDateTime = DateTime.Now.AddHours(-5),
                EndDateTime = DateTime.Now.AddHours(-3)
            });
            var res3 = context.Reservations.Add(new Reservation()
            {
                ConferenceRoom = room3,
                StartDateTime = DateTime.Now.AddHours(-1),
                EndDateTime = DateTime.Now.AddHours(+1)
            });
            context.SaveChanges();
        }


     

    }
}
