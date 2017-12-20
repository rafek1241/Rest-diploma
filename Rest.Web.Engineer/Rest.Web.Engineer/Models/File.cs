using System;
using System.Collections.Generic;
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
        public string MimeType { get; set; }
        public byte[] Content { get; set; }

    }
}
