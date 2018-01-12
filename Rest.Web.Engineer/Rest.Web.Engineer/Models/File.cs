using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class File
    {
        public long FileId { get; set; }
        public long? FileIdExt { get; set; }
        public int? Parts { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }

        [NotMapped]
        public string ContentBase64 { get; set; }
    }

}