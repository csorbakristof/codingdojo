using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20190502AniRejtvenyGeneratora
{
    class Program
    {
        /// Mask:
        /// int.MaxValue    Letters for the solution
        /// 0-...           Other fields, hidden in at least one puzzle (the one with that number)
        ///                 May be hidden in other puzzles as well.
        private int[,] mask;

        public Program(int size, int solutionLength, int puzzleCount)
        {
            Random rnd = new Random();
            mask = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    mask[i, j] = rnd.Next(puzzleCount);

            int n = solutionLength;
            while (n > 0)
            {
                int row = rnd.Next(size-1);
                int col = rnd.Next(size-1);
                if (mask[row, col] != int.MaxValue)
                {
                    mask[row, col] = int.MaxValue;
                    n--;
                }
            }
        }

        const char MaskCharacter = '#';

        public char[,] GetPuzzle(int index, string solution, int randomMaskPercentage, string randomLetterSet)
        {
            var rnd = new Random();
            var randomLetterSetLength = randomLetterSet.Length;
            int size = mask.GetLength(0);
            var result = new char[size, size];
            int nextSolutionLetterIndex = 0;
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                {
                    if (mask[r, c] == int.MaxValue)
                    {
                        // Letter of the solution
                        result[r, c] = (nextSolutionLetterIndex < solution.Length)
                            ? solution[nextSolutionLetterIndex++] : ' ';
                    }
                    else if (mask[r, c] == index)
                    {
                        // Has to be masked in this puzzle
                        result[r, c] = MaskCharacter;
                    }
                    else if (index == -1)
                    {
                        // All non-solution letters are masked for puzzle "-1"
                        result[r, c] = MaskCharacter;
                    }
                    else
                    {
                        // Does not need to be masked
                        if (randomMaskPercentage > rnd.Next(100))
                        {
                            // Masked anyway
                            result[r, c] = MaskCharacter;
                        }
                        else
                        {
                            // Showing a random letter
                            result[r, c] = randomLetterSet[rnd.Next(randomLetterSetLength)];
                        }
                    }
                }

            return result;
        }

        public static string FormatPuzzle(char[,] puzzle)
        {
            var sb = new StringBuilder();
            sb.Append(@"\begin{TAB}(e,1cm,2cm)[5pt]{");
            // Column alignment
            for (int i = 0; i < puzzle.GetLength(1); i++)
                sb.Append(@"|c");
            sb.Append(@"|}{");
            // Row alignment
            for (int i = 0; i < puzzle.GetLength(0); i++)
                sb.Append(@"|c");
            sb.AppendLine(@"|}");
            sb.AppendLine(@"\hline");

            for(int row = 0; row<puzzle.GetLength(0); row++)
            {
                for(int col = 0; col<puzzle.GetLength(1); col++)
                {
                    if (col > 0)
                        sb.Append(@" & ");
                    if (puzzle[row, col] == MaskCharacter)
                    {
                        sb.Append(@"\blacksquare");
                    }
                    else
                    {
                        sb.Append(puzzle[row, col]);
                    }
                }

                sb.AppendLine(@" \\");
            }

            sb.AppendLine(@"\end{TAB}");
            return sb.ToString();
        }

        // Skip suspicious letters...
        const string RandomLetterSet = "ABCDEFGHIJKLMNOPRSTUVYZABCDEFGHIJKLMNOPRSTUVYZÁÉÍÓÖŐÚÜŰ";

        static string GetDocumentBeginning()
        {
            return @"\documentclass[12pt]{article}
\usepackage[utf8]{inputenc}
\usepackage{amssymb}
\usepackage[thinlines]{easytable}
\begin{document}";
        }

        static string GetDocumentEnding()
        {
            return @"\end{document}";
        }

        class Config
        {
            public int PuzzleSize { get; set; }
            public int PuzzleCount => Solutions.Length;
            public int AdditionalMaskingPercentage { get; set; }
            public string[] Solutions { get; set; }
        }

        //private static Config ReadConfig(string configfilename)
        //{
        //    string json = File.ReadAllText(configfilename);
        //    Config conf = JsonConvert.DeserializeObject<Config>(json);
        //    return conf;

        //}

        static void Main(string[] args)
        {
            Config conf = new Config()
            {
                AdditionalMaskingPercentage = 50,
                PuzzleSize = 5,
                Solutions = new string[]
                {
                    "EZEGYTITOK",
                    "EZISEGYTITOK",
                    "EZMASIKTITOK",
                    "EZISTITOK"
                }
            };


            //const string configFilename = @"e:\temp\config.json";
            const string resultFilename = @"e:\temp\text1.tex";
            //Config conf = ReadConfig(configFilename);
            int solutionLength = conf.Solutions.Select(s => s.Length).Max();

            using (TextWriter writer = new StreamWriter(resultFilename))
            {

                var prog = new Program(conf.PuzzleSize, solutionLength, conf.PuzzleCount);

                writer.WriteLine(GetDocumentBeginning());

                for (int i = -1; i < conf.PuzzleCount; i++)
                {
                    string solution = i >= 0 ? conf.Solutions[i] : string.Empty;
                    var p = prog.GetPuzzle(i, solution, 50, RandomLetterSet);
                    string latexCode = Program.FormatPuzzle(p);
                    //writer.WriteLine($"\nPuzzle {i}\n");
                    writer.WriteLine("\\vspace{2cm}\n");
                    writer.Write(latexCode);
                }

                writer.WriteLine(GetDocumentEnding());
            }

            Console.WriteLine($"Output written to {resultFilename}.");
        }
    }
}
