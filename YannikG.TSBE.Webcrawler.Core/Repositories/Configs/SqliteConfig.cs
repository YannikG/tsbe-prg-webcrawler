using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YannikG.TSBE.Webcrawler.Core.Repositories.Configs
{
    public class SqliteConfig
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ArticleEntityTableName { get; set; } = string.Empty;
        public string ImageEntityTableName { get; set; } = string.Empty;
    }
}
