using Unity.Netcode.Components;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}