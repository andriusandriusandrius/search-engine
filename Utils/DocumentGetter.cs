using search_engine.Models;

namespace search_engine.Utils
{
    public static class DocumentGetter
    {
        public static Dictionary<int, DocumentFile> GetDocuments()
        {
            Dictionary<int, DocumentFile> documents = new();
            int id = 1;
            foreach (var path in Directory.GetFiles("Documents"))
            {
                documents[id] = new DocumentFile(
                     Path.GetFileNameWithoutExtension(path),
                    File.ReadAllText(path)
                );
                id++;
            }
            return documents;
        }
    }
}