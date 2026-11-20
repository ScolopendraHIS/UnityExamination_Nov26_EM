using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

// Hjälpkod så att skripten funkar i alla Unity-versioner. Du behöver inte röra den här filen.
public static class Keys
{
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
    static Keyboard K => Keyboard.current;
    public static float Horizontal => K == null ? 0f :
        ((K.dKey.isPressed || K.rightArrowKey.isPressed) ? 1f : 0f) - ((K.aKey.isPressed || K.leftArrowKey.isPressed) ? 1f : 0f);
    public static bool JumpPressed => K != null && (K.spaceKey.wasPressedThisFrame || K.wKey.wasPressedThisFrame || K.upArrowKey.wasPressedThisFrame);
    public static bool JumpReleased => K != null && (K.spaceKey.wasReleasedThisFrame || K.wKey.wasReleasedThisFrame || K.upArrowKey.wasReleasedThisFrame);
    public static bool DownHeld => K != null && (K.sKey.isPressed || K.downArrowKey.isPressed);
    public static bool TalkPressed => K != null && K.eKey.wasPressedThisFrame;
    public static bool RestartPressed => K != null && K.rKey.wasPressedThisFrame;
#else
    public static float Horizontal => Input.GetAxisRaw("Horizontal");
    public static bool JumpPressed => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    public static bool JumpReleased => Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    public static bool DownHeld => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    public static bool TalkPressed => Input.GetKeyDown(KeyCode.E);
    public static bool RestartPressed => Input.GetKeyDown(KeyCode.R);
#endif
}

public static class RbCompat
{
    public static Vector2 GetVel(this Rigidbody2D rb)
    {
#if UNITY_6000_0_OR_NEWER
        return rb.linearVelocity;
#else
        return rb.velocity;
#endif
    }
    public static void SetVel(this Rigidbody2D rb, Vector2 v)
    {
#if UNITY_6000_0_OR_NEWER
        rb.linearVelocity = v;
#else
        rb.velocity = v;
#endif
    }
}
