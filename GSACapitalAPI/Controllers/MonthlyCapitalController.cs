using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSACapitalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/monthly-capital/{strategies?}")]
    public class MonthlyCapitalController : Controller
    {
        private IUnitOfWork _uow;

        public MonthlyCapitalController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<MonthlyCapital> Get(string strategies=null)
        {
            List<MonthlyCapital> result = new List<MonthlyCapital>();

            var capitalsRepo = _uow.GetRepository<Capital>();

            var capitals = new List<Capital>();

            if (strategies != null)
            {
                capitals = capitalsRepo.GetQuery().Include(x => x.Strategy).Where(x=>strategies.Split(',').Contains(x.Strategy.Name)).ToList();
            }
            else
            {
                capitals = capitalsRepo.GetQuery().Include(x => x.Strategy).ToList();
            }

            foreach (var capital in capitals)
            {
                var monthlyCapital = new MonthlyCapital();
                monthlyCapital.strategy = capital.Strategy.Name;
                monthlyCapital.capital = capital.Value;
                monthlyCapital.date = capital.Date.ToString("yyyy-MM-dd");

                result.Add(monthlyCapital);
            }

            return result;
        }
    }
}