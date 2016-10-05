namespace Pirac
{
    public interface IActivatable
    {
        void Activate();

        void Deactivate(bool close);

        bool CanClose();
    }
}