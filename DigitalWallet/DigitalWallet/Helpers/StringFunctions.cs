namespace DigitalWallet.Helpers
{
    public static class StringFunctions
    {
        public static string MaskTC(this string tc)
        {
            if (string.IsNullOrEmpty(tc) || tc.Length != 11)
                return tc;

            return $"{tc.Substring(0, 3)}*****{tc.Substring(8, 3)}";

        }
    }
}
