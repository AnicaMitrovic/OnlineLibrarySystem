# OnlineLibrarySystem

This Online Library Application is based on REST APIs. 
The API Services have operations for the following:
1.	Books can be added to the Application.
  Book ID, Book Name, Author, Publisher can be considered as attributes to the book.
2.	Books can be searched or retrieved based on search criteria like 
Author, Publisher, Name of the Book
3.	Remove the book from the library only for the Authorized User. You can use JWT for creating Token for Authorization & validating the user.

  	Currently only Login available with username ```admin```, password ```admin```
4.	Update the Book name, or Author, or Publisher.

#### Getting Started
Authentication
Initial Login Credentials:

Username: ```admin```
Password: ```admin```

#### Book Operations
- Add Books: Insert new books into the system, specifying attributes like Book ID, Name, Author, and Publisher.
- Search Books: Retrieve books based on search criteria - Author, Publisher, or Book Name.
- Update Details: Authorized alteration of details like Book Name, Author, and Publisher.
- Remove Books: Secure removal functionality accessible only by authorized users.

#### Technical Insights
- C# with .NET 7: The project is built using the .NET 7.
- Onion Architecture: Adherence to Onion (or Clean) Architecture principles, ensuring a separation of concerns and enhancing maintainability.
- Mock Data Utilization: Mock data is employed for various operations, simulating database interactions.
- Unit Testing: Unit tests written using xUnit.
