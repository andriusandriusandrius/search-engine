using System.Reflection;

namespace search_engine.Models
{
    public class DocumentFile
    {
        private string _title;
        private string _text;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cant be empty");
                }
                _title = value;
            }
        }
        public string Text
        {
            get => _text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text cant be empty");
                }
                _text = value;
            }
        }
        public DocumentFile(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be empty.");
            _title = title;
            _text = text;
        }
    }
}