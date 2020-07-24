using amsv2.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amsv2.Model.Dto
{
    public class PageModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int startPage { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int rowCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; }
        public object data { get; set; }
    }
}
