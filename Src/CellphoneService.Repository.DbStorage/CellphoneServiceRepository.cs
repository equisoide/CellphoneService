using CellphoneService.Repository.DbStorage.EntityFramework;
using System.Linq;
using System;
using CellphoneService.Model;
using System.Data.Entity;

namespace CellphoneService.Repository.DbStorage
{
    public class CellphoneServiceRepository : ICellphoneServiceRepository
    {
        #region ICellphoneServiceRepository Methods

        public double GetPricePerMinute()
        {
            var price = 0d;

            using (var db = new CellphoneServiceEntities())
            {
                var setting = db.Settings
                    .Where(s => s.Id == "PricePerMinute")
                    .FirstOrDefault();

                if (setting != null)
                {
                    double.TryParse(setting.Value, out price);
                }
            }

            return price;
        }

        public double GetMinutesUsed(string lineNumber)
        {
            var line = GetLine(lineNumber);
            var minutes = line.SecondsUsed / 60d;

            return minutes;
        }

        public double GetMinutesLeft(string lineNumber)
        {
            var line = GetLine(lineNumber);
            var minutes = line.SecondsLeft / 60d;

            return minutes;
        }

        public Model.Recharge Recharge(Model.Recharge recharge)
        {
            using (var db = new CellphoneServiceEntities())
            {
                var line = GetLine(recharge.Number);
                var seconds = (int)((recharge.Value / (decimal)GetPricePerMinute()) * 60);
                var bonusSeconds = 0;
                var bonusDescription = "No bonus!";
                var tomorrowsDate = DateTime.Today.AddDays(1);

                // Only one bonus can be granted per day.
                var existsBonusToday = db.Recharge.Where(r =>
                    r.BonusSeconds > 0 &&
                    r.Date >= DateTime.Today &&
                    r.Date < tomorrowsDate).Any();

                if (!existsBonusToday)
                {
                    // If the recharged sum is greater than the average sums of all previous recharges,
                    // then a 10 % bonus is awarded on the recharged sum.
                    var averageAllPreviousRecharges = db.Recharge.Where(
                        r => r.Number == recharge.Number).Average(r => (decimal?)r.Value);

                    if (averageAllPreviousRecharges.HasValue &&
                        recharge.Value > averageAllPreviousRecharges.Value)
                    {
                        bonusSeconds = (int)(seconds * 0.1);
                        bonusDescription = "You won a 10% bonus on your recharge";
                    }
                }

                // Add recharge
                var result = db.Recharge.Add(new EntityFramework.Recharge
                {
                    Number = recharge.Number,
                    Value = recharge.Value,
                    Date = DateTime.Now,
                    Seconds = seconds,
                    BonusSeconds = bonusSeconds,
                    BonusDescription = bonusDescription,
                    TotalSeconds = seconds + bonusSeconds
                });

                // Update seconds left to the line number
                line.SecondsLeft += result.TotalSeconds;
                db.Entry(line).State = EntityState.Modified;

                db.SaveChanges();

                return ToBusinessModel(result);
            } 
        }

        #endregion

        #region Private Methods

        private Line GetLine(string lineNumber)
        {
            using (var db = new CellphoneServiceEntities())
            {
                var line = db.Line.Find(lineNumber);

                if (line == null)
                {
                    throw new Exception("Line number doesn´t exists: " + lineNumber);
                }

                return line;
            }
        }

        private Model.Recharge ToBusinessModel(EntityFramework.Recharge entityModel)
        {
            return new Model.Recharge
            {
                Id = entityModel.Id,
                Number = entityModel.Number,
                Value = entityModel.Value,
                Date = entityModel.Date,
                Seconds = entityModel.Seconds,
                BonusSeconds = entityModel.BonusSeconds,
                BonusDescription = entityModel.BonusDescription,
                TotalSeconds = entityModel.TotalSeconds
            };
        }

        #endregion
    }
}