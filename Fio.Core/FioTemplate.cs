namespace Fio.Core;

public sealed record FioTemplate(int SurnameParts, int Initials)
{
    public bool Matches(FioTemplate other)
    {
        return SurnameParts == other.SurnameParts && Initials == other.Initials;
    }

    public override string ToString()
    {
        return $"{SurnameParts} parte(s) de apellido, {Initials} inicial(es)";
    }
}

public static class FioValidator
{
    public static bool TryParse(string? value, out FioTemplate? template)
    {
        template = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || !IsLastNameValid(parts[0]) || !AreInitialsValid(parts[1]))
            return false;

        template = new FioTemplate(
            parts[0].Split('-').Length,
            parts[1].Split('.', StringSplitOptions.RemoveEmptyEntries).Length);
        return true;
    }

    private static bool IsLastNameValid(string name)
    {
        if (name.StartsWith('-') || name.EndsWith('-'))
            return false;

        return name.Split('-').All(part =>
            part.Length > 0 && part.All(char.IsLetter));
    }

    private static bool AreInitialsValid(string initials)
    {
        if (!initials.EndsWith('.') || initials.StartsWith('.') || initials.Contains(".."))
            return false;

        var blocks = initials.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return blocks.Length > 0 && blocks.All(block =>
            block.Length == 1 && char.IsLetter(block[0]) && char.IsUpper(block[0]));
    }
}
