import React from "react";
import { Container, Row } from "react-bootstrap";
import BookCard from "./BookCard";

function BooksContainer(props) {
  const books = props.books.map((book) => {
    return <BookCard key={book.Id} book={book} />;
  });

  return (
    <Container fluid="md">
      <Row md={4} className="center">
        {books}
      </Row>
    </Container>
  );
}

export default BooksContainer;
