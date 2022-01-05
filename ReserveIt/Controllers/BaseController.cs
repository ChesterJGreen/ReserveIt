using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReserveIt.Utilities.ConfigModels;
using ReserveIt.Config;


namespace ReserveIt.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected readonly IOptions<AppConfigModel> _appConfigOptions;

        protected AppConfigModel AppConfig => _appConfigOptions.Value;
        protected IMapper Mapper { get; private set; }

        public ControllerBase(BaseControllerDependencies dependencies)
        {
            Mapper = dependencies.mapper;
            _appConfigOptions = dependencies.appSettings;
        }
    }
}
