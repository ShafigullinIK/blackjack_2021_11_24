int[] CreateDeck()
{
    int[] array = new int[52];
    int k = 0;
    for(int i = 2; i < 15; i++)  
    {
        for(int j = 0; j < 4; j++) array[k++] = i;
    }
    return array;
}

int[] Mixing(int[] array)
{
    int j; int temporary;
    for(int i = 0; i < array.Length; i++)
    {
        temporary = array[i];
        j = new Random().Next(i, array.Length);
        array[i] = array[j]; 
        array[j] = temporary;
    }
    return array;
}

bool DoYouWantMoreCard()
{
    Console.WriteLine("Еще карту? y/n:");
    string answer = Console.ReadLine();
    if(answer == "y") return true;
    if(answer == "n") return false;
    else 
    {
        Console.WriteLine("Промазал(а), повтори ввод");
        return DoYouWantMoreCard();
    }
}

(int[], int[], int) FirstStep(int[] mixedCards, string[] cards)  
{
    int countCard = 0;
    int playerIndex = 0;
    int[] playerArray = new int[9];
    int[] dillerArray = new int[9];
    dillerArray[countCard] = mixedCards[countCard];
    
    string cardNumber = ConvertToNameCard(mixedCards, countCard, cards);
    Console.Write($"У диллера {cardNumber} - {CalculatePoints(dillerArray)} очков, "); 
    
    countCard++; 

    playerArray[playerIndex] = mixedCards[countCard]; playerIndex++;
    cardNumber = ConvertToNameCard(mixedCards, countCard, cards);
    Console.Write($"у игрока {cardNumber} и "); countCard++;
    playerArray[playerIndex] = mixedCards[countCard]; playerIndex++;
    cardNumber = ConvertToNameCard(mixedCards, countCard, cards);
    Console.Write($"{cardNumber}"); countCard++;
    Console.WriteLine($" - {CalculatePoints(playerArray)} очков");
    return (dillerArray, playerArray, countCard);
}

(int, int) SecondStep(int[] mixedCards, int[] playerArray, string[] cards, int countCard)
{
    int playerIndex = 2;
    int playerPoints = 0;
    while(DoYouWantMoreCard())
    { 
        playerArray[playerIndex] = mixedCards[countCard]; playerIndex++; countCard++;
        string cardNumber = ConvertToNameCard(mixedCards, countCard - 1, cards);
        Console.Write($"это {cardNumber}. У игрока {CalculatePoints(playerArray)} очков.");
        playerPoints = CalculatePoints(playerArray);
        if(playerPoints > 21) 
        {
            Console.WriteLine("Перебор!");
            break;
        }
    }
    return (countCard, playerPoints);

}

int FinalStep(int[] mixedCards, string[] cards, int countCard, int[] dillerArray, int playerPoints)
{
    int dillerIndex = 1;
    while(CalculatePoints(dillerArray) < 17)
    {
        if(playerPoints > 21) break; 
        string cardNumber = ConvertToNameCard(mixedCards, countCard, cards);
        dillerArray[dillerIndex] = mixedCards[countCard];
        Console.Write($" {cardNumber}, у диллера {CalculatePoints(dillerArray)} очков.");
        countCard++; dillerIndex++;
    }
    return CalculatePoints(dillerArray);
}

void SaysWhoWon(int dillerPoints, int playerPoints)
{
    if(playerPoints < 22 && (dillerPoints < playerPoints || dillerPoints > 21)) Console.WriteLine("Игрок победил!");
    else Console.WriteLine("Победило казино, фишки диллеру!");  
}

string ConvertToNameCard(int[] mixedCards, int n, string[] cards)  //метод выводящий именование карт на экран.
{
    string cardName = String.Empty;
    int m = 0;
    int j = 2; 
    while(j < cards.Length + 2)
    {
        if(mixedCards[n] == j) cardName = cards[m];
        j++;
        m++;
    }
    return cardName;
}

int CalculatePoints(int[] array)
{
    int total = 0;
    for(int i = 0; i < array.Length; i++) 
    {
        if(array[i] > 11) total = total + 10; 
        else total += array[i];
        for(int j = 0; j < array.Length; j++)
        {
            if(total > 21 && array[j] == 11) total -= 10;  
        }
    }
    return total;
}


string[] cards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "J", "Q", "K"};
int[] deckOfCards = CreateDeck();   
int[] mixedCards = Mixing(deckOfCards);
Console.WriteLine(String.Join(',', mixedCards));
(int[] dillerArray, int[] playerArray, int countCard)  = FirstStep(mixedCards, cards);
(countCard, int playerPoints) = SecondStep(mixedCards, playerArray, cards, countCard);
int dillerPoints = FinalStep(mixedCards, cards, countCard, dillerArray, playerPoints);
SaysWhoWon(dillerPoints, playerPoints);
