using UnityEngine;

public class GameplayWidget : Widget
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    public Joystick MoveStick
    {
        get => moveStick;
        private set => moveStick = value;
    }

    public Joystick AimStick
    {
        get => aimStick;
        private set => aimStick = value;
    }
    
}
