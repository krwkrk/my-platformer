[System.Serializable]
public class SaveData
{
    public const int CurrentVersion = 2;

    // ЗАВЖДИ перше поле: за ним гра розпізнає версію формату збереження
    public int version = CurrentVersion;

    public int coins;
    public int levelIndex;
}