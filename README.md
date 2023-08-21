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

 Based on the structure of the database, create the initial version of the Web Application using code-first approach. More specifically, I was required to build data models according to the database specifications and connect different entities with the appropriate one-to-many and many-to-many relations. The required relations were:

 - One book is related with one author through the foreign key AuthorId. One author can have more than one book.
 - One book can belong to many different genres, and one genre can be related with many books. This relation is enabled through the associative table BookGenre where BookId and genreId are foreign keys to Book and Genre.
 - One book can have many user reviews, enabled through the table Review, foreign key BookId.
 - One book can be bought by many users, enabled through the table UserBooks, foreign key BookId.

 For every Author we store its first name, last name, date of birth, nationality and gender.
 For every Review we store the user who created it, comment and rating.
 For every Genre we store its name.
 For every Book we store its title, year when it was published, number of pages, description, publisher, picture of the front page, link to be downloaded as pdf and average grade. The average grade is calculated based upon the total of all review grades for the same book.

 The last thing required in the first phase was to the appropriate MVC controllers and views. The controllers should support actions for adding new entities, modifying and deleting existing entitites, listing all entities and relation creation between the corresponding entities. The views will be simple user interfaces for enforcing the needed controller actions. The following actions and views should be enabled:

 - Ability to view all books, with the option to filter by title. All necessary details about the book should be displayed, as mentioned above.

 - Ability to view all books belonging to a specific genre, with all the necessary details displayed as mentioned earlier.

 - Views/actions for adding new books and editing/deleting existing books. The book should be able to relate to many genres and a different author upon editing.

 - Views/actions for adding new, editing and deleting genres.

 - Views/actions for adding new, editing and deleting authors.

 - Views/actions for adding new, editing and deleting reviews.

 - Ability to view all authors with the option to filter by name, surname and nationality. In the Details view of an Author, all of its books should be displayed.

<h3>Second Phase</h3>


