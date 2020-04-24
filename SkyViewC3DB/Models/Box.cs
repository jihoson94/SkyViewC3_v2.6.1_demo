using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SkyViewC3DB.Models
{
    public class Box
    {
        public string BoxID { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public bool IsOut { get; set; }
    }
}