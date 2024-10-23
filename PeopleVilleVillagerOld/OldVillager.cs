namespace PeopleVilleEngine.Villagers;
public class OldVillager : BaseVillager
{
    public OldVillager(Village village) : base(village)
    {
        Age = 65;

    }
    public OldVillager(Village village, int age) : base(village)
    {
        Age = age;
    }
}
