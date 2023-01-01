import React from "react";
import { Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import BookCard from "./BookCard";
import AddBookCard from "./AddBookCard";
import "../common/css/Container.css";

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
    <Container fluid="md" className="container">
      <Row md={4} className="center container-row">
        {books}
        <AddBookCard addBook={props.addBook} />
      </Row>
    </Container>
  );
}

export default BooksContainer;
