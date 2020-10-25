using NetCore.Dal.Models;
using NetCore.Dal.Services;
using NetCore.Dal.Services.Impl;
using NSql2Table.Models;
using NSql2Table.Models.OutputModels;
using NSql2Table.Repository;

namespace NSql2Table.Services
{
    public interface IDsconfigService : IService<Dsconfig>
    {
        Page<DsConfigOutput> GetList(PagingInfo page);

        DsConfigOutput GetById(int id);

        int Save(DsConfigOutput output);
    }
}
