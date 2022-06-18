﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class EntityBase
    {
        public int Id { get; private set; }
        public Guid Guid { get; set; }

        protected EntityBase()
        {  }
    }
}
