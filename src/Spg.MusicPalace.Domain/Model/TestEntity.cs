using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class TestEntity : EntityBase
    {
        public string Name { get; set; }

        public TestEntity()
        {
            Name = string.Empty;
        }

        public TestEntity(string _name)
        {
            Name = _name;
        }
    }
}
