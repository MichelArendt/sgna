using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Models
{
    public class ImageModel
    {
        [Key]
        public uint ImageName { get; set; }
        public uint ImageSize { get; set; }
        public uint ImageRawData { get; set; }
    }
}
