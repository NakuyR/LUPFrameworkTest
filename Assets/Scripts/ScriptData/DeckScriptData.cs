[System.Serializable]
public class DeckScriptData
{
    public string name;
    public string description;
    public string stat;
    public int gold;

    public DeckScriptData(string name, string description, string stat, int gold)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.gold = gold;
    }
}
