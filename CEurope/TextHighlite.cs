using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CEurope
{
    static class TextHighlite
    {
        static public readonly TextStyle CommentsStyle = new TextStyle(Brushes.MediumAquamarine, null, FontStyle.Italic);
        static public readonly TextStyle KeyWordStyle = new TextStyle(Brushes.DodgerBlue, null, FontStyle.Bold);
        static public readonly TextStyle BigOperatorsStyle = new TextStyle(Brushes.ForestGreen, null, FontStyle.Bold);
        static public readonly TextStyle LineOperatorsStyle = new TextStyle(Brushes.DarkOrchid, null, FontStyle.Bold);
        static public readonly TextStyle VarStyle = new TextStyle(Brushes.Crimson, null, FontStyle.Bold);
        static public readonly TextStyle StringAndSymbolStyle = new TextStyle(Brushes.Peru, null, FontStyle.Italic);

        static public void SetFoldingMarkers(Range changed_range)
        {
            changed_range.ClearFoldingMarkers();

            changed_range.SetFoldingMarkers("почати", "кінець");
        }

        static public void SetStyles(Range range, Range changed_range)
        {
            SetFoldingMarkers(changed_range);

            //Comments
            range.ClearStyle(CommentsStyle);
            //Inline comment
            changed_range.SetStyle(CommentsStyle, @"//.*");
            //Block comment
            //range.SetStyle(CommentsStyle, @"(\{.*?\})|(\{.*)", RegexOptions.Singleline);
            //range.SetStyle(CommentsStyle, @"(\{.*?\})|(.*\})", RegexOptions.Singleline |
            //            RegexOptions.RightToLeft);

            changed_range.ClearStyle(KeyWordStyle);
            //Key words
            //Allow ; after and before operator
            changed_range.SetStyle(KeyWordStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(почати|кінець|віддати)(?=((\s|\t|\n|\r|\v|;)|($)))");
            //Not allow ;
            changed_range.SetStyle(KeyWordStyle, @"(?<=((^)|(\s|\t|\n|\r|\v)))(програма|простірімен|новий)(?=((\s|\t|\n|\r|\v)|($)))");

            //Block operators
            changed_range.ClearStyle(BigOperatorsStyle);
            //Allow ; after and before operator
            changed_range.SetStyle(BigOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(встановити|отримати|інакше)(?=((\s|\t|\n|\r|\v|;)|($)))");
            //Allow ( after operator and ; before
            changed_range.SetStyle(BigOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(якщо|поки|для|перемикач|нераніше)(?=((\s|\t|\n|\r|\v|\()|($)))");
            //Not allow ; after
            //Allow ; before
            changed_range.SetStyle(BigOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(робити)(?=((\s|\t|\n|\r|\v)|($)))");

            //Line operators
            changed_range.ClearStyle(LineOperatorsStyle);
            //Allow ; after and before operator
            changed_range.SetStyle(LineOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(перерву|продовжуй)(?=((\s|\t|\n|\r|\v|;)|($)))");
            //Allow ; before
            changed_range.SetStyle(LineOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(використовуючи|відбирати|зазамовчуванням|громадськість|закритий|захищений)(?=((\s|\t|\n|\r|\v)|($)))");
            //Allow : after
            changed_range.SetStyle(LineOperatorsStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(зазамовчуванням)(?=((\s|\t|\n|\r|\v|\:)|($)))");

            //Variables
            changed_range.ClearStyle(VarStyle);
            //Allow [ after and ; before operator
            changed_range.SetStyle(VarStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(ціле|довгоціле|плавати" +
                         @"|подвійний|рядок|знак|логічний|таблиця)(?=((\s|\t|\n|\r|\v|\[)|($)))");
            //Allow ; before operator
            changed_range.SetStyle(VarStyle, @"(?<=((^)|(\s|\t|\n|\r|\v|;)))(змінна)(?=((\s|\t|\n|\r|\v)|($)))");

            changed_range.ClearStyle(StringAndSymbolStyle);
            range.SetStyle(StringAndSymbolStyle, "\".*?\"", RegexOptions.Singleline);

            range.SetStyle(StringAndSymbolStyle, @"\'.*?\'", RegexOptions.Singleline);
        }
    }
}
