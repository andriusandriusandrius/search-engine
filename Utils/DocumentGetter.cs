using search_engine.Models;

namespace search_engine.Utils
{
    public static class DocumentGetter
    {
        public static Dictionary<int, DocumentFile> GetDocuments()
        {
            Dictionary<int, DocumentFile> documents = new();
            try
            {
                int id = 1;
                var filePaths = Directory.GetFiles("Documents");
                if (filePaths.Length == 0)
                {
                    throw new FileNotFoundException("No documents found");
                }
                foreach (var path in Directory.GetFiles("Documents"))
                {
                    documents[id] = new DocumentFile(
                        Path.GetFileNameWithoutExtension(path),
                        File.ReadAllText(path)
                    );
                    id++;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
            return documents;
        }
    }
}