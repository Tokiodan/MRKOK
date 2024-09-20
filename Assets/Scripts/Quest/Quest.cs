[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted;
    public bool isAccepted;
    private System.Func<bool> clearCondition; // Condition to check for completion

    public Quest(string name, string desc, System.Func<bool> condition)
    {
        questName = name;
        description = desc;
        isCompleted = false;
        isAccepted = false;
        clearCondition = condition; // Assign the condition
    }

    public bool IsQuestCompleted()
    {
        return clearCondition != null && clearCondition(); // Check the condition
    }
}
