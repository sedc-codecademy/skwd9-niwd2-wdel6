// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.Text.Json;

using var sqlConnection = new SqlConnection("Server=.;Database=Books;User Id=bookz;Password=bookz;");
sqlConnection.Open();

// needed SQL Commands
using var authorCommand = new SqlCommand("insert into Authors values (@ID, @Name)", sqlConnection);
authorCommand.Parameters.Add("ID", System.Data.SqlDbType.Int);
authorCommand.Parameters.Add("Name", System.Data.SqlDbType.NVarChar);

using var bookSeekCommand = new SqlCommand("select count(*) from Books where ID = @ID", sqlConnection);
bookSeekCommand.Parameters.Add("ID", System.Data.SqlDbType.Int);

using var bookSaveCommand = new SqlCommand("insert into Books values (@ID, @Title, @Year, @TypeId)", sqlConnection);
bookSaveCommand.Parameters.Add("ID", System.Data.SqlDbType.Int);
bookSaveCommand.Parameters.Add("Title", System.Data.SqlDbType.NVarChar);
bookSaveCommand.Parameters.Add("Year", System.Data.SqlDbType.Int);
bookSaveCommand.Parameters.Add("TypeId", System.Data.SqlDbType.Int);

using var bookAuthorCommand = new SqlCommand("insert into BookAuthors (BookID, AuthorID) values (@BookID, @AuthorID)", sqlConnection);
bookAuthorCommand.Parameters.Add("BookID", System.Data.SqlDbType.Int);
bookAuthorCommand.Parameters.Add("AuthorID", System.Data.SqlDbType.Int);

using var bookTypeCommand = new SqlCommand("insert into BookTypes values (@ID, @Type)", sqlConnection);
bookTypeCommand.Parameters.Add("ID", System.Data.SqlDbType.Int);
bookTypeCommand.Parameters.Add("Type", System.Data.SqlDbType.NVarChar);

using var awardCommand = new SqlCommand("insert into Awards values (@ID, @Name)", sqlConnection);
awardCommand.Parameters.Add("ID", System.Data.SqlDbType.Int);
awardCommand.Parameters.Add("Name", System.Data.SqlDbType.NVarChar);

using var nominationCommand = new SqlCommand("insert into Nominations (AwardID, TitleID, Year, Winner) values (@AwardID, @TitleID, @Year, @Winner)", sqlConnection);
nominationCommand.Parameters.Add("AwardID", System.Data.SqlDbType.Int);
nominationCommand.Parameters.Add("TitleID", System.Data.SqlDbType.Int);
nominationCommand.Parameters.Add("Year", System.Data.SqlDbType.Int);
nominationCommand.Parameters.Add("Winner", System.Data.SqlDbType.Bit);

// book type cache
Dictionary<string, BookType> bookTypes = new();

// read all authors
var authorsText = File.ReadAllText("data/authors.json");
var authorsData = JsonSerializer.Deserialize<IEnumerable<Author>>(authorsText)!;
Console.WriteLine($"Loaded {authorsData.Count()} authors");

var index = 0;

// insert each author in the database
foreach (var author in authorsData)
{
    authorCommand.Parameters["ID"].Value = author.id;
    authorCommand.Parameters["Name"].Value = author.name;

    authorCommand.ExecuteNonQuery();
    
    // read books from this author
    var booksText = File.ReadAllText($"data/authors/{author.id}.json");
    var booksData = JsonSerializer.Deserialize<IEnumerable<Book>>(booksText)!;


    foreach (var book in booksData)
    {
        // is the book already in the db?
        bookSeekCommand.Parameters["ID"].Value = book.id;
        var response = (int)bookSeekCommand.ExecuteScalar();
        if (response == 0)
        {
            // insert the book in the database
            bookSaveCommand.Parameters["ID"].Value = book.id;
            bookSaveCommand.Parameters["Title"].Value = book.title;
            bookSaveCommand.Parameters["Year"].Value = book.year;
            if (!bookTypes.TryGetValue(book.type, out var bookType))
            {
                var nextId = (bookTypes.Count > 0 ? bookTypes.Values.Max(bt => bt.ID) : 0) + 1;
                bookType = new BookType(nextId, book.type);
                bookTypes.Add(book.type, bookType);
                bookTypeCommand.Parameters["ID"].Value = bookType.ID;
                bookTypeCommand.Parameters["Type"].Value = bookType.Type;
                bookTypeCommand.ExecuteNonQuery();
            }
            bookSaveCommand.Parameters["TypeId"].Value = bookType.ID;

            bookSaveCommand.ExecuteNonQuery();
        }

        // associate book with author
        bookAuthorCommand.Parameters["BookID"].Value = book.id;
        bookAuthorCommand.Parameters["AuthorID"].Value = author.id;

        bookAuthorCommand.ExecuteNonQuery();
        // Console.WriteLine($"  Finished with {book.title}");
    }
    index += 1;
    if ((index % 100) == 0)
    {
        // only log on each hundred authors
        Console.WriteLine($"Finished with {author.name} ({booksData.Count()} books processed) - {index} / {authorsData.Count()} authors processed");
    }
}

Console.WriteLine("Done with authors, starting on awards");

// read all awards
var awardsText = File.ReadAllText("data/awards.json");
var awardsData = JsonSerializer.Deserialize<IEnumerable<Award>>(awardsText)!;
Console.WriteLine($"Loaded {awardsData.Count()} awards");

// insert each award in the database
foreach (var award in awardsData)
{
    awardCommand.Parameters["ID"].Value = award.id;
    awardCommand.Parameters["Name"].Value = award.name;

    awardCommand.ExecuteNonQuery();

    // read nominations for this award
    var nominationsText = File.ReadAllText($"data/awards/{award.id}.json");
    var nominationData = JsonSerializer.Deserialize<IEnumerable<Nomination>>(nominationsText)!;

    foreach (var nomination in nominationData)
    {
        // insert the nomination in the database
        nominationCommand.Parameters["AwardID"].Value = award.id;
        nominationCommand.Parameters["TitleID"].Value = nomination.titleId;
        nominationCommand.Parameters["Year"].Value = nomination.year;
        nominationCommand.Parameters["Winner"].Value = nomination.winner;

        nominationCommand.ExecuteNonQuery();
    }
    Console.WriteLine($"Finished with {award.name} ({nominationData.Count()} nominations processed)");
}


// Record Definitions

record Author (int id, string name);
record Book (int id, string title, int year, string type);
record BookType (int ID, string Type);

record Award(int id, string name);
record Nomination(int titleId, int year, bool winner);