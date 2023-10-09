# OnlineLibrarySystem

This Online Library Application is based on REST APIs. 
The API Services have operations for the following:
1.	Books can be added to the Application.
a.	Book ID, Book Name, Author, Publisher can be considered as attributes to the book.
2.	Books can be searched or retrieved based on search criteria like 
Author, Publisher, Name of the Book
3.	Remove the book from the library only for the Authorized User. You can use JWT for creating Token for Authorization & validating the user.

  	Currently only Login available with username ```admin```, password ```admin```
5.	Update the Book name, or Author, or Publisher.

Requirements:
1.	APIs and Operations in REST API should be well defined.
2.	Consider adding headers for capturing date time, unique requests.
3.	Using C# .Net 6 and above.
4.	Mocks can be used to get the data.
5.	Unit testing should be written for the methods.
6.	No front-end development is required. All the inputs and responses are evaluated in REST API only.
