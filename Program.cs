/* Задачи
1. Создаем колоду карт.
2, Для игры нам необходима перемешанная колода, поэтому надо в случайном порядке 
перетасовать карты (или сделать какой-то механизм, чтобы менять порядок карт)
*/
int numberOfShuffle = 100;
string[] cardSuit = new string[] { "треф", "пик", "буби", "червы" };
string[] cardRank = new string[]
{"туз", "двойка", "тройка", "четвёрка", "пятерка", "шестёрка", "семёрка",
 "восьмёрка", "девятка", "десятка", "валет", "дама", "король"};

string[,] FillCardDesk(string[] array1, string[] array2) // создание колоды
{
    string[,] doubleArray = new string[array1.Length, array2.Length]; // новая колода
    int indxArray1 = 0; // масть
    for (int i = 0; i < array1.Length; i++)
    {
        int indxArray2 = 0; // ранг карты
        for (int j = 0; j < array2.Length; j++)
        {
            doubleArray[i, j] = $"{array2[indxArray2]} {array1[indxArray1]}, ";
            indxArray2++;
        }
        indxArray1++;
    }
    return doubleArray;
}
void PrintImage(string[,] doubleArray) // вывод колоды на консоль
{
    for (int i = 0; i < doubleArray.GetLength(0); i++)
    {
        for (int j = 0; j < doubleArray.GetLength(1); j++)
        {
            Console.Write($"{doubleArray[i, j]}");
        }
        Console.WriteLine();
    }
}
string[,] cardDesk = FillCardDesk(cardSuit, cardRank);
PrintImage(cardDesk);
Console.WriteLine();


string[,] DeskShuffle(string[,] doubleArray, int number)
{
    string temp = String.Empty;
    string temp2 = String.Empty;
    //int taceCard = 1;
    while (number != 0)
    {
        int i = new Random().Next(0, doubleArray.GetLength(0));
        int j = new Random().Next(0, doubleArray.GetLength(1));
        temp = doubleArray[i, j];
       // Console.Write($"temp = {doubleArray[i, j]}");
        int k = new Random().Next(0, doubleArray.GetLength(0));
        int l = new Random().Next(0, doubleArray.GetLength(1));
        temp2 = doubleArray[k, l];
        //Console.Write($"temp2 = {doubleArray[k, l]}");
        if (temp != temp2)
        {
            doubleArray[i, j] = temp2;
            //Console.Write($"card1 = {doubleArray[i, j]}");
            doubleArray[k, l] = temp;
            //Console.Write($"card2 = {doubleArray[k, l]}");
            number = number - 2;
        }

    }
    return doubleArray;
}
string[,] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
PrintImage(shuffledCardDesk);
//DeskShuffle(cardDesk, numberOfCards);