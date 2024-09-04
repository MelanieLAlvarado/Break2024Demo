using UnityEngine;

public class GameplayWidget : MonoBehaviour
{
    
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;

    public JoyStick MoveStick
    {
        get => moveStick;
        private set => moveStick = value;
    }

    public JoyStick AimStick
    {
        get => aimStick;
        private set => aimStick = value;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
