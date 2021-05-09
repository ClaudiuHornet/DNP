using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models {
public class Adult : Person {
    [Required]
    public Job JobTitle { get; set; }

    public override string ToString()
    {
        return Id + ", " + FirstName + ", " + LastName + ", " + JobTitle.ToString();
    }
}
}