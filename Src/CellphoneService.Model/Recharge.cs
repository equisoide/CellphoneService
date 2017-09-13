using System;

namespace CellphoneService.Model
{
    public class Recharge
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Value { get; set; }
        public int Seconds { get; set; }
        public int BonusSeconds { get; set; }
        public string BonusDescription { get; set; }
        public int TotalSeconds { get; set; }
        public DateTime Date { get; set; }
    }
}