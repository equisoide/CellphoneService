using CellphoneService.Model;
using CellphoneService.Repository;
using CellphoneService.Repository.DbStorage;
using System.Web.Http;

namespace CellphoneService.Rest.Controllers
{
    [Authorize]
    [RoutePrefix("api/CellphoneService")]
    public class CellphoneServiceController : ApiController
    {
        private ICellphoneServiceRepository _repository = new CellphoneServiceRepository();

        // GET api/CellphoneService/GetPricePerMinute
        [Route("GetPricePerMinute")]
        public double GetPricePerMinute()
        {
            return _repository.GetPricePerMinute();
        }

        // GET api/CellphoneService/GetMinutesUsed?lineNumber=3043499162
        [Route("GetMinutesUsed")]
        public double GetMinutesUsed(string lineNumber)
        {
            return _repository.GetMinutesUsed(lineNumber);
        }

        // GET api/CellphoneService/GetMinutesLeft?lineNumber=3043499162
        [Route("GetMinutesLeft")]
        public double GetMinutesLeft(string lineNumber)
        {
            return _repository.GetMinutesLeft(lineNumber);
        }

        // POST api/CellphoneService/Recharge
        [Route("Recharge")]
        [HttpPost]
        public Recharge Recharge(Recharge recharge)
        {
            return _repository.Recharge(recharge);
        }
    }
}