﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.PropertyAtributes
{
    public class AtributePrimaryKey : Attribute
    {
        public bool AutoGenerated { get; set; } = true;
    }
}
