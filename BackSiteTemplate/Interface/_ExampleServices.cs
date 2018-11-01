using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using BackSiteTemplate.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackSiteTemplate.Interface
{
    public class _ExampleServices
    {

        public interface _IExampleAction
        {

        }

        public class _ExampleService : _IExampleAction
        {
            private readonly BackSiteDBTempContext db;
            public _ExampleService(BackSiteDBTempContext DB)
            {
                this.db = DB;
            }
        }
    }
}
