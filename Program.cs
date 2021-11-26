int[] Mixing() // Перетасовка колоды карт, метод возвращает массив с 52-мя значениями карт,
{              // размещенных на случайных позициях 
    int[] deck = new int[52];
    for (int i = 0; i < deck.Length; i++)
    {
        deck[i] = new Random().Next(1, 14);
        if (CheckNum(deck[i], deck)) i -= 1;
    }

    bool CheckNum(int arg, int[] deck)
    {
        int count = 0;
        for (int i = 0; i < deck.Length; i++) if (arg == deck[i]) count += 1;
        return count > 4;
    }
    return deck;
}


(int[], int[]) setUp(int[] deck)            // набор карт игроком и крупье
{                                           // метод возвращает два массива со значениями карт
    int[] player = new int[deck.Length];    // массив игрока
    int[] croupier = new int[deck.Length];  // массив крупье
    int flag = 0;                           // переменная flag хранит позицию в массиве колоды
                                            // на которой закончил брать карты игрок
    player[0] = deck[deck.Length - 1];      // на первую (нулевую) позицию массива игрока записываем значение карты, которая находится в колоде на последней позиции
    deck[deck.Length - 1] = 0;              // последнюю позицию массива колоды обнуляем

    Console.WriteLine($"Ваша первая карта: {WhatIsCard(player[0])}");
    if ((!Overload(player)) && player[0] == 1) player[0] = 111;

    for (int i = 1; i < deck.Length - 1; i++)
    {
        Console.WriteLine("Ещё? (Да - 'Y', Нет - любая клавиша): ");
        if (WaitUser())
        {
            player[i] = deck[deck.Length - 1 - i];
            deck[deck.Length - 1 - i] = 0;
            Console.WriteLine($"Ваша {i + 1}-я карта: {WhatIsCard(player[i])}");

            if ((!Overload(player)) && player[i] == 1) player[i] = 111;
            if (Overload(player))
            {
                Console.WriteLine("У Вас перебор!");
                flag = i+1;
                i = deck.Length - 1;
            }

        }
        else
        {
            flag = i;
            i = deck.Length - 1;
        }
    }

    croupier[0] = deck[deck.Length - flag-1];   // на первую позицию массива крупье (за минусом нулевых позиций, которые забрал игрок)
                                                // записываем значение карты, которая находится в колоде на последней позиции
    deck[deck.Length - flag-1] = 0;             // последнюю позицию массива колоды ( за минусом нулевых) обнуляем

    Thread.Sleep(1500);                         // задержка                         
    Console.Clear();

    Console.WriteLine($"Первый ход крупье. Выпала карта: {WhatIsCard(croupier[0])}");
    if (croupier[0] == 1) croupier[0] = 111;    //если выпал туз, записываем в массив 111

    for (int j = 1; j < deck.Length - 1; j++)
    {
        if (OverloadCroupier(croupier) < 17)
        {
            Thread.Sleep(1500);
            Console.WriteLine($"{j + 1}-й xод крупье.");
            croupier[j] = deck[deck.Length - (flag + 1) - j];
            deck[deck.Length - (flag + 1) - j] = 0;
            Console.WriteLine($"Выпала карта: {WhatIsCard(croupier[j])}");

            if (croupier[j] == 1) croupier[j] = 111;
            if (OverloadCroupier(croupier) > 21)
            {
                Console.WriteLine("У крупье перебор!");
                j = deck.Length - 1;
            }

        }
        else
        {
            j = deck.Length - 1;
        }
        Thread.Sleep(1500);
    }

    bool Overload(int[] collection)      // проверка "перебора" у игрока
    {
        int sum = 0;
        for (int i = 0; i < collection.Length; i++)
        {
            if (collection[i] < 11) sum += collection[i];
            else if (collection[i] == 111) sum += collection[i] - 100;
            else sum += 10;
        }
        return sum > 21;
    }

    int OverloadCroupier(int[] collection)      // проверка "перебора" у крупье
    {
        int sum = 0;
        for (int i = 0; i < collection.Length; i++)
        {
            if (collection[i] < 11) sum += collection[i];
            else if (collection[i] == 111) sum += collection[i] - 100;
            else sum += 10;
        }
        return sum;
    }

    bool WaitUser()                             //метод (процедура) ожидание ответа пользователя
    {
        while (true)
        {
            string answer = Console.ReadLine();
            return answer.ToLower() == "y";     //если нажата "y", то возвращает значение true
        }
    }

    string WhatIsCard(int arg)
    {
        string ValueCard = string.Empty;
        if (arg == 1) ValueCard = "Туз";
        if (arg == 2) ValueCard = "Двойка";
        if (arg == 3) ValueCard = "Тройка";
        if (arg == 4) ValueCard = "Четверка";
        if (arg == 5) ValueCard = "Пятерка";
        if (arg == 6) ValueCard = "Шестерка";
        if (arg == 7) ValueCard = "Семерка";
        if (arg == 8) ValueCard = "Восьмерка";
        if (arg == 9) ValueCard = "Девятка";
        if (arg == 10) ValueCard = "Десятка";
        if (arg == 11) ValueCard = "Валет";
        if (arg == 12) ValueCard = "Дама";
        if (arg == 13) ValueCard = "Король";
        return ValueCard;
    }

    return (player, croupier);
}

Console.Clear();
var score = setUp(Mixing());
int playerScore = 0;
int croupierScore = 0;

for (int i = 0; i < score.Item1.Length; i++)
{
    if (score.Item1[i] < 11) playerScore += score.Item1[i];
    else if (score.Item1[i] == 111) playerScore += score.Item1[i] - 100;
    else playerScore += 10;
    if (score.Item2[i] < 11) croupierScore += score.Item2[i];
    else if (score.Item2[i] == 111) croupierScore += score.Item2[i]-100;
    else croupierScore += 10;
}
Console.Clear();
Console.WriteLine($"Счет игрока: {playerScore}, счет крупье: {croupierScore} ");
