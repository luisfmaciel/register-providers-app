using Domain;

namespace Infrastructure
{
    public class ProfessionalRepository : IProfessionalRepository
    {

        private static List<Professional> listProfessionals = new List<Professional>();

        public void Delete(Professional serviceProvider)
        {
            throw new NotImplementedException();
        }

        public IList<Professional> FindByCNPJ(ulong cnpj)
        {
            return listProfessionals.Where(professional => professional.CNPJ == cnpj).ToList();
        }

        public void Insert(Professional serviceProvider)    
        {
            listProfessionals.Add(serviceProvider);
        }

        public void Update(string name, string serviceName, ulong cnpj)
        {
            var professionals = FindByCNPJ(cnpj);
            foreach (var professional in professionals)
            {
                professional.Name = name;
                professional.Service = serviceName;
                professional.CNPJ = cnpj;
                professional.SetUpdatedAt(DateTime.Now);
            }
        }
    }
}