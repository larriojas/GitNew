
namespace Alemana.Nucleo.Common.ComponentModel
{
    public interface IComponentContainer
    {
        void Register<TFrom, TTo>(bool useCache) where TTo : TFrom;
        void Register<TFrom, TTo>() where TTo : TFrom;
        void Register<TFrom, TTo>(string name) where TTo : TFrom;
        void Register<TFrom>(string ttoFullName) where TFrom : class;
        void Register<TFrom>(string ttoFullName, string name) where TFrom : class;
        T Resolve<T>() where T : class;
        T Resolve<T>(string name) where T : class;

    }
}
