using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Utilities.Calculators;

namespace GSACapitalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/compound-daily-returns/{strategy}")]
    public class CompoundDailyReturnController : Controller
    {
        private IUnitOfWork _uow;
        private ICalculator<Tuple<List<Capital>, List<ProfitNLoss>, string>, List<CompoundDailyReturnDTO>> _calculator;

        public CompoundDailyReturnController(IUnitOfWork uow, ICalculator<Tuple<List<Capital>, List<ProfitNLoss>, string>, List<CompoundDailyReturnDTO>> calculator)
        {
            _uow = uow;
            _calculator = calculator;
        }

        [HttpGet]
        public IEnumerable<CompoundDailyReturnDTO> Get(string strategy)
        {
            

            var capitalRepo = _uow.GetRepository<Capital>();

            var pnlsRepo = _uow.GetRepository<ProfitNLoss>();

            var capitals = capitalRepo.GetQuery().Where(x => x.Strategy.Name == strategy).ToList();

            var pnls = pnlsRepo.GetQuery().Where(x => x.Strategy.Name == strategy).ToList();

            var result =
                _calculator.Calculate(new Tuple<List<Capital>, List<ProfitNLoss>, string>(capitals, pnls, strategy));

            return result;
        }
    }
}
