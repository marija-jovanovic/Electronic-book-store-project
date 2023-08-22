<h1 align = center>Electronic book store project</h1>
<div align="center"><img src="https://images.theconversation.com/files/45159/original/rptgtpxd-1396254731.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=754&fit=clip"></div>

 <b><h2 align = center>Introduction</h2></b>

This is the very first MVC Web Application project that I made for my university course - Development of server-based WEB applications.

More information about this university course can be found at the following <a href="https://feit.ukim.edu.mk/en/subject/development-of-server-based-web-applications/">link</a>.

The MVC Web Application is created using ASP.NET Core and it simulates an electronic book store. It allows users to view all books with an option to filter by title, search books belonging to a specific genre, create reviews about books that the user had already bought, edit/delete existing reviews and view all authors with the option to filter by name, surname and nationality. 

<h2 align = center>Goal and requirements</h2>

The creation of this project was divided into two phases.

<h3>First Phase</h3>

In the first phase, I was required to do the following things:

 Based on the structure of the database, I was supposed to create the initial version of the Web Application using code-first approach. More specifically, I was required to build data models according to the database specifications and connect different entities with the appropriate one-to-many and many-to-many relations. The required relations were:

 - One book is related with one author through the foreign key AuthorId. One author can have more than one book.
 - One book can belong to many different genres, and one genre can be related with many books. This relation is enabled through the associative table BookGenre where BookId and genreId are foreign keys to Book and Genre, respectively.
 - One book can have many user reviews, enabled through the table Review, foreign key BookId.
 - One book can be bought by many users, enabled through the table UserBooks, foreign key BookId.

 For every Author we store its first name, last name, date of birth, nationality and gender.
 
 For every Review we store the user who created it, comment and rating.
 
 For every Genre we store its name.
 
 For every Book we store its title, year when it was published, number of pages, description, publisher, picture of the front page, link to be downloaded as pdf and average grade. The average grade is calculated based upon the total of all review grades for the same book.

 The last thing required in the first phase was to create the appropriate MVC controllers and views. The controllers should support actions for adding new entities, modifying and deleting existing entitites, listing all entities and relation creation between the corresponding entities. The views will be simple user interfaces for enforcing the needed controller actions. The following actions and views should be enabled:

 - Ability to view all books, with the option to filter by title. All necessary details about the book should be displayed, as mentioned above.

 - Ability to view all books belonging to a specific genre, with all the necessary details displayed as mentioned earlier.

 - Views/actions for adding new books and editing/deleting existing books. The book should be able to relate to many genres and a different author upon editing.

 - Views/actions for adding new, editing and deleting genres.

 - Views/actions for adding new, editing and deleting authors.

 - Views/actions for adding new, editing and deleting reviews.

 - Ability to view all authors with the option to filter by name, surname and nationality. In the Details view of an Author, all of its books should be displayed.

<h3>Second Phase</h3>

The second phase was all about adding authentication and authorization to users.

The system consists of three types of users: administrator, registered user and unregistered user. The administrator and registered user will have their own identity role, whereas the unregistered user will not have its own role.

The admin and registered users can sign in with their email address and password. The system also allows user registration, however administrators are not able to register. In other words, by performing the registration process the user is automatically assigned to the registered user role.

The system consists of only one administrator, inserted manually into the database. 

Both the administrator and registered users are able to change their passwords if they wish to.

As a service for authentication and authorization in this project, ASP.NET Core Identity was used.

After the Identity service is implemented, the next step was to separate the functionalities available to different users. By separation it means there should be defined different actions and views, as well as access authorization for the actions and views.

<h4>Functionalities for each user type</h4>

The <b>*Administrator*</b> is authorized to view all existing books, authors and genres. It is also authorized to create new books, authors, genres and edit/delete existing ones. The admin is authorized to relate books with specific authors and genres, upload front page pictures for the books as well as provide a download link for the book in pdf format.

The <b>*Unregistered users*</b> have a view type of access to the application. They are authorized to view all books, authors and genres. They are allowed to filter books by title, genre and author. They are authorized to view all authors with the option to filter by name, surname and nationality. By going to the Details page of a specific author, they should be able to view all its books and their necessary info. By going to the Details page of a specific book, they should be able to view all the necessary info for that book, except for the pdf-download link. In the same page, they should be able to read all the reviews for the chosen book.

The <b>*Registered users*</b> have all the functionalities as the unregistered users, with some additional permissions. They are allowed to buy books and write reviews only for the books they have already bought. With every book purchase, the book is inserted into the list of "my books" for that user. The user has the ability to view all of its purchased books and it can download its books in pdf format. Additionally, the user can grade and write reviews only for its purchased books.  

<h2 align = center>Personal summary</h2>

The process of creating this project, by dividing it into two separate phases was very helpful. The first phase helped me learn to create data models according to given database specifications and connect entities between eachother with the appropriate one-to-many and many-to-many relations. It also helped me learn to create appropriate MVC controllers supporting various actions such as adding new entitites, editing/deleting existing entitites, listing all entities, as well as creating views (user interfaces) enforcing the actions from the controllers. 

The second phase helped me learn about the implementation of authentication and authorization in an MVC ASP.NET Core Web Application, by creating different user roles and assigning separate functionalities for each user type.

Overall, this project helped me build the basic foundation skills for creating a well-functioning MVC ASP.NET Core Web Application.

