using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.DTOs
{

    public class BookRequest
    {
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public bool IsTaken { get; set; } = false;
        public byte[] Image { get; set; } = [];
    }
}
