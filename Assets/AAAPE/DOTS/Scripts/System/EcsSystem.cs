using Unity.Entities;
using Unity.Collections;

namespace AAAPE.DOTS {
    public abstract class EcsSysten : SystemBase {
        protected abstract void Flop();
        protected void BeforeUpdate(){}
        protected void AfterUpdate(){}

        protected override void OnUpdate()
        {
           BeforeUpdate();
           Flop();
           AfterUpdate();
        }
    }
}