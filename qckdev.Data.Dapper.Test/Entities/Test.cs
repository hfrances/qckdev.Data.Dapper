﻿using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Data.Dapper.Test.Entities
{
    sealed class Test
    {

        public Guid TestId { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }

    }
}