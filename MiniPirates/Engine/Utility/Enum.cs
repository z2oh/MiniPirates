﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Utility
{
    public static class Enum
    {
        public enum RelevantDirection
        {
            Forward,
            Right,
            Left,
            Back
        }

        public enum ColliderType
        {
            Undefined,
            Static,
            Dynamic
        }
    }
}
