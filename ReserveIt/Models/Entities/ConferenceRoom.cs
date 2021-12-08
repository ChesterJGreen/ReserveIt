using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;
using static ReserveIt.Data.Reference;

namespace ReserveIt.Models
{
    public class ConferenceRoom : BaseResource
    {
        public ConferenceRoom()
        {
            Reservations = new HashSet<Reservation>();
        }
        [MaxLength(50)]
        public string Location { get; set; }
        public string BuildingName { get; set; }
        public int SeatingProvided { get; set; }

        public AssistedLectureDevices LectureDevices { get; private set; }

        [NotMapped]
        public IEnumerable<AssistedLectureDevices> AvailableLectureDevices
        {
            get
            {
                List<AssistedLectureDevices> result = new List<AssistedLectureDevices>();

                // test for each flag defined in AssistedLectureDevices
                foreach (string deviceName in Enum.GetNames(typeof(AssistedLectureDevices)))
                {
                    // get actual value for this device
                    var deviceFlagValue = Enum.Parse<AssistedLectureDevices>(deviceName);

                    // add it to the list if set
                    if (LectureDevices.HasFlag(deviceFlagValue))
                        result.Add(deviceFlagValue);
                }

                return result;
            }

            set
            {
                AssistedLectureDevices result = AssistedLectureDevices.none; // 00000000

                // set the bit for each device in the array
                foreach (AssistedLectureDevices device in value)
                {
                    result = result | device;
                }

                // store the underlying data
                LectureDevices = result;
            }
        }

        public ResourceType ResourceType { get; set; } = ResourceType.ConferenceRoom;
        [ForeignKey("ResourceId")]
        public virtual IEnumerable<Reservation> Reservations { get; set; }
        





    }
}
