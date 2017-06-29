using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.Calculators;

namespace GSACapitalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/cumulative-pnl/{startDate?}/{region?}")]
    public class CumulativePNLController : Controller
    {
        private IUnitOfWork _uow;
        private ICalculator<List<ProfitNLoss>, List<CumulativePNL>> _calculator;

        public CumulativePNLController(IUnitOfWork uow, ICalculator<List<ProfitNLoss>, List<CumulativePNL>> calculator)
        {
            _uow = uow;
            _calculator = calculator;
        }

        public List<CumulativePNL> Get(string startDate=null, string region=null)
        {
            
            var pnls = _uow.GetRepository<ProfitNLoss>().GetQuery().Include(x => x.Strategy).ToList();

            if (startDate != null)
            {
                pnls = pnls.Where(x => DateTime.Parse(startDate).Date.CompareTo(x.Date.Date) == -1).ToList();
            }

            if (region != null)
            {
                pnls = pnls.Where(x => region == x.Strategy.Region).ToList();
            }

            var result = _calculator.Calculate(pnls);

            return result.OrderBy(x => x.region).ToList();

        }
    }
}