<Query Kind="Program" />

void Main()
{
	var corigin1 = new CPoint {X=0, Y = 0};
	var corigin2 = new CPoint {X=0, Y = 0};
	Console.WriteLine("--- CLASS ---");
	Console.WriteLine(corigin1 == corigin2);
	Console.WriteLine(corigin1.Equals(corigin2));
	
	var sorigin1 = new SPoint {X=0, Y = 0};
	var sorigin2 = new SPoint {X=0, Y = 0};
	
	Console.WriteLine("--- STRUCT ---");
	//Console.WriteLine(sorigin1 == sorigin2);
	Console.WriteLine(object.ReferenceEquals(sorigin1, sorigin2));
	Console.WriteLine(sorigin1.Equals(sorigin2));
	
	var rorigin1 = new RPoint {X=0, Y = 0};
	var rorigin2 = new RPoint {X=0, Y = 0};
	
	Console.WriteLine("--- RECORD ---");
	Console.WriteLine(rorigin1 == rorigin2);
	Console.WriteLine(rorigin1.Equals(rorigin2));
}

public class CPoint {
	public int X {get; set;}
	public int Y {get; set;}
}

public struct SPoint {
	public int X {get; set;}
	public int Y {get; set;}
}

public record RPoint {
	public int X {get; set;}
	public int Y {get; set;}
}