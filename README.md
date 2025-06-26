
Users can register and log in (wityh ASP.NET Core Identity)
Only logged-in users can leave reviews or vote on them
View a list of books (filter by genre, year, and rating)
View book details along and reviews
Logged-in users can:
Add reviews
Upvote or downvote each review (1 vote per user)
Users can also create, edit, or delete books (normally an admin privildge but this is beyond the scope of this project)

RESTful API Endpoints
- `GET /api/books`  list all books 
- `GET /api/books/{id}`  get details of a book
- `POST /api/books`  create a book (authenticated)
- `GET /api/books/{id}/reviews`  list reviews of a book
- `POST /api/reviews`  add a new review (if logged in)
- `POST /api/reviews/{id}/vote` like or dislike a review (if logged in)

This project is split into three parts.

BooksWebApp.Core has the core stuff like the Book and Review classes, and the interfaces for the repositories.

BooksWebApp.Infrastructure is where the database logic is. It connects the app to the database using Entity Framework.

BooksWebApp.Web is the main app. It has the pages, the controllers, and handles user login and the UI.

They all work together as one solution. You just open the solution file and everything should be ready to go.

The project uses MVC architecture

Entities: ( Book, Review, ReviewVote, ApplicationUser)
Interfaces : Repo interfaces
Infrastructure
Data → Db
Repositories 

Application
DTO
ViewModels 

Web
Controllers
MVC : UI controllers 
Api : REST API endpoints 
Views  
Identity : Register/Login pages via ASP.NET Core Identity

All database work uses **EF Core Code-First**
- Async/await is used throughout



**************************************************************************************************************************************


HOW TO RUN: 

1. Clone the repo
2. Set up a connection string in appsettings.json
3. Run migrations (dotnet ef database update)
4. Start the project 
