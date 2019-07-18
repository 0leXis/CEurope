using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static readonly Dictionary<string, string> ErrorCodes = new Dictionary<string, string>()
        {
            ["BAD_NAMESPACE"] = "Неприпустиме оголошення простору імен",
            ["BAD_LITERALNAME"] = "Введено неприпустиме ім'я",
            ["NO_NAMESPACE"] = "Простір імен відсутня",
            ["NO_PROGRAM"] = "Відсутня точка входу",
            ["DEFINED_PROGRAM"] = "Точка входу вже визначена в даному або іншому просторі імен",
            ["BAD_LITERAL"] = "Виявлено несподіваний оператор",
            ["BADEND_BLOCK"] = "Виявлено кінець блоку, але не виявлено початок",
            ["BAD_BLOCK"] = "Виявлено неприпустимі блок операторів",
            ["BAD_TYPE"] = "Неприпустимий тип змінної",
            ["BAD_EXPRESSION"] = "Неприпустиме вираз",
            ["BAD_OPERATOR"] = "Неприпустимий оператор",
            ["BAD_CAST"] = "Неприпустиме приведення типів в вираженні",
            ["BAD_VARIABLE"] = "Неприпустиме значення змінної",
            ["BADNAME_VARIABLE"] = "Змінної з таким ім'ям не існує",
            ["BADINIT_VARIABLE"] = "Змінна, зазначена ключовим словом \"змінна\" не була инициализирована",
            ["NOTFOUND_IF"] = "Виявлено \"інакше\" але не виявлено \"якщо\"",
            ["BAD_CONTINUE"] = "Виявлено \"продовжуй\" але не виявлений блок циклу",
            ["BAD_BREAK"] = "Виявлено \"перерву\" але не виявлений блок для виходу"
        };

        static readonly string[] KeyWords = new string[]
        {
            "використовуючи", "вживання", "почати", "кінець", "програма",
            "простірімен", "встановити", "отримати", "новий", "віддати",
            "змінна", "ціле", "довгоціле", "плавати", "подвійний", "рядок",
            "знак", "логічний", "таблиця", "якщо", "інакше", "поки", "нераніше", "робити", "для",
            "перерву", "продовжуй", "перемикач", "відбирати", "зазамовчуванням",
            "громадськість", "закритий", "захищений",
            "True", "False", "true", "false", "null",
            "ЧитатиРядок", "ПисатиРядок" //TODO: В отдельные функции
        };

        static readonly string[] DataDefs = new string[]
        {
            "змінна",
            "ціле",
            "довгоціле",
            "плавати",
            "подвійний",
            "рядок",
            "знак",
            "логічний",
            "таблиця"
        };

        static readonly char[] IgnoreCharacters = new char[] { ' ', '\t', '\n', '\r', '\v' };

        static readonly string[] operators = new string[]
        {
            "++", "--", "-", "+", "!", "*", "/", "%", "<<", ">>", ">", "<", ">=",
            "<=", "==", "!=", "&", "^", "|", "&&", "||"
        };

        static readonly char[] operator_tokens = new char[]
        {
            '+', '-', '!', '*', '/', '%', '<', '>', '=', '&', '^', '|'
        };

        private const int MIN_PRIORITY = 100;

        static readonly Dictionary<OperatorType, int> OperatorPriority = new Dictionary<OperatorType, int>()
        {
            [OperatorType.LeftBracket] = MIN_PRIORITY,
            [OperatorType.RightBracket] = MIN_PRIORITY,
            [OperatorType.None] = MIN_PRIORITY,

            //[OperatorType.Increment] = 0,
            //[OperatorType.Decrement] = 0,
            [OperatorType.UnaryMinus] = 1,
            [OperatorType.UnaryPlus] = 1,
            [OperatorType.Not] = 1,
            [OperatorType.Multiply] = 2,
            [OperatorType.Divide] = 2,
            [OperatorType.ModuleDivide] = 2,
            [OperatorType.Substract] = 3,
            [OperatorType.Add] = 3,
            [OperatorType.LeftShift] = 4,
            [OperatorType.RightShift] = 4,
            [OperatorType.More] = 5,
            [OperatorType.Less] = 5,
            [OperatorType.MoreOrEqual] = 5,
            [OperatorType.LessOrEqual] = 5,
            [OperatorType.Equal] = 6,
            [OperatorType.NotEqual] = 6,
            [OperatorType.And] = 7,
            [OperatorType.Xor] = 8,
            [OperatorType.Or] = 9
        };
    }
}