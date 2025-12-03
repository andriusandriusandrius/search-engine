### An inverted index based search engine

This project implements a simple search engine using an inverted index, allowing for fast lookup of documents based on keyword queries.

### Tokenizing Documents

To begin, we first have to tokenize every word of every file in our selected directory. After that, we must construct an inverted index, with token being the key and postings being the value.

```
var documents = DocumentGetter.GetDocuments(); // Step 1: Get documents

foreach (var document in documents)
{
    searchEngine.IndexDocument(document.Value); // Step 2: Index each document
}

var ats = searchEngine.Search("instantly and space");
foreach (var at in ats)
{
    Console.WriteLine($"Document: {at.Document.Title}, Score: {at.Score}");
}

```
### Now what exactly is a posting?

A posting describes:

- Which document contains the term

- Where in the document the term appears

- How frequently it appears

By storing only the positions of each word in a document, we can naturally derive the frequency without additional bookkeeping. Nice!

### So what about the actual searching?

The searching in my application follows a really simple flow:

<img width="244" height="337" alt="image" src="https://github.com/user-attachments/assets/bd12e1c8-ab5d-4cc7-aac6-27c5724f3fe1" />


As you can see from the graph we
1. Get a search query
2. Tokenize the query
3. Convert from infix to postfix using the Shunting Yard algorithm

### Why do we convert the query to postfix?

Simply put, infix is not very clean to work with, because of a little thing called **order of operations**. The structure of the query would be such that we would complex logic and temperory storage since we would need to figure out which operation happens when.

A better solution is to simply create a list with the operands and operators which have the **order of operations** ingrained in the very position of the elements. As such we use **postfix**.

### Expression tree

Now we can simply convert the postfix query to an expression tree of nodes and using recursive functions **evaluate** each and every node until the solution bubbles up to the top.

Since this is done by singular nodes in an expression tree we don't need to write an overarching function and let the nodes themselves do all the work.

### Nice! How do I run this?

To try it out simply ``` git clone ``` it and then use the function ```dotnet run```, as you hopefully won't need to install any libraries or anything else since this is project based on simple data structures.

### Features still in development:
- Brackets
- Frontend
- More operations (e.g NOT)
- Tests
- Error handling


### Useful resources I found while making this project:

**TF-IDF** (the algorithm by which I rank the results)**:**

https://www.youtube.com/watch?v=A5ounv0D_cs

**Shauntin yard algorithm:**

https://mathcenter.oxford.emory.edu/site/cs171/shuntingYardAlgorithm

https://www.youtube.com/watch?v=Wz85Hiwi5MY

