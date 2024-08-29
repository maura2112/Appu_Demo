using System;

public static class Extensions {

    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static int ToInt(this Enum enumValue) {
        return (int)((object)enumValue);
    }
}