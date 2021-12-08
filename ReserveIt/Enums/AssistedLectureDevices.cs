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
}
