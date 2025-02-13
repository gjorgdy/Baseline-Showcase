namespace Baseline.Models.Tiles;

public class SkillsTileViewModel(
    string title,
    int width,
    int height
) : TileViewModel(title, width, height)
{

    public List<Skill> Skills { get; } = [];

    public SkillsTileViewModel add(string skillName, int percentage)
    {
        Skills.Add(new Skill(skillName, percentage));
        return this;
    }

    public class Skill(string name, int percentage)
    {
        public string Name => name;
        public int Percentage => percentage;
    }

}