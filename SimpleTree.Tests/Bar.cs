using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTree.Tests
{
    public class Bar
    {
        public Bar(int id, int? parentId)
        {
            this.Id = id;
            this.ParentId = parentId;
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }
    }
}
