# BookCatalogue

### Projekt Programowanie Wizualne 2022

## 1. Encje

![alt obrazek](entity_diagram.png)

## 2. Metody:

### 2.1 BooksRepository

- GetBooks(title, authorId, category)
- GetBookById(id)
- AddBook(book)
- UpdateBookImageUrl(bookId, imageUrl)
- UpdateBook(book)
- DeleteBook(book)

### 2.2 AuthorsRepository

- GetAuthors(name, status)
- GetAuthorById(id)
- AddAuthor(author)
- UpdateAuthorImageUrl(authorId, imageUrl)
- UpdateAuthor(author)
- DeleteAuthor(author)

### 2.3 ReviewsRepository

- GetReviews(bookId)
- GetReviewById(id)
- AddReview(review)
- UpdateReview(review)
- DeleteReview(review)

### 2.4 ImageRepository

- GetImage(name, directory)
- PostImage(file, directory)
- PutImage(file, directory, currentName)
- DeleteImage(directory, name)

## 3. API:

### 3.1 BooksController

- GET /api/books?author=&category=&language=&title=
- GET /api/books/{id}
- POST /api/books
- PATCH /api/books/{id}
- PUT /api/books/{id}
- DELETE /api/books/{id}

### 3.2 AuthorsController

- GET /api/authors?name=&status=
- GET /api/authors/{id}
- POST /api/authors
- PATCH /api/authors/{id}
- PUT /api/authors/{id}
- DELETE /api/authors/{id}

### 3.3 ReviewsController

- GET /api/reviews?book=
- GET /api/reviews/{id}
- POST /api/reviews
- PUT /api/reviews/{id}
- DELETE /api/reviews/{id}

### 3.4 ImageController

- GET /api/image/{directory}/{name}
