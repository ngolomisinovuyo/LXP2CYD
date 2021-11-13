using System;
namespace LPX2YCDProject2020.Models.YearPlans
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BudgtedAmount { get; set; }
        public double AmountSpent { get; set; }
        public int? YearPlanId { get; set; }
    }
}
