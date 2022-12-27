import React from "react";
import { Container, Row } from "react-bootstrap";
import BookCard from "./BookCard";
import "./BooksContainer.css";
import PlaceholderBookCard from "./AddBookCard";

function BooksContainer(props) {
  const books = props.books.map((book) => {
    return <BookCard key={book.Id} book={book} />;
  });

  return (
    <Container fluid="md" className="books-container">
      <Row md={4} className="center books-container-row">
        {books}
        <PlaceholderBookCard addBook={props.addBook} />
      </Row>
    </Container>
  );
}

export default BooksContainer;
