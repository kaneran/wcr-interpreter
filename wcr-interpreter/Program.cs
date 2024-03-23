Console.WriteLine("Hello, please input some wcr code and then press the Enter key");


int x = 2;

switch (x)
{
    case -1:
        Console.WriteLine("Yes");
        break;
    default:
        if (x == 0) Console.WriteLine("Wow");
        else Console.WriteLine("Nope");
        break;
}


var Ch = '=';
var isWhiteSpace = Ch.Equals(' ') || Ch.Equals('\n') || Ch.Equals('\r') || Ch.Equals('\t');

Console.WriteLine(isWhiteSpace);