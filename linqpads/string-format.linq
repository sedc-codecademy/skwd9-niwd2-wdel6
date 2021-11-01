<Query Kind="Statements" />

// var result = string.Format("Hello {0} from {1}", "SEDC", "Wekoslav");
var sedc = "SEDC";
var wekoslav = "Wekoslav";
//var result = string.Format("Hello {0} from {1}", sedc, wekoslav);
var result = $"Hello {sedc} from {wekoslav}";
Console.WriteLine(result);