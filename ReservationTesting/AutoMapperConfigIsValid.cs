using Xunit;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationTesting
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapperConfigIsValid()
        {

            var configuration = ReserveIt.TestingCommon.AutoMapperTesting.GetAutoMapperConfig();
                
            configuration.AssertConfigurationIsValid();
        }
    }
}
