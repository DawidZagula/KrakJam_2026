using UnityEngine;

public class Enums : MonoBehaviour
{
    
}

public enum Mask
{
    Repair,
    Destruction,
    Consolation,
    Fright
}

public enum MovableParts
{
    Floor,
    Background,
    Foreground,
    Edge
}

public enum EnvironmentLevel
{
    TestLevel0,
    TestLevel1,
    TestLevel2
}

public enum Scenes
{
    MainMenu,
    GameScene
}

public enum GameState
{
    NotStarted,
    Playing,
    GameOver
}