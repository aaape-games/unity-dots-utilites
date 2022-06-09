using Unity.Entities;
using System;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAPE.DOTS {
    [AlwaysUpdateSystem]
    public class PlayOneShotAnimationClipSystem : SystemBase {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((Entity entity, AnimatorProxy animator, in PlayAnimationClip clip) => {
                    animator.Animator.Play(clip.clip.ToString());
                }).WithoutBurst().Run();
        }
    }
}
