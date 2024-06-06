namespace Snackis.Helpers
{
    public class HelperFunctions
    {
        public static string SwearFilter(string input)
        {
            List<string> swears = new List<string>
        {
            "fuck",
            "shit",
            "bitch",
            "cunt",
            "whore",
            "ass",
            "cock"

        };
            foreach (var swear in swears)
            {
                string censored = new string('*', swear.Length);
                input = input.Replace(swear, censored, StringComparison.OrdinalIgnoreCase);
            }
            return input;
        }
    }
}
