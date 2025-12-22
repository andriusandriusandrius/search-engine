using search_engine.Engine;
using search_engine.Utils;

SearchEngine searchEngine = new SearchEngine();

var documents = DocumentGetter.GetDocuments();

foreach (var document in documents)
{
    searchEngine.IndexDocument(document.Value);
}
var ats = searchEngine.Search("(history or coffee) and space");
foreach (var at in ats)
{
    Console.WriteLine($" Document: {at.Document.Title}, Score {at.Score}");
}