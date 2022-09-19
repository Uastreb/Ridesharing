using AutoMapper;
using RideSharingApp.DAL.Interfaces;
using System.Collections.Generic;

namespace RideSharingApp.BLL.Services
{
    public class AutoMap<EntityIn, EntityOut>
    {
        IMapper mapper;
        public AutoMap()
        {
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<EntityOut, EntityIn>()).CreateMapper();
        }

        public EntityIn Initialize(EntityOut obj)
        {
            return mapper.Map<EntityOut, EntityIn>(obj);
        }

        public IEnumerable<EntityIn> GetAll (IEnumerable<EntityOut> collection)
        {
            return mapper.Map<IEnumerable<EntityOut>, IEnumerable<EntityIn>>(collection);
        }
    }
}
