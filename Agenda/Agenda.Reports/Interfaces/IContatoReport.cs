using Agenda.Data.Entities;

namespace Agenda.Reports.Interfaces
{
    public interface IContatoReport
    {
        public byte[] CreateReport(List<Contato> contatos, Usuario usuario);
    }
}
