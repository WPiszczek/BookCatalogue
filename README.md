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

- GetAuthors(name)
- GetAuthorById(id)
- AddAuthor(author)
- UpdateAuthorImageUrl(authorId, imageUrl)
- UpdateAuthor(author)
- DeleteAuthor(author)

### 2.3 ReviewsRepository

- GetReviews(bookId, rating)
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

- GET /books?author=&category=&language=&title=
- GET /books/id
- POST /books
- PATCH /books/id
- PUT /books/id
- DELETE /books/id

- GET /authors?name=
- GET /authors/id
- POST /authors
- PATCH /authors/id
- PUT /authors/id
- DELETE /authors/id

- GET /reviews?book=&rating=
- GET /reviews/id
- POST /reviews
- PUT /reviews/id
- DELETE /reviews/id
