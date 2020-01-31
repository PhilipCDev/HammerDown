using HammerDown.Tools;

namespace HammerDown.Interfaces
{
    public interface IGrabable
    {
        void OnGrab(Hand hand);
        void OnRelease(Hand hand);
    }
}
