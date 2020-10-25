
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：{Comment}                                                    
*│　作    者：{Author}                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：{GeneratorTime}                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： NSql2Table.Services.Impl                                  
*│　类    名： SqltemlateService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using NetCore.Dal.Services.Impl;
using NSql2Table.Models;
using NSql2Table.Repository;
using NSql2Table.Services;
using System;
using System.Linq;
using YDal;
using NSql2Table.Models.InputModels;
using NSql2Table.Common;
using NetCore.Dal.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using YDal.Common;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.Text.RegularExpressions;

namespace NSql2Table.Services.Impl
{
    public class SqltemlateService : BaseService<ISqltemlateRepository, Sqltemlate>,
        ISqltemlateService
    {
        private readonly ISqltemlateRepository _repository;
        private readonly ISqlcondconfRepository _sqlcondconfRepository;

        private readonly ISqlresultconfRepository _sqlresultconfRepository;


        private readonly IDsconfigRepository _dsconfigRepository;

        public SqltemlateService(ISqltemlateRepository repository
            , ISqlcondconfRepository sqlcondconfRepository
            , ISqlresultconfRepository sqlresultconfRepository
            , IDsconfigRepository dsconfigRepository
            ) : base(repository)
        {
            this._repository = repository;
            this._sqlcondconfRepository = sqlcondconfRepository;
            this._sqlresultconfRepository = sqlresultconfRepository;
            this._dsconfigRepository = dsconfigRepository;
        }

        public Page<Sqltemlate> Paging(PagingInfo page)
        {
            return _repository.Entities.OrderByDescending(f=>f.Id).Paging<Sqltemlate>(page);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public SqlTemplateDto Fetch(int id)
        {
            var result = _repository.Entities.Where(f => f.Id == id).Select(p => new SqlTemplateDto
            {
                DsSourceId = p.DsSourceId,
                SqlString = p.SqlString,
                TemplateName = p.TemplateName,
            }).First();

            var cdQuery = _sqlcondconfRepository.Entities
                 .Where(f => f.SqlTemplateId == id)
                 .Select(f => new SqlConditionDto
                 {
                     CreatedById = 0,
                     CreatedByName = "系统",
                     CreatedDate = DateTime.Now,
                     ModifiedByName = "系统",
                     ModifiedDate = DateTime.Now,
                     DbColumnName = f.DbColumnName,
                     IsShow = f.IsShow,
                     QueryMode = f.QueryMode,
                     TableColumnDefaultValue = f.TableCloumnDefaultValue,
                     TableColumnName = f.TableCloumnName,
                     TableColumnType = f.TableCloumnType,
                     TableDefaultValueCode = f.TableDefaultValueCode,

                 });
                 
             

            result.ConditionList = cdQuery.ToList();

            var rtQuery = _sqlresultconfRepository.Entities
                .Where(f => f.SqlTemplateId == id)
                .Select(f => new SqlResultDto
                {
                    CreatedById = 0,
                    CreatedByName = "系统",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "系统",
                    ModifiedDate = DateTime.Now,
                    DbColumnName = f.DbColumnName,
                    IsShow = f.IsShow,
                    TableColumnName = f.TableCloumnName,
                    TableColumnType = f.TableCloumnType,

                });
            result.ResultList = rtQuery.ToList();

            return result;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save(SqlTemplateDto dto)
        {
            if (dto.Id > 0)
            {
                _repository.Update(f => f.Id == dto.Id, p => new Sqltemlate
                {
                    DsSourceId = dto.DsSourceId,
                    SqlString = dto.SqlString,
                    TemplateName = dto.TemplateName,
                });
            }
            else
            {
                var model = new Sqltemlate
                {
                    SqltemlateCode = StringUtils.GuidToBase64String(),
                    DsSourceId = dto.DsSourceId,
                    SqlString = dto.SqlString,
                    TemplateName = dto.TemplateName,
                };
                _repository.Insert(model);
                dto.Id = model.Id;
            }

            //删除条件表
            _sqlcondconfRepository.Delete(f => f.SqlTemplateId == dto.Id);
            //删除结果表
            _sqlresultconfRepository.Delete(f => f.SqlTemplateId == dto.Id);

            //添加
            if (dto.ConditionList != null)
            {
                dto.ConditionList.ForEach(f =>
                {
                    _sqlcondconfRepository.Insert(new Sqlcondconf
                    {
                        CreatedById = 0,
                        CreatedByName = "系统",
                        CreatedDate = DateTime.Now,
                        ModifiedByName = "系统",
                        ModifiedDate = DateTime.Now,
                        DbColumnName = f.DbColumnName,
                        IsShow = f.IsShow,
                        QueryMode = f.QueryMode,
                        SqlTemplateId = dto.Id,
                        TableCloumnDefaultValue = f.TableColumnDefaultValue,
                        TableCloumnName = f.TableColumnName,
                        TableCloumnType = f.TableColumnType,
                        TableDefaultValueCode = f.TableDefaultValueCode,

                    }, false);
                });
            }

            if (dto.ResultList != null)
            {
                dto.ResultList.ForEach(f =>
                {
                    _sqlresultconfRepository.Insert(new Sqlresultconf
                    {
                        CreatedById = 0,
                        CreatedByName = "系统",
                        CreatedDate = DateTime.Now,
                        ModifiedByName = "系统",
                        ModifiedDate = DateTime.Now,
                        DbColumnName = f.DbColumnName,
                        IsShow = f.IsShow,
                        SqlTemplateId = dto.Id,
                        TableCloumnName = f.TableColumnName,
                        TableCloumnType = f.TableColumnType,

                    }, false);
                });
            }
            return _repository.Commit() > 0;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public PageObject<object> Search(IFormCollection fc)
        {
            //主键
            var id = fc["__search__id"].ToString().CastTo<int>();
            var page = fc["page"].CastTo<int>();
            var pageSize = fc["pageSize"].CastTo<int>();
            var paramters = new List<SqlParameter>();
            foreach (var key in fc.Keys)
            {
                paramters.Add(new SqlParameter(key, fc[key][0]));
            }
            var query = from a in _repository.Entities
                        join b in _dsconfigRepository.Entities on a.DsSourceId equals b.Id
                        where a.Id == id
                        select new
                        {
                            b.DbType,
                            b.DbConnection,
                            a.SqlString,
                        };
            var templete = query.FirstOrDefault();

            var connection = templete.DbType.CastTo<Common.DbType>().GetConnection( templete.DbConnection);
            /*
             * 页面统一要求传入
             * select * from (
    　　　    　select *, ROW_NUMBER() OVER(Order by ArtistId ) AS RowId from ArtistModels
    　　       ) as b
              where RowId between 10 and 20
             */
            //找到order by 字段
            var regex = new Regex(@"order\s+by\s+([\s|\S]+)(desc|asc)", RegexOptions.IgnoreCase);
            var matches = regex.Match(templete.SqlString);
            if (matches.Groups.Count <= 1)
            {
                throw new Exception("没有order by 语句 分页失败");
            }
            //order by 对应字段
            var orderByColumns = matches.Groups[1].Value.Trim();
            var sPage = (page - 1) < 0 ? 0 : (page - 1) * pageSize;


            var sql = $"select * from ({templete.SqlString} ) as b where RowId between {sPage} and {page * pageSize}";
            var dt = connection.QueryTable(sql, parameters: paramters.AsEnumerable());
            var count = connection.SqlQuery(templete.SqlString, paramters.AsEnumerable());
            var dic = new Dictionary<string, object>();
            foreach (DataRow item in dt.Rows)
            {
                var arr = item.ItemArray;
                foreach (var filed in arr)
                {
                    dic.Add(filed.ToString(), item[filed.ToString()]);
                }
            }

            return new PageObject<object>
            {
                Records = dic,
                TotalCount = count,
            };
        }
    }
}