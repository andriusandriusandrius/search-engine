### An inverted index based search engine

So, this is a search engine project.

To begin, we first have to (1) tokenize every word of every file in our selected directory, (2) construct an inverted index, with token being the key and postings being the value.

```
var documents = DocumentGetter.GetDocuments(); // <- (1)

foreach (var document in documents)
{
    searchEngine.IndexDocument(document.Value); // <- (2)
}
var ats = searchEngine.Search("instantly and space");
foreach (var at in ats)
{
    Console.WriteLine($" Document: {at.Document.Title}, Score {at.Score}");
}

```
### Now what exactly is a posting?

Well a posting is just a record of exactly **where** and **how frequently** in **what** document does the word appear in.

As you might notice in the construct of posting itself we can merely include the position of the word in a document and in that way also get the frequency it. Nice!


### So what about the actual searching?

The searching in my application follows a really simple flow:

<img width="244" height="337" alt="image" src="https://github.com/user-attachments/assets/bd12e1c8-ab5d-4cc7-aac6-27c5724f3fe1" />


As you can see from the graph (if you want to call it that) we get a search query, we then tokenize the query, which now we can convert, using the Shauntin yard algorithm, to postfix... to postfix...

### Why do we convert the query to postfix?

Simply put, infix is not very clean to work with, because of a little thing called **order of operations**. The structure of the query would be such that we wouldn't be able to access the first operations happening in the query (like AND before OR) as such we would need to store them somewhere in memory and then return and that is a whole hassle in itself.

A better solution is to simply create a list with the operands and operators which have the **order of operations** ingrained in the very position of the elements.

**That is postfix!**

### Expression tree

Now we can simply convert the postfix query to an expression tree of nodes and using recursive functions **evaluate** each and every node until the solution bubbles up to the top.
By evaluating, what I mean is simply getting the documents (which we can do incredibly fast because of the creation of an inverted index) of the word nodes and then giving those sets of documents to be evaluated by operator nodes.

Since this is done by singular nodes in an expression tree we don't need to write an overarching function and let the nodes themselves do all the work.

### Nice! How do I run this?

To try it out simply ``` git clone ``` it and then use the function ```dotnet run```, as you hopefully won't need to install any libraries or anything else since this is project based on simple data structures.

### Features still in development:
- Brackets
- Frontend
- More operations like NOT
- Tests
- Errors


