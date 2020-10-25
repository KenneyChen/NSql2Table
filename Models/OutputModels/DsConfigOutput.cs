using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSql2Table.Models.OutputModels
{
    public class DsConfigOutput
    {
        public Int32 Id { get; set; }


        public String Code { get; set; }


        public String Name { get; set; }


        public Int32 DbType { get; set; }

        public String DbConnection { get; set; }


        public Int32? CreatedById { get; set; }


        public String CreatedByName { get; set; }


        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get; set; }


        public String ModifiedByName { get; set; }
    }
}
