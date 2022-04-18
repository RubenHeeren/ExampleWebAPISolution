namespace ExampleWebAPI.Data.Models;

public class PostCreateUpdateDTO
{
    // string.Empty is used to initialize this property to an empty string (just empty quotes: "") to avoid nullability warnings.
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool Published { get; set; }
}
