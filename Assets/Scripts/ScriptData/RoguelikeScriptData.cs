[System.Serializable]
public class RoguelikeScriptData
{
    public string name;
    public string description;
    public string stat;
    public int gold;

    public RoguelikeScriptData(string name, string description, string stat, int gold)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.gold = gold;
    }
}
              