using System;

namespace AAAPE.DOTS {
    public class WithGameStateAttribute: Attribute {
        public dynamic State;
        public bool ReadOnly = true;
        public WithGameStateAttribute(dynamic State) {
            this.State = State;
        }
    }
}