using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Document : IAPIRequestable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Url { get; set; }
        public string FileName { get { return Path.GetFileNameWithoutExtension(Url); } }
        public string FileExtension { get { return Path.GetExtension(Url); } }

        [NotMapped]
        public string Base64{ get; set; }

        public string APILocation => "/api/Documents";
    }
}