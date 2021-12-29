using System;
using AutoMapper;

namespace ReserveIt.TestingCommon
{
    public static class AutoMapperTesting
    {
        public static MapperConfiguration GetAutoMapperConfig()
        {
            var config = new MapperConfiguration(conf => { Config.AutoMapperConfig.ConfigureAutoMapper(conf); });
            return config;

        }
        public static IMapper GetMapper()
        {
            var config = GetAutoMapperConfig();
            return config.CreateMapper();
        }
    }
}
