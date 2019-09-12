using UnityEngine;

public static class InputController
{
    public static int Horizontal()
    {
        var value = Input.GetAxis("Horizontal");

        if (value > 0) return 1;
        if (value < 0) return -1;
        return 0;
    }

    public static int Vertical()
    {
        var value = Input.GetAxis("Vertical");

        if (value > 0) return 1;
        if (value < 0) return -1;
        return 0;
    }

    public static bool Button1()
    {
        return Input.GetKey("j");
    }

    public static bool Button2()
    {
        return Input.GetKey("k");
    }

    public static bool Button3()
    {
        return Input.GetKey("l");
    }

    public static bool Button4()
    {
        return Input.GetButton("Fire1") ||
               Input.GetKey("n");
    }

    public static bool Button5()
    {
        return Input.GetButton("Fire2") ||
               Input.GetKey("m");
    }

    public static bool Button6()
    {
        return Input.GetKey(",");
    }
}
