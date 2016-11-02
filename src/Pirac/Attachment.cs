namespace Pirac
{
    public interface IAttachment<T>
    {
        void AttachTo(T obj);
    }

    public abstract class Attachment<T> : IAttachment<T>
    {
        protected T viewModel;

        protected abstract void OnAttach();

        void IAttachment<T>.AttachTo(T obj)
        {
            viewModel = obj;
            OnAttach();
        }
    }
}