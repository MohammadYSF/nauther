namespace Nauther.Framework.Application.Helpers;

public static class NationalCodeValidation
{
    public static bool IsValidNationalCode(this string nationalCode)
    {
        if (new string(nationalCode[0], 10) == nationalCode)
            return false;
        var sum = 0;
        for (var i = 0; i < 9; i++)
        {
            sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
        }

        var reminder = sum % 11;
        var controlDigit = int.Parse(nationalCode[9].ToString());
        return (reminder < 2 && controlDigit == reminder) || (reminder >= 2 && controlDigit == (11 - reminder));
    }
}