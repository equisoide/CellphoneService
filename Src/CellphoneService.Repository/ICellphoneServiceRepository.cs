using CellphoneService.Model;

namespace CellphoneService.Repository
{
    public interface ICellphoneServiceRepository
    {
        double GetPricePerMinute();
        double GetMinutesUsed(string lineNumber);
        double GetMinutesLeft(string lineNumber);
        Recharge Recharge(Recharge recharge);
    }
}