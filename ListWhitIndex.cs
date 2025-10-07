namespace System.Collections.Generic;

public static class ListWhitIndex
{
    public static void ForEachWithIndex(this List<string> list, Action<string, int> action)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (action == null) throw new ArgumentNullException(nameof(action));

        for (int i = 0; i < list.Count; i++)
        {
            action(list[i], i);
        }
    }

    public static string FormatterN(this int number) => $"Hola {number}";

    public static string Salud(this string value) => $"Brindemos {value}";
}