﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ReproductiveGroupGenerators
{
    public class ParentsPlusChildrenReproductiveGroupGenerator : IReproductiveGroupGenerator
    {
        public IList<Encoding> Create(IList<Encoding> parents, IList<Encoding> children)
        {
            return parents.Concat(children).ToList();
        }
    }
}
