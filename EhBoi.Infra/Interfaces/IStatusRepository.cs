using EhBoi.Infra.Data;

namespace EhBoi.Infra.Interfaces
{
    public interface IStatusRepository
    {
        public StatusDoBanco ObterStatusDoBanco();
    }
}
