namespace _20180824BankOcr
{
    class Patterns
    {
        public const string all0 =
            " _  _  _  _  _  _  _  _  _ \n" +
            "| || || || || || || || || |\n" +
            "|_||_||_||_||_||_||_||_||_|\n" + "\n";
        public const string all1 =
            "                           \n" +
            "  |  |  |  |  |  |  |  |  |\n" +
            "  |  |  |  |  |  |  |  |  |\n" + "\n";
        public const string all2 =
            " _  _  _  _  _  _  _  _  _ \n" +
            " _| _| _| _| _| _| _| _| _|\n" +
            "|_ |_ |_ |_ |_ |_ |_ |_ |_ \n" + "\n";
        public const string sequencePattern =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _||_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|\n" + "\n";

        public const string digit1 =
            "   " +
            "  |" +
            "  |";
        public const string digit2 =
            " _ " +
            " _|" +
            "|_ ";

        public const string codeWithWrong3And7 = "12?456?89";
        public const string patternWithWrong3And7 =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _ |_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_| _||_| _|\n" + "\n";

        public const string codeWithCorrectChecksum = "345882865";
        public const string patternWithCorrectChecksum =
            " _     _  _  _  _  _  _  _ \n" +
            " _||_||_ |_||_| _||_||_ |_ \n" +
            " _|  | _||_||_||_ |_||_| _|\n" + "\n";

        public const string codeWithWrongChecksum = "111111111";
        public const string patternWithWrongChecksum =
            "                           \n" +
            "  |  |  |  |  |  |  |  |  |\n" +
            "  |  |  |  |  |  |  |  |  |\n" + "\n";

        public const string correctCodeForPatternWithSingleCharMistake = "123456789";
        public const string patternWithSingleCharMistake =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _ |_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|\n" + "\n";


    }
}
