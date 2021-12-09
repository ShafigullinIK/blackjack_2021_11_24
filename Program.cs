int[] deckOfCards(int countCard)       // Метод создания колоды
{
    int[] array = new int[countCard];
    int countArray = 0;
    for (int i = 2; i < 15; i++)
        for (int j = 0; j < 4; j++)
            array[countArray++] = i;
    return array;
}

int[] Mixing(int[] array)        // Метод тасует колоду
{
    int j, temporary;
    Random random = new Random();
    for (int i = 0; i < array.Length; i++)
    {
        temporary = array[i];
        j = random.Next(i, array.Length);
        array[i] = array[j];
        array[j] = temporary;
    }
    return array;
}

string NameCard(int valueCard)      // Метод определяет название карты
{
    string[] nameCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Туз", "Валет", "Дама", "Король" };
    return nameCards[valueCard - 2];
}

bool Answer(string? str) => str == "y";        // Метод возвращает TRUE если игрок согласен

void Print(string word) { Console.WriteLine(word); }       // Метод печати на консоль

(int sum, int count) Turn(int Player, int[] cards, int iCard)        // Метод подсчета очков (возвращает кортеж (сумма очков, индекс карты))
{
    var result = (sum: 0, count: iCard);
    int sumTus = 0, Tus = 0, officer = 0;
    int sumICard = iCard;
    if (cards[iCard] > 11) officer = cards[iCard] - 10;
    if (cards[iCard + 1] > 11) officer += cards[iCard + 1] - 10;
    result.sum = cards[iCard] + cards[iCard + 1] - officer;
    officer = 0;
    if (cards[iCard] == 11) sumTus += 10;
    if (cards[iCard + 1] == 11) sumTus += 10;
    if (result.sum > 21) result.sum -= sumTus;

    Print($"Игрок {Player}, тебе выпали карты {NameCard(cards[iCard])} и {NameCard(cards[iCard + 1])}.\nИх сумма {result.sum}. Желаешь взять ещё карту? y/n");
    iCard += 2; // 3я карта в массиве от старта игрока
    while (Answer(Console.ReadLine()))
    {
        if (cards[iCard] > 11) officer = cards[iCard] - 10;
        result.sum += cards[iCard] - officer;
        officer = 0;
        if (cards[iCard] == 11) Tus += 10;
        if (result.sum > 21 && sumTus == 10) { result.sum -= sumTus; sumTus = 0; }
        if (result.sum > 21) { result.sum -= Tus; Tus = 0; }
        Print($"Вот тебе еще карта: {NameCard(cards[iCard])}. \nСумма карт {result.sum}. Ещё? y/n");
        iCard++;
    }
    result.count = iCard;
    Print($"Sum cards Player {Player}: {result.sum}"); //// строка для проверки
    return result;
}

void Game(int PlayerNum, int[] mixedCards, int iCard)       // Метод игры для 2-уx игроков 
{
    (int sum, int count) countCard = (0, 0);
    int[] arraySum = new int[2];
    int i = 0;
    while (PlayerNum < 3)
    {
        countCard = Turn(PlayerNum, mixedCards, iCard);
        iCard = countCard.count;
        Print("Использовано карт в игре: " + iCard.ToString() + "\n");
        arraySum[i++] = countCard.sum;
        PlayerNum++;
    }
    Winer(arraySum[0], arraySum[1]);
}

void Winer(int sumFirst, int sumSecond)     // Метод определяет победителя
{
    if (sumFirst > 21 && sumSecond > 21) Print("Вы оба проиграли!");
    else if (sumFirst == sumSecond) Print("Ничья!");
    else if (sumFirst > 21 && sumSecond <= 21) Print("Выйграл Player 2!");
    else if (sumFirst <= 21 && sumSecond > 21) Print("Выйграл Player 1!");
    else
    {
        if (sumFirst > sumSecond) Print("Выйграл Player 1!");
        else Print("Выйграл Player 2!");
    }
}



int[] cardOfPlay = Mixing(deckOfCards(52));     // Перемешивание колоды
Console.WriteLine(String.Join(',', cardOfPlay));        // Вывод колоды на экран
byte PlayerNum = 1;     // Номер игрока
int iCard = 0; // Индекс карты из массива колоды
do
{
    Console.Clear();
    Game(PlayerNum, cardOfPlay, iCard);     // Запуск игры     
    Print("Может ещё разок? y/n");
    cardOfPlay = Mixing(deckOfCards(52));     // Перемешивание колоды
    // Console.WriteLine(String.Join(',', cardOfPlay));        // Вывод колоды на экран
} while (Answer(Console.ReadLine()));