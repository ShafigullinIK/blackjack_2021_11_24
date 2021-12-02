//  Специальный коммит
int[] Mixing(int numCards, int numDecks)
{
    int j, temp, fromValueCard; int count = 0; int[] Deck = new int[numCards * numDecks];

    if (numCards == 52) fromValueCard = 2; else fromValueCard = 6;

    for (int k = 0; k < 4 * numDecks; k++)
    { for (int n = fromValueCard; n < 15; n++) { Deck[count] = n; count += 1; } }

    for (int i = 0; i < Deck.Length; i++)
    { temp = Deck[i]; j = new Random().Next(i, Deck.Length); Deck[i] = Deck[j]; Deck[j] = temp; }

    return Deck;
}

int RequestNumber(string words) // ввод чисел с проверкой
{
    while (true)
    {
        Console.Write(words);
        if (int.TryParse(Console.ReadLine(), out int num) && num > 0) return num;
        else Console.WriteLine("Что-то вы не то ввели, давайте-ка снова.");
    }
}

string[] AskNames()
{
    while (true)
    {
        Console.Write("Введите имена игроков через запятую: ");
        string names = Console.ReadLine() + ",Крупье";
        string[] playersNames = names.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Прошлый код давал ошибку вводя строку "Маша, Паша, Саша", получали массив {"Маша","","Паша","","Cаша"}. Теперь можно вводить как "Маша Паша Саша" так и "Маша, Паша,Саша" и т.п.
        if (playersNames.Length > 8) Console.WriteLine("За нашим столиком всего 7 кресел, может кто-то подождёт остальных в баре?");
        else return playersNames;
    }
}

(string[] playersNames, int numDecks, int[] balance) Greetings() // Здесь нужно проверить достаточность колод в зависимости от числа игроков
{
    string[] playersNames = AskNames();
    int[] balance = new int[playersNames.Length - 1];
    int numDecks = RequestNumber("Укажите начальный баланс игроков: ");
    for (int i = 0; i < balance.Length; i++)
    {
        balance[i] = numDecks;
    }
    numDecks = RequestNumber("Сколько колод возьмём: "); // Возможно нужно устанавливать количество колод автоматически, в зависимости от числа игроков
    return (playersNames, numDecks, balance);
}

int[] MakeBets(string[] playersNames, int[] balance) //опрос всех игроков о их ставке, количество игроков и их балансов должны быть массивы одинакового размера
{
    int playersCount = playersNames.Length - 1;
    int[] betsArray = new int[playersCount];
    for (int i = 0; i < playersCount; i++)
    {
        betsArray[i] = AskForBet(playersNames[i], balance[i]);
    }
    return betsArray;
}

int AskForBet(string playerName, int playerBalance) //метод опроса отдельного игрока, переспрашивает пока ставка не будет больше 0 и меньше баланса.
{
    while (true)
    {
        int betAmount = RequestNumber($"{playerName} у вас {playerBalance} фишек, делайте вашу ставку: ");
        if (betAmount <= playerBalance) return betAmount;
        else Console.WriteLine($"Ставка не может быть меньше 1 или больше количества ваших фишек.");
    }
}

(int[,], int) SetUp(string[] playersNames, int[] Deck) // создает двумерный массив, в котором каждая строка - массив карт игроков, столбец - значение карты
{
    int playersCount = playersNames.Length, nextCard = Deck.Length - 1;
    int[,] playersDecks = new int[playersCount, 11]; // массивы колод игроков и крупье, максимальный размер - 11, 

    for (int i = 0; i < playersCount; i++) // первые две карты игроков
    {
        for (int j = 0; j < 2; j++)
        {
            playersDecks[i, j] = Deck[nextCard--];
        }
    }
    return (playersDecks, nextCard);
}

int[] Round(int[] deck, int[,] playersDecks, string[] playersNames, int nextCard)
{
    int[] playersCardsScores = new int[playersDecks.GetLength(0)];
    int[] cardsArray = new int[playersDecks.GetLength(1)];
    Dictionary<int, string> CardNames = new Dictionary<int, string>
    {
        [2] = "Двойка",
        [3] = "Тройка",
        [4] = "Четверка",
        [5] = "Пятерка",
        [6] = "Шестерка",
        [7] = "Семерка",
        [8] = "Восьмерка",
        [9] = "Девятка",
        [10] = "Десятка",
        [11] = "Туз",
        [12] = "Валет",
        [13] = "Дама",
        [14] = "Король",
    };
    Console.Clear();
    Console.WriteLine("Р А С К Л А Д:");
    for (int j = 0; j < playersDecks.GetLength(0); j++)
    {
        if (j == playersDecks.GetLength(0) - 1)
        {
            Console.Write($"{playersNames[j]}: ");
            cardsArray[j] = playersDecks[j, 0];
            Console.Write($"{CardNames[cardsArray[0]]} ");
        }
        else
        {
            Console.Write($"{playersNames[j]}: ");
            for (int i = 0; i < 2; i++)
            {
                cardsArray[i] = playersDecks[j, i];
                Console.Write($"{CardNames[cardsArray[i]]} ");
            }
        }
        Console.WriteLine();
    }
    Thread.Sleep(5000);                         // задержка                         

    for (int i = 0; i < playersDecks.GetLength(0); i++)
    {
        (int playerCardsScore, nextCard) = GamePlayer(i, playersNames, playersDecks, deck, nextCard);
        playersCardsScores[i] = playerCardsScore;
        Console.Clear();
        if (i < playersDecks.GetLength(0) - 1) Console.WriteLine("ПЕРЕХОД ХОДА");
        Thread.Sleep(1000);
    }
    return (playersCardsScores);
}

(int, int) GamePlayer(int playerIndex, string[] playersNames, int[,] playersDecks, int[] Deck, int nextCard)
{
    int[] cardsArray = new int[playersDecks.GetLength(1)];
    Dictionary<int, string> CardNames = new Dictionary<int, string>
    {
        [2] = "Двойка",
        [3] = "Тройка",
        [4] = "Четверка",
        [5] = "Пятерка",
        [6] = "Шестерка",
        [7] = "Семерка",
        [8] = "Восьмерка",
        [9] = "Девятка",
        [10] = "Десятка",
        [11] = "Туз",
        [12] = "Валет",
        [13] = "Дама",
        [14] = "Король",
    };
    Console.Clear();
    Console.Write($"У игрока {playersNames[playerIndex]} выпали карты: ");

    for (int i = 0; i < 2; i++)
    {
        cardsArray[i] = playersDecks[playerIndex, i];
        Console.Write($"{CardNames[cardsArray[i]]} ");
    }
    int playerCardsScore = CardsScore(cardsArray);
    Console.WriteLine(); Console.WriteLine($"Сумма очков: {CardsScore(cardsArray)} ");

    if (CardsScore(cardsArray) > 21) return (playerCardsScore, nextCard);

    for (int j = 2; j < playersDecks.GetLength(1); j++)
    {
        if (playerIndex == playersNames.Length - 1)
        {
            cardsArray[j] = Deck[nextCard];
            playersDecks[playerIndex, j] = Deck[nextCard--];
            Console.Write($"Выпала карта: {CardNames[cardsArray[j]]} ");
            Console.WriteLine($"Сумма очков: {CardsScore(cardsArray)} ");
            playerCardsScore = CardsScore(cardsArray);
            Thread.Sleep(2500);
            if (CardsScore(cardsArray) > 17)
            {
                return (playerCardsScore, nextCard);
            }
        }
        else
        {
            if (UserAnswer())
            {
                cardsArray[j] = Deck[nextCard];
                playersDecks[playerIndex, j] = Deck[nextCard--];
                Console.Write($"Выпала карта: {CardNames[cardsArray[j]]} ");
                Console.WriteLine($"Сумма очков: {CardsScore(cardsArray)} ");
                playerCardsScore = CardsScore(cardsArray);
                Thread.Sleep(2500);
                if (CardsScore(cardsArray) > 21)
                {
                    return (playerCardsScore, nextCard);
                }
            }
            else { j = playersDecks.GetLength(1); return (playerCardsScore, nextCard); }
        }
    }

    return (playerCardsScore, nextCard);
}

bool UserAnswer()                             //метод (процедура) ожидание ответа пользователя
{
    Console.WriteLine("Берем карту?");
    while (true)
    {
        string answer = Console.ReadLine();
        return answer.ToLower() == "y";     //если нажата "y", то возвращает значение true
    }
}

// Метод подсчёта наибольшей суммыочков с заданных карт, на входе массив карт заданных как числа (2-14)
// Для определения блэкджека от суммы 21, результат при блэкджеке =99 (недостижимый простым подсчётом карт)
int CardsScore(int[] cardsArray)
{
    int len = cardsArray.Length;
    int aceCount = 0;
    int totalScore = 0;
    for (int i = 0; i < len; i++)
    {
        switch (cardsArray[i])
        {
            case < 11:                               //для карт 2-10 по номиналу
                totalScore += cardsArray[i];
                break;
            case 11:
                totalScore += cardsArray[i];
                aceCount++;
                break;

            case > 11:                               //все карты с картинками как 10
                totalScore += 10;
                break;
        }
    }
    if (totalScore == 21 && len == 2) { return 99; }  //указатель для отличия БлэкДжека от просто суммы 21
    while (totalScore > 21 && aceCount > 0)         //если по итогам получили перебор за каждого туза вычитам 10 (начинаем считать его как 1), пока не закончатся тузы или не окажемся ниже 21
    {
        totalScore -= 10;
        aceCount--;
    }
    return totalScore;
}

//метод возвращает изменения баланса игрока, по очкам их карт и величине ставки
//переборы игрока сюда не попадают (их отлавливаем в процессе игры и сразу вызываем balance[i] += BalanceChangeValue(-1,bets[i]));
int CompareCardsResult(int playerScoreValue, int dealerScoreValue) //-1 проигра, 0 - ничья, 1 выиграл, 2 выиграл по блэкджеку
{
    //условие ничьей
    if (dealerScoreValue == playerScoreValue) return 0; //сумма карт поровну (при этом никто не перебрал)
    //условия победы
    if (playerScoreValue == 99) { return 2; } //победа по блэкджеку (у же не ничья т.е. у крупье не блэкджек)
    if ((dealerScoreValue > 21 && dealerScoreValue != 99) || playerScoreValue > dealerScoreValue) return 1; //простая победа, у дилера перебор или у игрока сумма выше
    //все остальные варианты проигрыш
    return -1; //у крупье больше чем у игрока (нету переборов и блэджеков и т.п.)
}

//метод изменения баланса игрока
//при переборе в процессе добора вызываем BalanceChange(-1,betValue), при этом обнуляем положение ставки
//для всех не выбывших игроков у которых в Bets != 0, производим BalanceChange(CompareCardsResult(playerScore,dealerScore),betValue);
int BalanceChangeValue(int WinLossValue, int betValue)
{
    switch (WinLossValue)
    {
        case -1:
            return -betValue; //результат проигрыш
        case 0:
            return 0;         //результат ничья
        case 1:
            return betValue;  //результат выигрышь 1 к 1  
        case 2:
            return betValue * 3 / 2;    //результат выигрышь 3 к 2 (по Блэкджеку), копейки остаются у казино
        default:
            return 0; //результат которого не должно быть!
    }
}

void InitGame()
{
    (string[] playersNames, int numDecks, int[] balance) = Greetings(); //передаём результат кортежа в переменные
    int[] bets = MakeBets(playersNames, balance); //заполняем массив принятых ставок
    RunGame(numDecks, playersNames); //запускаем игру
}

//Код игры
void RunGame(int numDecks, string[] playersNames)
{
    int[] deck = Mixing(52, numDecks);
    (int[,] playersDecks, int nextCard) = SetUp(playersNames, deck);
    Console.WriteLine(String.Join(',', (Round(deck, playersDecks, playersNames, nextCard))));
}

InitGame();
