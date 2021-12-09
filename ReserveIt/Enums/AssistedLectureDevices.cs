using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Enums
{
    [Flags]
    public enum AssistedLectureDevices : int
    {
        none = 0,           // 00000000
        projector = 1,      // 00000001
        podium = 2,         // 00000010
        speakers = 4,       // 00000100
        microphone = 8,     // 00001000
        // ex: mic + podium =  00001010
        chalkboard = 16,    // 00010000 
        whiteboard = 32     // 00100000
    }



    public enum AssistedLectureDevicesNoFlag
    {
        projector,
        podium,
        speakers,
        microphone,
        chalkboard,
        whiteboard
    }

    public class DemoCode
    {
        public static void DeleteMe()
        {
            AssistedLectureDevices notAFlag = AssistedLectureDevices.chalkboard;
            notAFlag = AssistedLectureDevices.podium;

            notAFlag = AssistedLectureDevices.podium | AssistedLectureDevices.speakers;
            // notAFlag is actually equal to podium (1) + speakers (2) = microphone (3)

            bool isamic = notAFlag.HasFlag(AssistedLectureDevices.microphone);
        }
    }
}
