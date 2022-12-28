import React from "react";
import { Container, Row } from "react-bootstrap";
import BookCard from "./BookCard";
import "./BooksContainer.css";
import AddBookCard from "./AddBookCard";
import { useNavigate } from "react-router-dom";

function BooksContainer(props) {
  const navigate = useNavigate();

  const redirectToBook = (bookId) => {
    navigate(`/books/${bookId}`);
  };

  const books = props.books.map((book) => {
    return (
      <BookCard key={book.Id} book={book} redirectToBook={redirectToBook} />
    );
  });

  return (
    <Container fluid="md" className="books-container">
      <Row md={4} className="center books-container-row">
        {books}
        <AddBookCard addBook={props.addBook} />
      </Row>
    </Container>
  );
}

export default BooksContainer;
