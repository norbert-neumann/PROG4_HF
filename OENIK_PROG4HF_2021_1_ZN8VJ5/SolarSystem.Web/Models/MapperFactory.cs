namespace SolarSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;

    /// <summary>
    /// Mappar factory for Data.Planet to Web.Planet conversion.
    /// </summary>
    public static class MapperFactory
    {
        /// <summary>
        /// Creates a mapper.
        /// </summary>
        /// <returns>New Data.Planet to Web.Planet mapper.</returns>
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SolarSystem.Data.Models.Planet, SolarSystem.Web.Models.Planet>().
                    ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id)).
                    ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name)).
                    ForMember(dest => dest.Mass, map => map.MapFrom(src => src.Mass)).
                    ForMember(dest => dest.Diameter, map => map.MapFrom(src => src.Diameter)).
                    ForMember(dest => dest.Distance, map => map.MapFrom(src => src.Distance)).
                    ForMember(dest => dest.Molecules, map => map.MapFrom(src => src.Molecules)).
                    ForMember(dest => dest.HostId, map => map.MapFrom(src => src.HostId)).
                    ForMember(dest => dest.Host, map => map.MapFrom(src => src.Host)).
                    ForMember(dest => dest.IsHabitable, map => map.MapFrom(src => src.IsHabitable));
            });
            return config.CreateMapper();
        }
    }
}
