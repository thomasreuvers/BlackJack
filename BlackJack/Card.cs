﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Card
    {
        public Uri FileLocation { get; set; }

        public int Value { get; set; }

        public bool IsAce { get; set; }
    }
}
