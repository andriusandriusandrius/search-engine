namespace search_engine.Utils
{
    public static class DocumentGetter
    {
        public static Dictionary<int, string> GetDocuments()
        {
            Dictionary<int, string> documents = new();
            int id = 0;
            foreach (var path in Directory.GetFiles("Documents"))
            {
                documents[id] = File.ReadAllText(path);
                id++;
            }
            return documents;
        }
    }
}