/* Задачи
1. Создаем колоду карт.
2, Для игры нам необходима перемешанная колода, поэтому надо в случайном порядке 
перетасовать карты (или сделать какой-то механизм, чтобы менять порядок карт)
*/
int numberOfShuffle = 100;
string[] cardSuit = new string[] { "треф", "пик", "буби", "червы" };
string[] cardRank = new string[]
{"туз", "2", "3", "4", "5", "6", "7",
 "8", "9", "10", "валет", "дама", "король"};

string[] RefillArray(string[] array)
{
    string[] array2 = new string[array.Length * 1];
    int index = 0;
    for (int j = 0; j < 1; j++)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (index < array2.Length)
            {
                array2[index] = $"{array[i]}";
                index++;
            }
        }
    }
    return array2;
}

string[] DeskShuffle(string[] array, int number)
{
    string temp = String.Empty;
    string temp2 = String.Empty;
    while (number != 0)
    {
        int i = new Random().Next(0, array.Length);
        temp = array[i];
        int l = new Random().Next(0, array.Length);
        temp2 = array[l];
        if (temp != temp2)
        {
            array[i] = temp2;
            array[l] = temp;
            number = number - 2;
        }

    }
    return array;
}

void PrintImage(string[] doubleArray) // вывод колоды на консоль
{
    for (int j = 0; j < doubleArray.Length; j++)
    {
        Console.Write($"{doubleArray[j]} ");
    }
    Console.WriteLine();
}

int GetSumCard(string[] array)
{
    int sum = 0;
    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < 1; j++)
        {
            if (Char.IsDigit(array[i][j]) == true)
            {
                sum += Convert.ToInt32(array[i]);
            }
            else if (array[i][j] != 'т')
            {
                sum += 10;
            }
            else sum += 11;
        }
    }
    return sum;
}

string[] cardDesk = RefillArray(cardRank);
//PrintImage(cardDesk);
//Console.WriteLine();

string[] shuffledCardDesk = DeskShuffle(cardDesk, numberOfShuffle);
PrintImage(shuffledCardDesk);

int result = GetSumCard(shuffledCardDesk);
Console.WriteLine(result);

