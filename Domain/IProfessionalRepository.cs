using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProfessionalRepository
    {
        IList<Professional> FindByCNPJ(ulong cnpj);
        void Insert(Professional serviceProvider);
        void Update(string name, string serviceName, ulong cnpj);
        void Delete(Professional serviceProvider);
    }
}
