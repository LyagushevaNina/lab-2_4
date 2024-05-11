internal static class Program
{
    [STAThread]

    // Метод для преобразования букв сообщения в цифры и наоборот 
    static string StringConvert(string a, bool choice)
    {
        //Таблица замен для русского алфавита
        string[] alphabet = ["А10","Б11","В12","Г13","Д14","Е15","Ж16","З17","И18","Й19","К20","Л21",
                              "М22","Н23","О24","П25","Р26","С27","Т28","У29","Ф30","Х31","Ц32","Ч33",
                              "Ш34","Щ35","Ъ36","Ы37","Ь38","Э39","Ю40","Я41"," 99"];

        string? b = null;
        if (choice)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (a[i] == alphabet[j][0])
                    {
                        b = b + alphabet[j][1] + alphabet[j][2];
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < a.Length / 2; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    string s1 = new(new char[] { a[i * 2], a[(i * 2) + 1] });
                    string s2 = new(new char[] { alphabet[j][1], alphabet[j][2] });
                    if (s1 == s2)
                    {
                        b += alphabet[j][0];
                    }
                }
            }
        }

        return b;
    }

    // Метод для нахождения обратного элемента по модулю 
    private static int Euclid(int a, int m)
    {
        int r;
        int k = 1;

        while (true)
        {
            k += m;
            if (k % a == 0)
            {
                r = k / a;
                return r;
            }
        }
    }
    // НОД
    static int NOD(int a, int b)
    {
        while (b != 0)
        {
            if (a > b)
            {
                a -= b;
            }
            else
            {
                b -= a;
            }
        }

        return a;
    }
    // Метод для возведение в степень и деления с остатком
    static int Exp(int a, int e, int m)
    {
        long result = a;
        for (int i = 0; i < e - 1; i++)
        {
            result = (long)(result * a);

            result = (long)(result % m);

        }

        return (int)result;
    }

    static int SafeReadInteger()
    {
        while (true)
        {
            string sValue = Console.ReadLine();
            if (int.TryParse(sValue, out int iValue))
            {
                return iValue;
            }

            Console.WriteLine("ОШИБКА: Неверный формат. Введите целое число");
        }
    }
    internal static void Main()
    {
        int p = 197;
        int q = 349;
        int fi = (p - 1) * (q - 1);

        while (true)
        {
            Console.WriteLine("0 - Зашифровать");
            Console.WriteLine("1 - Расшифровать");
            Console.WriteLine("2 - Вывести ключи");
            Console.WriteLine("3 - Выход");
            int Menu = SafeReadInteger();
            switch (Menu)
            {
                case 0:
                    Console.WriteLine("Шифруем. Введите сообщение:");
                    //Выполняется преобразование сообщения в числовое представление
                    string M = StringConvert(Console.ReadLine().ToUpper(), true);

                    Console.WriteLine("Введите открытый ключ:");
                    int keyEncrypt = SafeReadInteger();
                    string? messEncrypt = null; // Сюда записывается зашифрованное сообщение
                    string block = M[0].ToString();
                    string x;
                    for (int i = 1; i < M.Length; i++)
                    {
                        if (int.Parse(block + M[i]) < (p * q))
                        {
                            block += M[i];
                        }
                        else
                        {
                            x = Exp(int.Parse(block), keyEncrypt, p * q).ToString();
                            messEncrypt = messEncrypt + x.Length.ToString() + x;
                            block = M[i].ToString();
                        }
                    }

                    x = Exp(int.Parse(block), keyEncrypt, p * q).ToString();
                    messEncrypt = messEncrypt + x.Length.ToString() + x;
                    Console.WriteLine("Сообщение зашифровано");
                    Console.WriteLine(messEncrypt.ToString());
                    break;

                case 1:
                    Console.WriteLine("Расшифруем. Введите строку:");
                    string C = Console.ReadLine().ToString();

                    Console.WriteLine("Введите секретный ключ:");
                    int keyDecrypt = SafeReadInteger();
                    string? messDecrypt = null;
                    while (C != "")
                    {
                        messDecrypt += Exp(int.Parse(C.Substring(1, Convert.ToInt32(char.GetNumericValue(C[0])))), keyDecrypt, p * q).ToString();
                        C = C.Remove(0, Convert.ToInt32(char.GetNumericValue(C[0])) + 1);
                    }

                    Console.WriteLine("Сообщение расшифровано");
                    Console.WriteLine(StringConvert(messDecrypt, false));
                    break;

                case 2:
                    Console.WriteLine("Введите количество ключей:");
                    int keyNumber = SafeReadInteger();
                    int d = 2;
                    for (int i = 0; i < keyNumber; i++)
                    {
                        while (NOD(d, fi) != 1)
                        {
                            d++;
                        }

                        int e = Euclid(d, fi);

                        Console.WriteLine("Открытый ключ ({0}, {1}) Секретный ключ ({2}, {1})", e, p * q, d);
                        d++;
                    }

                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Ошибка. Введите порядковый номер");
                    break;
            }
        }
    }
}