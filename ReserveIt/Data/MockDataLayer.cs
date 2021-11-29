using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;

namespace ReserveIt.Data
{
    public class MockDataLayer
    {
        public static void SeedEmptyDatabase()
        {
            ResContext context = new ResContext();

            var room = new ConferenceRoom()
            {
                Name = "Blue Room",
                ResourceType = Data.Reference.ResourceType.ConferenceRoom,
                BuildingName = "Taco Bell Arena",
                SeatingProvided = 30,
                ResourceTimeZone = ResourceTimeZone.MST,
                CreatedDate = DateTime.Now,
                AvailableLectureDevices = new AssistedLectureDevices[]
                {
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
                SeatingProvided = 50,
                CreatedDate = DateTime.Now.AddHours(-1)

            };
            var ef = context.ConferenceRooms.Add(room);
            var ef2 = context.ConferenceRooms.Add(room2);

            var res = context.Reservations.Add(new Reservation()
            {
                ConferenceRoom = room,
                StartDateTime = DateTime.Now.AddHours(-1),
                EndDateTime = DateTime.Now,

            });
            context.SaveChanges();
        }
        private static List<ConferenceRoom> result = new List<ConferenceRoom>();
        public static void init()
        {
            result.Add(
                new ConferenceRoom()

                );
            result.Add(
                new ConferenceRoom()
                {

                    Id = 2,
                    BuildingName = "Stella's",
                    Name = "Outdoor Patio",
                    Location = "Down-Town Nampa",
                    AvailableLectureDevices = new AssistedLectureDevices[]
                {
                    AssistedLectureDevices.speakers,
                    AssistedLectureDevices.microphone
                },
                    ResourceTimeZone = ResourceTimeZone.MST,
                    SeatingProvided = 20
                });
            Reservation[] reservations = new Reservation[2];
            reservations[0] = new Reservation()
            {
                Id = 1,
                ResourceId = 1,
                StartDateTime = DateTime.Now.AddHours(-5),
                EndDateTime = DateTime.Now.AddHours(-3)

            };
            reservations[1] = new Reservation()
            {
                Id = 2,
                ResourceId = 2,
                StartDateTime = DateTime.Now.AddHours(-1),
                EndDateTime = DateTime.Now.AddHours(+1)
            };
        }

       

        public static List<ConferenceRoom> GetConferenceRooms()
        {
            
            return result;
        }
        //TODO make the mockDataLayer process the Create/Put/Delete functionality
        public static Task<ConferenceRoom> CreateConferenceRoom(ConferenceRoom newConferenceRoom)
        {
            var resultForId = result.ToArray();
            int maxId = 0;

            //foreach( ConferenceRoom room in resultForId)
            //{
            //    if (room.Id > maxId)
            //    {
            //        maxId = room.Id;
            //    }
               
            //}

            maxId = result.Max(room => room.Id);

            result.Add(
                new ConferenceRoom()

                {
                    Id = ++maxId,
                    BuildingName = newConferenceRoom.BuildingName,
                    Name = newConferenceRoom.Name,
                    Location = newConferenceRoom.Location,
                    AvailableLectureDevices = newConferenceRoom.AvailableLectureDevices,
                    ResourceTimeZone = newConferenceRoom.ResourceTimeZone,
                    SeatingProvided = newConferenceRoom.SeatingProvided

                }) ;
            return Task.FromResult(result.Single(room => room.Id == maxId));
        }
        internal static Task<ConferenceRoom> EditConferenceRoom(int id, ConferenceRoom venue)
        {
            throw new NotImplementedException();
        }

        internal static Task<ConferenceRoom> DeleteConferenceRoom(int id)
        {
            throw new NotImplementedException();
        }
    }
}
