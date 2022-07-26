using System;

namespace AAAPE.DOTS
{
    public class WithGameFlagAttribute : Attribute
    {
        public Type Flag;

        public WithGameFlagAttribute(Type t)
        {
            this.Flag = t;
        }
    }
}