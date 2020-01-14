using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public sealed class VacationStatistics : ValueObject<VacationStatistics>
	{
		public int Year { get; }
		public VacationType Type { get; }
		public int CurrentYearAvailableDays { get; }
		public int PreviousYearOverdueDays { get; }
		public int TotalVacationDays { get; }
		public int Requested { get; }
		public int Accepted { get; }
		public int Rejected { get; }
		public int Cancelled { get; }
		public int Planned { get; }
		public int DaysLeft => TotalVacationDays - Accepted;

		public VacationStatistics(int year, VacationType type, int currentyearavailabledays, int previousyearoverduedays, int totalvacationdays, int requested, int accepted, int rejected, int cancelled, int planned)
		{
			Year = year;
			Type = type;
			CurrentYearAvailableDays = currentyearavailabledays;
			PreviousYearOverdueDays = previousyearoverduedays;
			TotalVacationDays = totalvacationdays;
			Requested = requested;
			Accepted = accepted;
			Rejected = rejected;
			Cancelled = cancelled;
			Planned = planned;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Year;
			yield return Type;
			yield return CurrentYearAvailableDays;
			yield return PreviousYearOverdueDays;
			yield return TotalVacationDays;
			yield return Requested;
			yield return Accepted;
			yield return Rejected;
			yield return Cancelled;
			yield return Planned;
		}
	}
}