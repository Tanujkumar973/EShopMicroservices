string input = Console.ReadLine();
String[] arr = input.Split(' ');
int a = Convert.ToInt32(arr[0]);
int b = Convert.ToInt32(arr[1]);
int c = Convert.ToInt32(arr[2]);

Console.WriteLine((a + b) / 2 > c ? "YES" : "NO");