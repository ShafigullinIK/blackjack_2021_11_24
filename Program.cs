void PrintArray(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        System.Console.Write(array[i] + " ");
    }
    System.Console.WriteLine();
}


int[] GetPackOfCards(int number_of_packs, int[] init_pack)
{
    int total_cards = init_pack.Length * number_of_packs;
    int[] pack_of_cards = new int[total_cards];
    int index = 0;
    while (index < total_cards)
    {
        for (int card = 0; card < init_pack.Length; card++)
        {
            pack_of_cards[index++] = init_pack[card];
        }
    }
    return pack_of_cards;
}

int[] ShuffleCards(int[] pack_of_cards, int shuffle_quality)
{
    Random rand = new Random();
    int first, second, temp;
    for (int i = 0; i < shuffle_quality; i++)
    {
        first = rand.Next(0, pack_of_cards.Length);
        second = rand.Next(0, pack_of_cards.Length);
        temp = pack_of_cards[first];
        pack_of_cards[first] = pack_of_cards[second];
        pack_of_cards[second] = temp;
    }
    return pack_of_cards;
}

int TakeCardFromPack(int[] pack_of_cards)
{
    int card = 0;
    for (int i = 0; i < pack_of_cards.Length; i++)
    {
        if (pack_of_cards[i] != 0)
        {
            card = pack_of_cards[i];
            pack_of_cards[i] = 0;
            return card;
        }
    }
    return card;
}

void ShowCard(int card, int points)
{
    System.Console.WriteLine($"Ваша карта {card}, всего очков {points}.");
}

bool MoreCards()
{
    string answer = string.Empty;
    System.Console.WriteLine("Чтобы взять еще одну карту введите 'да'.");
    answer = Console.ReadLine();
    if (answer.ToLower() == "да")
        return true;
    else
        return false;
}

int UserTakesCards(int[] pack_of_cards)
{
    int points = 0;
    int card = 0;
    System.Console.WriteLine("Ход игрока");
    do
    {
        card = TakeCardFromPack(pack_of_cards);
        if ((card == 11) && (points > 10)) card = 1; //Это туз дает 1 очко
        points += card;
        ShowCard(card, points);
        if (points > 21) return 0;
        if (points == 21) return points;
    }
    while (MoreCards());
    return points;
}

int CasinoTakesCards(int[] pack_of_cards, int players_result)
{
    int points = 0;
    int card = 0;
    System.Console.Write("Ход казино");
    Console.ReadLine();
    do
    {
        card = TakeCardFromPack(pack_of_cards);
        points += card;
        ShowCard(card, points);
        if (points > 21) return 0;
    }
    while (points < players_result);
    return points;
}

void Round(int[] pack_of_cards)
{
    int casino_result = 1, players_result = 0;
    players_result = UserTakesCards(pack_of_cards); //возврат 0 при переполнении (больше 21)
    if (players_result == 0)
    {
        System.Console.WriteLine("У Вас перебор. Вы проиграли.");
        return;
    }
    casino_result = CasinoTakesCards(pack_of_cards, players_result); //возврат 0 при переполнении (больше 21)
    if (casino_result == 0)
    {
        System.Console.WriteLine("Поздравляем. У казино перебор.");
        return;
    }
    RoundIsOver(players_result, casino_result);
}

bool NextRound()
{
    string answer = string.Empty;
    System.Console.Write("Введите 'да', чтобы продолжить игру: ");
    answer = Console.ReadLine();
    if (answer.ToLower() == "да")
        return true;
    else
        return false;
}

void RoundIsOver(int players_result, int casino_result)
{
    if (players_result > casino_result)
        System.Console.WriteLine("Поздравляем, Вы победили!");
    else
        System.Console.WriteLine("Победило казино");
}



int number_of_packs = 1;
int[] pack_of_cards;
int[] init_pack = new int[13] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

do
{
    pack_of_cards = ShuffleCards(GetPackOfCards(number_of_packs, init_pack), 20);
    Round(pack_of_cards);
}
while (NextRound());
