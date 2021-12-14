using Xunit;
using AutoMapper;
using System;

namespace ReservationTesting
{
    public class AutoMapperConfigIsValid
    {
        [Fact]
        public void AutoMapperConfigurationIsValid()
        {
            var configuration = new AutoMapper.MapperConfiguration(
                config => ReserveIt.Config.AutoMapperConfig.ConfigureAutoMapper(config)
                );
            configuration.AssertConfigurationIsValid();
        }
    }
}
