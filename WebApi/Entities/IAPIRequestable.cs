using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Entities
{
    public interface IAPIRequestable
    {
        int Id { get; set; }
        string APILocation { get; }
    }
}