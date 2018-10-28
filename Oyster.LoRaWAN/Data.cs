namespace Oyster.LoRaWAN
{
    public class Data
    {
        public string Type { get; set; }
        public double LatitudeDeg { get; set; }
        public double LongitudeDeg { get; set; }
        public bool InTrip { get; set; }
        public bool FixFailed { get; set; }
        public double HeadingDeg { get; set; }
        public int SpeedKmph { get; set; }
        public double BatV { get; set; }
        public bool? ManDown { get; set; }
        public int Sequence { get; set; }
        public byte FwMaj { get; set; }
        public byte FwMin { get; set; }
        public bool Accepted { get; set; }
        public double InitialBatV { get; set; }
        public int TxCount { get; set; }
        public int TripCount { get; set; }
        public int GpsSuccesses { get; set; }
        public int GpsFails { get; set; }
        public int AveGpsFixS { get; set; }
        public int AveGpsFailS { get; set; }
        public int AveGpsFreshenS { get; set; }
        public int WakeupsPerTrip { get; set; }
        public int UptimeWeeks { get; set; }
    }
}