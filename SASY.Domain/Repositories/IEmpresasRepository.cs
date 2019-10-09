using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASY.Domain.Repositories
{
    public interface IEmpresasRepository
    {
        Task Guardar(Empresa empresa);
        Task<Empresa> ObtenerPorId(string id, bool incluirPersonas = false);
        Task<IEnumerable<Empresa>> ObtenerTodas();
        Task Eliminar(string id);
    }
}
