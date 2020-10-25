using NetCore.Dal.Models;
using NetCore.Dal.Services.Impl;
using NSql2Table.Common;
using NSql2Table.Models;
using NSql2Table.Models.OutputModels;
using NSql2Table.Repository;
using System;
using System.Linq;
using YDal;

namespace NSql2Table.Services.Impl
{
    public class DsconfigService : BaseService<IDsconfigRepository, Dsconfig>,
        IDsconfigService
    {
        /// <summary>
        /// 直接注入类对象，获取数据库基本操作
        /// </summary>
        public IDsconfigRepository Repository { get; private set; }


        public DsconfigService(IDsconfigRepository dsconfigRepository) : base(dsconfigRepository)
        {
            this.Repository = dsconfigRepository;
        }

    
        public Page<DsConfigOutput> GetList(PagingInfo page)
        {
            var query = from a in Repository.Entities
                        select new DsConfigOutput
                        {
                            Code = a.Code,
                            CreatedById = a.CreatedById,
                            CreatedByName = a.CreatedByName,
                            CreatedDate = a.CreatedDate,
                            DbConnection = a.DbConnection,
                            DbType = a.DbType,
                            Id = a.Id,
                            ModifiedByName = a.ModifiedByName,
                            ModifiedDate = a.ModifiedDate,
                            Name = a.Name,
                        };

            return query.Paging(page);
        }

        public DsConfigOutput GetById(int id)
        {
            var query = from a in Repository.Entities
                        where a.Id == id
                        select new DsConfigOutput
                        {
                            Code = a.Code,
                            CreatedById = a.CreatedById,
                            CreatedByName = a.CreatedByName,
                            CreatedDate = a.CreatedDate,
                            DbConnection = a.DbConnection,
                            DbType = a.DbType,
                            Id = a.Id,
                            ModifiedByName = a.ModifiedByName,
                            ModifiedDate = a.ModifiedDate,
                            Name = a.Name,
                        };
            return query.First();
        }

        public int Save(DsConfigOutput output)
        {
            if (output.Id > 0)
            {
                //更新
                return Repository.Update(f => f.Id == output.Id, p => new Dsconfig
                {
                    DbType = output.DbType,
                    DbConnection = output.DbConnection,
                    Name = output.Name,
                    ModifiedDate = DateTime.Now,
                });
            }
            else
            {
                //添加
                return Repository.Insert(new Dsconfig
                {
                    Code = StringUtils.GuidToBase64String(),
                    CreatedById = 0,
                    CreatedByName = "系统",
                    CreatedDate = DateTime.Now,
                    DbConnection = output.DbConnection,
                    DbType = output.DbType,
                    Name = output.Name,
                    ModifiedDate = DateTime.Now,
                    ModifiedByName = "系统",
                });
            }

        }

        
    }
}
