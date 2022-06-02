using Unity.Entities;
using System;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAPE.DOTS {
    [AlwaysUpdateSystem]
    public class PlayAnimationClipSystem : SystemBase {
        protected override void OnUpdate()
        {
            Entities
                .ForEach( (AnimatorProxyAuthoring animator, in PlayAnimationClip clip) => {
                    Debug.Log("animate");
                    animator.Animator.Play(clip.clip.ToString());
                }).WithoutBurst().Run();
        }
    }
}
