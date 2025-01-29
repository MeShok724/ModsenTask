﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Core.Entities
{
    public class UserBook
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public DateTime BorrowedAt { get; set; }
        public DateTime ReturnBy { get; set; }
    }
}
