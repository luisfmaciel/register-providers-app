using Domain;

namespace Infrastructure
{
    public class ProfessionalInMemoryRepository : IProfessionalRepository
    {

        private static List<Professional> listProfessionals = new List<Professional>();

        public void Delete(ulong cnpj)
        {
            var professionals = FindByCNPJ(cnpj);
            foreach(var professional in professionals)
            {
                listProfessionals.Remove(professional);
            }
        }

        public IList<Professional> FindByCNPJ(ulong cnpj)
        {
            return listProfessionals.Where(professional => professional.CNPJ == cnpj).ToList();
        }

        public void Insert(Professional serviceProvider)    
        {
            listProfessionals.Add(serviceProvider);
        }

        public void Update(string name, string serviceName, ulong cnpjUpdated, ulong oldCnpj)
        {
            var professionals = FindByCNPJ(cnpjUpdated);
            foreach (var professional in professionals)
            {
                professional.Name = name;
                professional.Service = serviceName;
                professional.CNPJ = cnpjUpdated;
                professional.SetUpdatedAt(DateTime.Now);
            }
        }

        public IList<Professional> FindLastRegisters()
        {
            listProfessionals.Reverse();
            var listLastRegister = new List<Professional>();
            if (listLastRegister.Count > 0)
            {
                for (int i = 0; listProfessionals.Count < 5; i++)
                {
                    listLastRegister.Add(listProfessionals[i]);
                }
            }
            return listLastRegister;
        }
    }
}