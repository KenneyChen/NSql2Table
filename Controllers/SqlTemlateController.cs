using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulv.YCF.API.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Dal.Models;
using NSql2Table.Models;
using NSql2Table.Models.InputModels;
using NSql2Table.Models.OutputModels;
using NSql2Table.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSql2Table.Controllers
{
    public class SqlTemlateController : ControllerBase
    {
        private readonly ISqltemlateService _sqltemlateService;

        public SqlTemlateController(ISqltemlateService sqltemlateService)
        {
            this._sqltemlateService = sqltemlateService;
        }

        [HttpGet]
        [HttpPost]
        public Page<Sqltemlate> GetList([FromQuery] PagingInfo pageinfo)
        {
            return _sqltemlateService.Paging(pageinfo);
        }
        /*
         * {"SqlString":"select id from user where id=@id and name=@name","DsSourceId":1,"TemplateName":"333","ResultList":[{"tableColumnName":"id","dbColumnName":"id","tableColumnType":1,"isShow":true},{"tableColumnName":"name","dbColumnName":"name","tableColumnType":1,"isShow":true}],"ConditionList":[{"tableColumnName":" id ","dbColumnName":" id ","tableColumnType":1,"tableColumnDefaultValue":"","tableDefaultValueCode":"","isShow":true,"queryMode":1}]}
         */

        /*
         SELECT SerialNumber,CooperatorOrderId,VoucherValue,O.OrderNo,F.CheckinTime FROM TPOrdersMapping F
LEFT JOIN OrderPackageDetail D ON D.ProviderId=F.CooperatorId AND D.OrderPackageDetailId=F.OrderId
LEFT JOIN ORDERS O ON D.OrderId=O.OrderId
WHERE CooperatorId=@CooperatorId AND CooperatorOrderId=@CooperatorOrderId
         */
        [HttpPost]
     
        public bool Save([FromBody]SqlTemplateDto output)
        {
            return _sqltemlateService.Save(output);
        }

        [HttpGet]
        public SqlTemplateDto Fetch(int id)
        {
            return _sqltemlateService.Fetch(id);
        }

        [HttpPost]
        public object Search(IFormCollection fc)
        {
            return _sqltemlateService.Search(fc);
        }
    }
}
