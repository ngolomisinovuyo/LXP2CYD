using System;
namespace LPX2YCDProject2020.Models.YearPlans
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int YearPlanId { get; set; }
    }
}
