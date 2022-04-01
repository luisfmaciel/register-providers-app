using Domain;
using System.Globalization;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IProfessionalRepository _respository;

        public Worker(IProfessionalRepository repository)
        {
            _respository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string? option;

            void ShowHeader()
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Service Provider Manager");
                Console.WriteLine("----------------------------------------");
            }

            void ShowMenu()
            {
                Console.WriteLine("[1] - Add new professional");
                Console.WriteLine("[2] - Search by professional");
                Console.WriteLine("[3] - Update professional data");
                Console.WriteLine("[4] - Delete professional data");
                Console.WriteLine("[5] - Exit\n");

                Console.WriteLine("Enter an option:");
            }

            ShowHeader();
            var listLastRegisters = _respository.FindLastRegisters();
            if (listLastRegisters.Count == 0)
            {
                Console.WriteLine("No register found...");
            }
            else
            {
                foreach (var reg in listLastRegisters)
                {
                    Console.WriteLine($"\nId: {reg.Id}");
                    Console.WriteLine($"Name: {reg.Name}");
                    Console.WriteLine($"Service: {reg.Service}");
                    Console.WriteLine($"CNPJ: {Convert.ToUInt64(reg.CNPJ).ToString(@"00\.000\.000\/0000\-00")}");
                    if (reg.GetActive() == true)
                    {
                        Console.WriteLine("Status: Ativo");
                    }
                    else
                    {
                        Console.WriteLine("Status: Inativo");
                    }
                    Console.WriteLine($"Created Date: {reg.CreatedAt}");
                    Console.WriteLine($"Updated Date: {reg.UpdatedAt}");
                    Console.WriteLine("----------------------------------------");
                }
            }

            Console.WriteLine("\nPress [enter] to go to main menu...");
            Console.ReadKey();
            Console.Clear();

            do
            {
                ShowHeader();
                ShowMenu();

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        {
                            Console.Clear();
                            ShowHeader();

                            Console.WriteLine("Enter professional name:");
                            var professionalName = Console.ReadLine();

                            if (String.IsNullOrWhiteSpace(professionalName))
                            {
                                Console.WriteLine("Invalid name!");
                                break;
                            }

                            Console.WriteLine("Enter service name:");
                            var serviceName = Console.ReadLine();

                            if (String.IsNullOrWhiteSpace(serviceName))
                            {
                                Console.WriteLine("Invalid service name!");
                                break;
                            }

                            Console.WriteLine("Enter the CNPJ (Only numbers):");
                            var professionalCnpj = Console.ReadLine();

                            var validateCnpj = ulong.TryParse(professionalCnpj, out ulong cnpj);
                            if (!validateCnpj || professionalCnpj?.Length < 14 || professionalCnpj?.Length > 14)
                            {
                                Console.WriteLine("Invalid CNPJ");
                                break;
                            }

                            Console.WriteLine("\nConfirm the data below:");
                            Console.WriteLine($"Name: {professionalName}");
                            Console.WriteLine($"Serice: {serviceName}");
                            Console.WriteLine($"CNPJ: {Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00")}");

                            Console.WriteLine("\n[1] - Save");
                            Console.WriteLine("[2] - Cancel");

                            var confirmation = Console.ReadLine();

                            if (confirmation == "1")
                            {
                                var newProfessional = new Professional();

                                newProfessional.Name = professionalName;
                                newProfessional.Service = serviceName;
                                newProfessional.CNPJ = cnpj;

                                _respository.Insert(newProfessional);

                                Console.WriteLine("Success Register");
                            }
                            else if (confirmation == "2")
                            {
                                Console.WriteLine("Operation canceled!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid option");
                            }
                            break;
                        }

                    case "2":
                        {
                            Console.Clear();
                            ShowHeader();

                            Console.WriteLine("Search by CNPJ (Only numbers):");
                            var cnpjSearched = Console.ReadLine();
                            var validateCnpj = ulong.TryParse(cnpjSearched, out ulong cnpj);

                            if (!validateCnpj || cnpjSearched?.Length < 14 || cnpjSearched?.Length > 14)
                            {
                                Console.WriteLine("Invalid CNPJ");
                                break;
                            }

                            var list = _respository.FindByCNPJ(cnpj);

                            if (list.Count == 0)
                            {
                                Console.WriteLine("\nNo professionals found...");
                                break;
                            }

                            foreach (var item in list)
                            {
                                Console.WriteLine($"\nId: {item.Id}");
                                Console.WriteLine($"Name: {item.Name}");
                                Console.WriteLine($"Service: {item.Service}");
                                Console.WriteLine($"CNPJ: {Convert.ToUInt64(item.CNPJ).ToString(@"00\.000\.000\/0000\-00")}");
                                if (item.GetActive() == true)
                                {
                                    Console.WriteLine("Status: Ativo");
                                } 
                                else
                                {
                                    Console.WriteLine("Status: Inativo");
                                }
                                Console.WriteLine($"Created Date: {item.CreatedAt}");
                                Console.WriteLine($"Updated Date: {item.UpdatedAt}");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.Clear();
                            ShowHeader();

                            Console.WriteLine("Search by CNPJ (Only numbers):");
                            var cnpjSearched = Console.ReadLine();
                            var validateCnpj = ulong.TryParse(cnpjSearched, out ulong cnpj);

                            if (!validateCnpj || cnpjSearched?.Length < 14 || cnpjSearched?.Length > 14)
                            {
                                Console.WriteLine("Invalid CNPJ");
                                break;
                            }

                            var list = _respository.FindByCNPJ(cnpj);

                            if (list.Count == 0)
                            {
                                Console.WriteLine("\nNo professionals found...");
                                break;
                            }

                            foreach (var item in list)
                            {
                                Console.WriteLine($"\nId: {item.Id}");
                                Console.WriteLine($"Name: {item.Name}");
                                Console.WriteLine($"Service: {item.Service}");
                                Console.WriteLine($"CNPJ: {Convert.ToUInt64(item.CNPJ).ToString(@"00\.000\.000\/0000\-00")}");
                                if (item.GetActive() == true)
                                {
                                    Console.WriteLine("Status: Ativo");
                                }
                                else
                                {
                                    Console.WriteLine("Status: Inativo");
                                }
                                Console.WriteLine($"Created Date: {item.CreatedAt}");
                                Console.WriteLine($"Updated Date: {item.UpdatedAt}");
                            }

                            Console.WriteLine("\nEnter professional name:");
                            var professionalName = Console.ReadLine();

                            if (String.IsNullOrWhiteSpace(professionalName))
                            {
                                Console.WriteLine("Invalid name!");
                                break;
                            }

                            Console.WriteLine("Enter service name:");
                            var serviceName = Console.ReadLine();

                            if (String.IsNullOrWhiteSpace(serviceName))
                            {
                                Console.WriteLine("Invalid service name!");
                                break;
                            }

                            Console.WriteLine("Enter the CNPJ (Only numbers):");
                            var professionalCnpj = Console.ReadLine();

                            var validateCnpjUpdate = ulong.TryParse(professionalCnpj, out ulong cnpjUpdate);
                            if (!validateCnpjUpdate || professionalCnpj?.Length < 14 || professionalCnpj?.Length > 14)
                            {
                                Console.WriteLine("Invalid CNPJ");
                                break;
                            }

                            _respository.Update(professionalName, serviceName, cnpjUpdate, cnpj);

                            Console.WriteLine("Successfully Updated");

                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            ShowHeader();

                            Console.WriteLine("Search by CNPJ (Only numbers):");
                            var cnpjSearched = Console.ReadLine();
                            var validateCnpj = ulong.TryParse(cnpjSearched, out ulong cnpj);

                            if (!validateCnpj || cnpjSearched?.Length < 14 || cnpjSearched?.Length > 14)
                            {
                                Console.WriteLine("Invalid CNPJ");
                                break;
                            }

                            var list = _respository.FindByCNPJ(cnpj);

                            if (list.Count == 0)
                            {
                                Console.WriteLine("\nNo professionals found...");
                                break;
                            }

                            foreach (var item in list)
                            {
                                Console.WriteLine($"\nId: {item.Id}");
                                Console.WriteLine($"Name: {item.Name}");
                                Console.WriteLine($"Service: {item.Service}");
                                Console.WriteLine($"CNPJ: {Convert.ToUInt64(item.CNPJ).ToString(@"00\.000\.000\/0000\-00")}");
                                if (item.GetActive() == true)
                                {
                                    Console.WriteLine("Status: Ativo");
                                }
                                else
                                {
                                    Console.WriteLine("Status: Inativo");
                                }
                                Console.WriteLine($"Created Date: {item.CreatedAt}");
                                Console.WriteLine($"Updated Date: {item.UpdatedAt}");
                            }

                            Console.WriteLine("\n[1] - Delete");
                            Console.WriteLine("[2] - Cancel");

                            var confirmation = Console.ReadLine();

                            if (confirmation == "1")
                            {
                                _respository.Delete(cnpj);

                                Console.WriteLine("Successfully Deleted");
                            }
                            else if (confirmation == "2")
                            {
                                Console.WriteLine("Operation canceled!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid option");
                            }
                            break;
                        }
                    case "5":
                        Console.WriteLine("Shutting down...");
                        break;
                    default: 
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine("\nPress [enter] to return to main menu...");
                Console.ReadKey();
                Console.Clear();

            } while (option != "5"); 
        }
    }
}