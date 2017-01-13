namespace SeeSharp.Infrastructure
{
    public static class ManagerFactory
    {
        public static T GetManager<T>() where T : new()
        {
            return new T();
        }
    }
}