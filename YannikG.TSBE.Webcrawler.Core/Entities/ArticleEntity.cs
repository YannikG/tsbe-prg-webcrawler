﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YannikG.TSBE.Webcrawler.Core.Entities
{
    public class ArticleEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? ArticleNumber { get; set; }
        public string? ArticleManufacturer { get; set; }
        public string? Url { get; set; }
        public long? ImageId { get; set; }
    }
}
