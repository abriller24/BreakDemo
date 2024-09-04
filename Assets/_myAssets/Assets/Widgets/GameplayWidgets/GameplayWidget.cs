using UnityEngine;

public class GameplayWidget : MonoBehaviour
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
