namespace Assignment_1.Models {
public class Adult : Person {
    public Job JobTitle { get; set; }
    public override string ToString()
    {
        return Id + ", " + FirstName + ", " + LastName + ", " + JobTitle.ToString();
    }
}
}