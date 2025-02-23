namespace Core.Models.Tiles;

public class ExperiencesTileViewModel(
    string title,
    int width,
    int height
) : TileViewModel(title, width, height)
{
    public List<Experience> Experiences { get; } = [];

    public ExperiencesTileViewModel add(string name, string timePeriod, string location, string? description = null)
    {
        Experiences.Add(new Experience(name, timePeriod, location, description));
        return this;
    }

    public class Experience(
        string name,
        string timePeriod,
        string location,
        string? description = null
    )
    {

        public string Name {get;} = name;
        public string TimePeriod {get;} = timePeriod;
        public string Location {get;} = location;

        public string? Description {get;} = description;
    }

}