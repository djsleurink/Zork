﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zork.Objects;

namespace Zork.Objects
{
    public class CorpseNPCObject : BaseObject
    {
        public CorpseNPCObject(string name, string description = "A dead body.") : base(name, description)
        {
        }

        public override ConsoleColor Color => ConsoleColor.DarkRed;

        public override void PickupObject(Room room, Character character)
        {
            Console.WriteLine("Can't pickup a dead body");
        }
    }
}
