namespace Assignment_1.Models
{
    public class Job
    {
        public string JobTitle { get; set; }
        public int Salary { get; set; }

        public override string ToString()
        {
            return JobTitle + ", " + Salary;
        }
    }
}