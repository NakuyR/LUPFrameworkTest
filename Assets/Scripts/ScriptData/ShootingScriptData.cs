[System.Serializable]
public class ShootingScriptData
{
    public string name;
    public string description;
    public string stat;
    public int cur;

    public ShootingScriptData(string name, string description, string stat, int cur)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.cur = cur;
    }
}
