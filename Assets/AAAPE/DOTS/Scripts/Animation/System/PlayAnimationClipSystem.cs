using Unity.Entities;
using System;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAPE.DOTS {
    [AlwaysUpdateSystem]
    public partial class PlayOneShotAnimationClipSystem : SystemBase {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((Entity entity, AnimatorProxy animator, in PlayAnimationClip clip) => {
                    if (animator.Animator == null)
                    {
                        return;
                    }
                    animator.Animator.Play(clip.clip.ToString());
                }).WithoutBurst().Run();
        }
    }
}
