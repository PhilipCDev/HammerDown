using UnityEngine;

namespace HammerDown.Map.Fix
{
    public class MaskApplyable : MonoBehaviour
    {
        void Start()
        {
            gameObject.GetComponent<Renderer>().material.renderQueue = 2500;
        }
    }
}
