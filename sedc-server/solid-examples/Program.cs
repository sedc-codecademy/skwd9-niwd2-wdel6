// See https://aka.ms/new-console-template for more information

var GetArea = (Rectangle r) => r.Width * r.Height;
var CutInHalf = (Rectangle r) => r.Height /= 2;

var r = new Rectangle { Width = 10, Height = 4 };
Console.WriteLine($"Area is {GetArea(r)}");
CutInHalf(r);
Console.WriteLine($"Area is {GetArea(r)}");

Console.WriteLine("---------");

var sq = new Square { Width = 10 };
Console.WriteLine($"Area is {GetArea(sq)}"); 
CutInHalf(sq);
Console.WriteLine($"Area is {GetArea(sq)}");



class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

class Square : Rectangle {
    public override int Height { get => base.Width; set => base.Width = value; }
}
