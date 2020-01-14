using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.CQRS.Standard21.Domain
{
    public static class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context, bool restore = false)
        {
            if (restore) context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Employees.Any())
            {
                context.Roles.AddRange(Enum.GetValues(typeof(Role)).Cast<Role>().ToList().Select(s => new ApplicationRole { Name = s.ToString(), NormalizedName = s.ToString() }));

                context.SaveChanges();


                context.VacationStatuses.AddRange(Enum.GetValues(typeof(VacationState)).Cast<VacationState>().ToList().Select(s => new VacationStatus(s.ToString())));

                context.SaveChanges();


                var standard = new VacationType("Standard", 26, true, null);
                context.VacationTypes.Add(standard);

                var ondemand = new VacationType("On Demand", 4, false, standard);
                context.VacationTypes.Add(ondemand);

                context.SaveChanges();


                context.ContractTypes.AddRange(Enum.GetValues(typeof(ContractForm)).Cast<ContractForm>().ToList().Select(s => new ContractType(s.ToString())));

                context.SaveChanges();


                var mgmt = new Department("Management", "Company management office.", 1);
                context.Departments.Add(mgmt);

                var adm = new Department("Administration", "Company administration bureaucracy.", 2);
                context.Departments.Add(adm);

                var fin = new Department("Finance", "Company financial operations.", 2);
                context.Departments.Add(fin);

                var rnd = new Department("Research and Development", "Research and development facility.", 2);
                context.Departments.Add(rnd);

                var hr = new Department("Human Resources", "Human resources office.", 2);
                context.Departments.Add(hr);

                var law = new Department("Legal", "Legal department office.", 2);
                context.Departments.Add(law);

                context.SaveChanges();


                var ceo = new Position("Chief Executive Officer", "The most senior corporate officer, executive, leader or administrator in charge of managing an organization.", 8);
                context.Positions.Add(ceo);

                var cto = new Position("Chief Technology Officer", "The occupation is focused on scientific and technological issues within an organization.", 7);
                context.Positions.Add(cto);

                var cmo = new Position("Chief Marketing Officer", "The corporate executive responsible for marketing activities in an organization.", 7);
                context.Positions.Add(cmo);

                var cfo = new Position("Chief Financial Officer", "The corporate officer primarily responsible for managing the financial risks of the corporation.", 7);
                context.Positions.Add(cfo);

                var gm = new Position("General Manager", "The executive who has overall responsibility for managing both the revenue and cost elements of a corporation.", 6);
                context.Positions.Add(gm);

                var se = new Position("Senior Engineer", "The people who invent, design, analyse, build and test machines, systems, structures and materials.", 5);
                context.Positions.Add(se);

                var sx = new Position("Secretary", "Personal assistant is a person whose work consists of supporting management.", 4);
                context.Positions.Add(sx);

                context.SaveChanges();


                var holidays = new List<Holiday>()
                {
                    new Holiday(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 4, 2, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 5, 1, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 5, 3, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 5, 31, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 8, 15, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 12, 25, 0, 0, 0, TimeSpan.FromHours(0))),
                    new Holiday(new DateTimeOffset(2018, 12, 26, 0, 0, 0, TimeSpan.FromHours(0)))
                };

                context.Holidays.AddRange(holidays);

                context.SaveChanges();


                var luciusdebeers = new Employee(null, "John", "Smith", new DateTimeOffset(1966, 10, 22, 0, 0, 0, TimeSpan.FromHours(0)), true, "john.smith@enterprise.org", "000000000", "Baker Street 221", "01-100", "London", ceo, mgmt, null);
                context.Employees.Add(luciusdebeers);

                var morganeverett = new Employee(null, "James", "Everett", new DateTimeOffset(1982, 5, 16, 0, 0, 0, TimeSpan.FromHours(0)), true, "james.everett@enterprise.org", "000000000", "Sunset Boulevard 42", "22-369", "Los Angeles", cto, mgmt, luciusdebeers);
                context.Employees.Add(morganeverett);

                var elizabethduclare = new Employee(null, "Elizabeth", "Dumier", new DateTimeOffset(1980, 9, 3, 0, 0, 0, TimeSpan.FromHours(0)), false, "elizabeth.dumier@enterprise.org", "000000000", "Champs Élysées 100", "10-200", "Paris", cmo, mgmt, luciusdebeers);
                context.Employees.Add(elizabethduclare);

                var stantondowd = new Employee(null, "Paul", "Weathers", new DateTimeOffset(1983, 12, 6, 0, 0, 0, TimeSpan.FromHours(0)), true, "paul.weathers@enterprise.org", "000000000", "Wall Street 8", "55-128", "New York", cfo, mgmt, luciusdebeers);
                context.Employees.Add(stantondowd);

                var robertpage = new Employee(null, "Robert", "Page", new DateTimeOffset(1990, 7, 1, 0, 0, 0, TimeSpan.FromHours(0)), true, "robert.page@enterprise.org", "000000000", "Hell's Kitchen 11", "51-666", "New York", gm, rnd, morganeverett);
                context.Employees.Add(robertpage);

                var garysavage = new Employee(null, "Gary", "Savage", new DateTimeOffset(1991, 2, 28, 0, 0, 0, TimeSpan.FromHours(0)), true, "gary.savage@enterprise.org", "000000000", "Battery Park 16", "52-256", "New York", se, rnd, robertpage);
                context.Employees.Add(garysavage);

                context.SaveChanges();


                var ld = new Contract(luciusdebeers.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), null, ContractForm.Employment, 1000000);
                context.Contracts.Add(ld);

                var me = new Contract(morganeverett.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), null, ContractForm.Employment, 800000);
                context.Contracts.Add(me);

                var ed = new Contract(elizabethduclare.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), null, ContractForm.Employment, 800000);
                context.Contracts.Add(ed);

                var sd = new Contract(stantondowd.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), null, ContractForm.Employment, 800000);
                context.Contracts.Add(sd);

                var rp = new Contract(robertpage.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), null, ContractForm.Employment, 500000);
                context.Contracts.Add(rp);

                var gs = new Contract(garysavage.Id, new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2052, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), ContractForm.Employment, 300000);
                context.Contracts.Add(gs);

                context.SaveChanges();


                var db = new AvailableVacationDays(luciusdebeers.Id, 2018, standard, 26);
                context.AvailableVacationsDays.Add(db);

                var de = new AvailableVacationDays(luciusdebeers.Id, 2018, ondemand, 4);
                context.AvailableVacationsDays.Add(de);

                var ev = new AvailableVacationDays(morganeverett.Id, 2018, standard, 26);
                context.AvailableVacationsDays.Add(ev);

                var dc = new AvailableVacationDays(elizabethduclare.Id, 2018, standard, 26);
                context.AvailableVacationsDays.Add(dc);

                var dw = new AvailableVacationDays(stantondowd.Id, 2018, standard, 26);
                context.AvailableVacationsDays.Add(dw);

                var pg = new AvailableVacationDays(robertpage.Id, 2018, standard, 26);
                context.AvailableVacationsDays.Add(pg);

                var sa = new AvailableVacationDays(garysavage.Id, 2018, standard, 20);
                context.AvailableVacationsDays.Add(sa);

                context.SaveChanges();


                var vacations = new List<Vacation>
                {
                    new Vacation(luciusdebeers.Id, "", new DateTimeOffset(2018, 4, 11, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 4, 11, 0, 0, 0, TimeSpan.FromHours(0)), ondemand, VacationState.Accepted),
                    new Vacation(luciusdebeers.Id, "", new DateTimeOffset(2018, 4, 21, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 4, 29, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Accepted),
                    new Vacation(luciusdebeers.Id, "", new DateTimeOffset(2018, 9, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 9, 29, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Accepted),
                    new Vacation(luciusdebeers.Id, "", new DateTimeOffset(2018, 10, 12, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 10, 12, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Rejected),
                    new Vacation(luciusdebeers.Id, "", new DateTimeOffset(2018, 11, 13, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 11, 13, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Cancelled),
                    new Vacation(morganeverett.Id, "", new DateTimeOffset(2018, 12, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested),
                    new Vacation(elizabethduclare.Id, "", new DateTimeOffset(2018, 12, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested),
                    new Vacation(stantondowd.Id, "", new DateTimeOffset(2018, 12, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested),
                    new Vacation(robertpage.Id, "", new DateTimeOffset(2018, 12, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested),
                    new Vacation(garysavage.Id, "", new DateTimeOffset(2018, 12, 24, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 31, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested),
                    new Vacation(garysavage.Id, "", new DateTimeOffset(2018, 12, 21, 0, 0, 0, TimeSpan.FromHours(0)), new DateTimeOffset(2018, 12, 21, 0, 0, 0, TimeSpan.FromHours(0)), standard, VacationState.Requested)
                };

                context.Vacations.AddRange(vacations);

                context.SaveChanges();


                foreach (var role in context.Roles) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = luciusdebeers.Id, RoleId = role.Id });
                foreach (var role in context.Roles.Where(r => r.Id != (int)Role.CompanyManagement)) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = morganeverett.Id, RoleId = role.Id });
                foreach (var role in context.Roles.Where(r => r.Id != (int)Role.CompanyManagement)) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = elizabethduclare.Id, RoleId = role.Id });
                foreach (var role in context.Roles.Where(r => r.Id != (int)Role.CompanyManagement)) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = stantondowd.Id, RoleId = role.Id });
                foreach (var role in context.Roles.Where(r => r.Id == (int)Role.VacationManagement)) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = robertpage.Id, RoleId = role.Id });
                foreach (var role in context.Roles.Where(r => r.Id == (int)Role.VacationManagement)) context.UserRoles.Add(new IdentityUserRole<int>() { UserId = garysavage.Id, RoleId = role.Id });

                context.SaveChanges();
            }
        }
    }
}