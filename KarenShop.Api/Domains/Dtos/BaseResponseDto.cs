using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Dtos
{
    public class BaseResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}
