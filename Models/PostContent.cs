﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Models
{
    public class PostContent
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int Index { get; set; }

        public byte Type { get; set; }

        public string Data { get; set; }
    }
}
