using System;
using UnityEngine;

namespace AAAPE.DOTS
{
    public class ProxiedComponent: MonoBehaviour
    {
        private void OnDisable()
        {
            Destroy(this.gameObject);
        }
    }
}