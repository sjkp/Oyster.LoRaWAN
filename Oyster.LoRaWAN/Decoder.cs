using System;
using System.Collections.Generic;

namespace Oyster.LoRaWAN
{
    public class Decoder
    {
        public static Data Decode(string hex, int port)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes.Add(byte.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
            }

            return Decode(bytes.ToArray(), port);
        }

        public static Data Decode(byte[] bytes, int port)
        {           

            Data decoded = new Data();
            if (port == 1)
            {
                decoded.Type = "position";
                decoded.LatitudeDeg = bytes[0] + bytes[1] * 256 +
                bytes[2] * 65536 + bytes[3] * 16777216;
                if (decoded.LatitudeDeg >= 0x80000000) // 2^31
                    decoded.LatitudeDeg -= 0x100000000; // 2^32
                decoded.LatitudeDeg /= 1e7;

                decoded.LongitudeDeg = bytes[4] + bytes[5] * 256 +
                bytes[6] * 65536 + bytes[7] * 16777216;
                if (decoded.LongitudeDeg >= 0x80000000) // 2^31
                    decoded.LongitudeDeg -= 0x100000000; // 2^32
                decoded.LongitudeDeg /= 1e7;
                decoded.InTrip = ((bytes[8] & 0x1) != 0) ? true : false;
                decoded.FixFailed = ((bytes[8] & 0x2) != 0) ? true : false;
                decoded.HeadingDeg = (bytes[8] >> 2) * 5.625;

                decoded.SpeedKmph = bytes[9];
                decoded.BatV = bytes[10] * 0.025;
                decoded.ManDown = null;
            }
            else if (port == 4)
            {
                decoded.Type = "position";
                decoded.LatitudeDeg = bytes[0] + bytes[1] * 256 + bytes[2] * 65536;
                if (decoded.LatitudeDeg >= 0x800000) // 2^23
                    decoded.LatitudeDeg -= 0x1000000; // 2^24
                decoded.LatitudeDeg *= 256e-7;

                decoded.LongitudeDeg = bytes[3] + bytes[4] * 256 + bytes[5] * 65536;
                if (decoded.LongitudeDeg >= 0x800000) // 2^23
                    decoded.LongitudeDeg -= 0x1000000; // 2^24
                decoded.LongitudeDeg *= 256e-7;
                decoded.HeadingDeg = (bytes[6] & 0x7) * 45;
                decoded.SpeedKmph = (bytes[6] >> 3) * 5;
                decoded.BatV = bytes[7] * 0.025;
                decoded.InTrip = ((bytes[8] & 0x1) != 0) ? true : false;
                decoded.FixFailed = ((bytes[8] & 0x2) != 0) ? true : false;
                decoded.ManDown = ((bytes[8] & 0x4) != 0) ? true : false;
            }
            else if (port == 2)
            {
                decoded.Type = "downlink ack";
                decoded.Sequence = (bytes[0] & 0x7F);
                decoded.Accepted = ((bytes[0] & 0x80) != 0) ? true : false;
                decoded.FwMaj = bytes[1];
                decoded.FwMin = bytes[2];
            }
            else if (port == 3)
            {
                
                decoded.Type = "stats";
                decoded.InitialBatV = 4.0 + 0.100 * (bytes[0] & 0xF);
                decoded.TxCount = 32 * ((bytes[0] >> 4) + (bytes[1] & 0x7F) * 16);
                decoded.TripCount = 32 * ((bytes[1] >> 7) + (bytes[2] & 0xFF) * 2
                + (bytes[3] & 0x0F) * 512);
                decoded.GpsSuccesses = 32 * ((bytes[3] >> 4) + (bytes[4] & 0x3F) * 16);
                decoded.GpsFails = 32 * ((bytes[4] >> 6) + (bytes[5] & 0x3F) * 4);
                decoded.AveGpsFixS = 1 * ((bytes[5] >> 6) + (bytes[6] & 0x7F) * 4);
                decoded.AveGpsFailS = 1 * ((bytes[6] >> 7) + (bytes[7] & 0xFF) * 2);
                decoded.AveGpsFreshenS = 1 * ((bytes[7] >> 8) + (bytes[8] & 0xFF) * 1);
                decoded.WakeupsPerTrip = 1 * ((bytes[8] >> 8) + (bytes[9] & 0x7F) * 1);
                decoded.UptimeWeeks = 1 * ((bytes[9] >> 7) + (bytes[10] & 0xFF) * 2);
            }

            return decoded;
        }
    }
}
