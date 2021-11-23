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
        public static List<ConferenceRoom> GetConferenceRooms()
        {
            List<ConferenceRoom> result = new List<ConferenceRoom>();
            result.Add(
                new ConferenceRoom()
            
            {
                Id = 1,
                BuildingName = "Nampa Public Library",
                Name = "Lecture Hall",
                Location = "Nampa",
                AvailableLectureDevices = {
                    AssisstedLectureDevices.projector,
                    AssisstedLectureDevices.microphone,
                    AssisstedLectureDevices.podium
                },
                ResourceTimeZone = ResourceTimeZone.MST,
                SeatingProvided = 50

            });
            result.Add(
                new ConferenceRoom()
            {
                
                Id = 2,
                BuildingName = "Stella's",
                Name = "Outdoor Patio",
                Location = "Down-Town Nampa",
                AvailableLectureDevices =
                {
                    AssisstedLectureDevices.speakers,
                    AssisstedLectureDevices.microphone
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
            return result;
        }
    }
}
