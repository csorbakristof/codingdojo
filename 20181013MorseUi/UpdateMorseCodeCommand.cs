using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace _20181013MorseUi
{
    public class UpdateMorseCodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainViewModel mainViewModel;

        public UpdateMorseCodeCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object textToTransform)
        {
            var dashArray = UpdateMorseCodeDashArray(textToTransform as string);
            dashArray.Add(InterTextClearance);
            mainViewModel.MorseLineDashArray = dashArray;
        }

        public DoubleCollection UpdateMorseCodeDashArray(string text)
        {
            // https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.shapes.shape.strokedasharray?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(Windows.UI.Xaml.Shapes.Shape.StrokeDashArray)%3Bk(VS.XamlEditor)%3Bk(SolutionItemsProject)%3Bk(TargetFrameworkMoniker-.NETCore%2CVersion%3Dv5.0)%26rd%3Dtrue%26f%3D255%26MSPPError%3D-2147217396
            return GetStrokeLengths(GetMorseCodeDotString(text));
        }

        public double InterTextClearance { get; set; } = 10.0;
        public const double LongSignal = 2.0;
        public const double ShortSignal = 1.0;
        public const double SignalClearance = 1.0;
        public const double LetterClearance = 3.0;

        public DoubleCollection GetStrokeLengths(string morseCode)
        {
            DoubleCollection result = new DoubleCollection();
            bool isFirst = true;
            bool lastWasLetterClearance = false;
            foreach (var c in morseCode.ToCharArray())
            {
                // There is no SignalClearance before and after spaces
                //  indicating inter-letter clearance.
                if (!isFirst && c!=' ' && !lastWasLetterClearance)
                    result.Add(SignalClearance);
                result.Add(c == '-' ? LongSignal :
                    (c == '.' ? ShortSignal : LetterClearance));

                lastWasLetterClearance = (c==' ');
                isFirst = false;
            }
            return result;
        }

        public string GetMorseCodeDotString(string text)
        {
            var sb = new StringBuilder();
            bool isFirst = true;
            foreach (var c in text.ToCharArray())
            {
                if (!morseCodes.ContainsKey(c))
                    return "";
                if (!isFirst)
                    sb.Append(' ');
                sb.Append(morseCodes[c]);
                isFirst = false;
            }
            return sb.ToString();
        }

        private Dictionary<char, string> morseCodes = new Dictionary<char, string>()
            {
                {'a', ".-"},
                {'b', "-..."},
                {'c', "-.-."},
                {'d', "-.."},
                {'e', "."},
                {'f', "..-."},
                {'g', "--."},
                {'h', "...."},
                {'i', ".."},
                {'j', ".---"},
                {'k', "-.-"},
                {'l', ".-.."},
                {'m', "--"},
                {'n', "-."},
                {'o', "---"},
                {'p', ".--."},
                {'q', "--.-"},
                {'r', ".-."},
                {'s', "..."},
                {'t', "-"},
                {'u', "..-"},
                {'v', "...-"},
                {'w', ".--"},
                {'x', "-..-"},
                {'y', "-.--"},
                {'z', "--.."},
                {'0', "-----"},
                {'1', ".----"},
                {'2', "..---"},
                {'3', "...--"},
                {'4', "....-"},
                {'5', "....."},
                {'6', "-...."},
                {'7', "--..."},
                {'8', "---.."},
                {'9', "----."}
            };
    }
}
