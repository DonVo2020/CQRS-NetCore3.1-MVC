using DonVo.CQRS.Standard21.Domain.Model.Company;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class StatisticViewModel
    {
        public int Year { get; set; }

        public string Type { get; set; }

        public int CurrentYearAvailableDays { get; set; }

        public int PreviousYearOverdueDays { get; set; }

        public int TotalVacationDays { get; set; }

        public int Requested { get; set; }

        public int Accepted { get; set; }

        public int Rejected { get; set; }

        public int Cancelled { get; set; }

        public int Planned { get; set; }

        public int DaysLeft { get; set; }

        public void LoadFromDomain(VacationStatistics entity)
        {
            Year = entity.Year;
            Type = entity.Type.Name;
            CurrentYearAvailableDays = entity.CurrentYearAvailableDays;
            PreviousYearOverdueDays = entity.PreviousYearOverdueDays;
            TotalVacationDays = entity.TotalVacationDays;
            Requested = entity.Requested;
            Accepted = entity.Accepted;
            Rejected = entity.Rejected;
            Cancelled = entity.Cancelled;
            Planned = entity.Planned;
            DaysLeft = entity.DaysLeft;
        }
    }
}