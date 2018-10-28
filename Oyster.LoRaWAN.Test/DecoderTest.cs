using System;
using System.Collections.Generic;
using Xunit;

namespace Oyster.LoRaWAN.Test
{
    public class DecoderTest
    {
        [Fact]
        public void Test1()
        {
            var hex = "53AB783C0421F98E940AB3";
            

            var o = Decoder.Decode(hex, 1);

            Assert.Equal(101.4541139, o.LatitudeDeg);
            Assert.Equal(-189.6275708, o.LongitudeDeg);
            Assert.Equal(208.125, o.HeadingDeg);
            Assert.Equal(10, o.SpeedKmph);
            Assert.Equal(4.475, o.BatV, 4);
        }

        [Fact]
        public void Test2()
        {
            var hex = "02873f2185147107020077";

            var o = Decoder.Decode(hex, 1);

        }
    }
}
