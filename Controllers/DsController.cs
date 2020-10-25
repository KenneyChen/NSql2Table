using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulv.YCF.API.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCore.Dal.Models;
using NSql2Table.Models.OutputModels;
using NSql2Table.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSql2Table.Controllers
{
    public class DsController : ControllerBase
    {
        private readonly IDsconfigService dsconfigService;

        public DsController(IDsconfigService dsconfigService)
        {
            this.dsconfigService = dsconfigService;
        }

        [HttpGet]
        [HttpPost]
        public Page<DsConfigOutput> GetList([FromQuery] PagingInfo pageinfo)
        {
            return dsconfigService.GetList(pageinfo);
        }

        [HttpGet()]
        public DsConfigOutput GetById(int id)
        {
            return dsconfigService.GetById(id);
        }

        [HttpPost]
        public int Save([FromBody] DsConfigOutput output)
        {
            return dsconfigService.Save(output);
        }


        [HttpPost]
        public int test([FromBody]DsConfigOutput output)
        {
            return 0;
        }

    }
}
