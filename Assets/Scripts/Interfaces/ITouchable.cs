using HammerDown.Tools;

namespace HammerDown.Interfaces
{
    public interface ITouchable
    {
        void OnTouchEnter(Hand hand);
        void OnTouchStay(Hand hand);
        void OnTouchExit(Hand hand);
    }
}
