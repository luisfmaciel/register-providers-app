using Domain;
using System.IO;

namespace Infrastructure
{
    public class ProfessionalOnFileRepository : IProfessionalRepository
    {

        string path = $"{Directory.GetCurrentDirectory()}\\Registers\\registers.txt";

        public void Delete(ulong cnpj)
        {
            if (File.Exists(path))
            {
                File.WriteAllLines(path, 
                    File.ReadLines(path).Where(l => !l.Contains(cnpj.ToString())).ToList()); 
            }
        }

        public IList<Professional> FindByCNPJ(ulong cnpj)
        {
            Professional newProfessional;
            var newList = new List<Professional>();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var listData = line.Split('|');
                        if (listData[3] == cnpj.ToString())
                        {
                            newProfessional = new Professional();

                            newProfessional.SetId(listData[0]);
                            newProfessional.Name = listData[1];
                            newProfessional.Service = listData[2];
                            newProfessional.CNPJ = Convert.ToUInt64(listData[3]);
                            newProfessional.SetCreatedAt(Convert.ToDateTime(listData[4]));
                            newProfessional.SetUpdatedAt(Convert.ToDateTime(listData[5]));
                            newProfessional.SetActive(Convert.ToBoolean(listData[6]));
                            newList.Add(newProfessional);
                        }
                    }
                }
            }
            return newList;
        }

        public void Insert(Professional professional)
        {
            if (!File.Exists(path)) {
                using StreamWriter swNew = new StreamWriter(path);
                swNew.WriteLine(professional.ToString());
                return;
            }

            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine(professional.ToString());
        }

        public void Update(string name, string serviceName, ulong cnpjUpdated, ulong oldCnpj)
        {
            Professional newProfessional;
            var listData = File.ReadAllLines(path);
            
            foreach (var line in listData)
            {
                if (line.Contains(oldCnpj.ToString())) {

                    var newList = line.Split('|');
                    newProfessional = new Professional();

                    newProfessional.SetId(newList[0]);
                    newProfessional.Name = name;
                    newProfessional.Service = serviceName;
                    newProfessional.CNPJ = cnpjUpdated;
                    newProfessional.SetCreatedAt(Convert.ToDateTime(newList[4]));
                    newProfessional.SetUpdatedAt(DateTime.Now);
                    newProfessional.SetActive(true);

                    Delete(oldCnpj);
                    Insert(newProfessional);
                }
            }
        }

        public IList<Professional> FindLastRegisters()
        {
            var listLastRegister = new List<Professional>();
            if (!File.Exists(path))
            {
                return listLastRegister;
            }
            using (StreamReader sr = new StreamReader(path))
            {
               
                Professional newProfessional;
                var i = 0;

                var listData = File.ReadAllLines(path).Reverse();
            
                foreach (var line in listData)
                {
                    if (i == 5)
                    {
                        break;
                    }

                    var newList = line.Split('|');
                    newProfessional = new Professional();

                    newProfessional.SetId(newList[0]);
                    newProfessional.Name = newList[1];
                    newProfessional.Service = newList[2];
                    newProfessional.CNPJ = Convert.ToUInt64(newList[3]);
                    newProfessional.SetCreatedAt(Convert.ToDateTime(newList[4]));
                    newProfessional.SetUpdatedAt(Convert.ToDateTime(newList[5]));
                    newProfessional.SetActive(Convert.ToBoolean(newList[6]));

                    listLastRegister.Add(newProfessional);

                    i++;
                }
                return listLastRegister;
            }
        }
    }
}