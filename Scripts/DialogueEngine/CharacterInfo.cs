[System.Serializable]
public class CharacterInfo
{
    // name of the character
    public string name;

    // transports the player to the scene with the given scene name after the dialogue box ends
    // if empty, no scene transition occurs
    public string sceneName = "";
}
