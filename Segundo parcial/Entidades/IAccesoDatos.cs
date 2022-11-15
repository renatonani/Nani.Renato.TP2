using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public interface IAccesoDatos<T>
    {       
        bool ProbarConexion();
        
        List<T> ObtenerListaDato();

        bool AgregarDato(T dato);

        bool ModificarDato(T dato);

        bool ExisteDato(T dato);
        
    }
}
