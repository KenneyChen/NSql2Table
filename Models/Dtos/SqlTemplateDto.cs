using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSql2Table.Models.InputModels
{
    public class SqlTemplateDto
    {
        public Int32 Id { get; set; }

        public Int32 DsSourceId { get; set; }

        public String TemplateName { get; set; }


        public String SqlString { get; set; }

        public List<SqlConditionDto> ConditionList { get; set; }

        public List<SqlResultDto> ResultList { get; set; }
    }

    public class SqlResultDto
    {


        public String DbColumnName { get; set; }


        public String TableColumnName { get; set; }


        public Int32 TableColumnType { get; set; }


        public bool IsShow { get; set; }


        public Int32? CreatedById { get; set; }


        public String CreatedByName { get; set; }


        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public String ModifiedByName { get; set; }
    }

    public class SqlConditionDto
    {

        public String DbColumnName { get; set; }


        public String TableColumnName { get; set; }


        public Int32 TableColumnType { get; set; }


        public String TableColumnDefaultValue { get; set; }


        public String TableDefaultValueCode { get; set; }


        public bool IsShow { get; set; }


        public Int32 QueryMode { get; set; }


        public Int32? CreatedById { get; set; }

				public String CreatedByName { get; set; }


        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public String ModifiedByName { get; set; }
    }
}
