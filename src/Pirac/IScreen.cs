namespace Pirac
{
    public interface IScreen
    {
        void Activate();

        void Deactivate(bool close);

        bool CanClose();
    }
}