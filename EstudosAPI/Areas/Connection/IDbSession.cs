using System.Data;

namespace EstudosAPI.Areas.Connection
{
    public interface IDbSession
    {
        IDbConnection Connection { get; }

        void Dispose();
    }
}