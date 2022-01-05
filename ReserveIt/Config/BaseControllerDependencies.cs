using AutoMapper;
using Microsoft.Extensions.Options;
using ReserveIt.Utilities.ConfigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Config
{
    public class BaseControllerDependencies
    {
        public IMapper mapper;
        public IOptions<AppConfigModel> appSettings;

        public BaseControllerDependencies(
            IMapper mapper,
            IOptions<AppConfigModel> appSettings)
        {
            this.mapper = mapper;
            this.appSettings = appSettings;
        }
    }
}
