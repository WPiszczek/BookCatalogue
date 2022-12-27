import { React } from "react";
import { Card, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { BookCategoryMap } from "../utils/MapBookCategory";
import "./BookCard.css";

function BookCard(props) {
  const book = props.book;

  return (
    <Card
      border="light"
      style={{ width: "19rem" }}
      className="book-card-container">
      <Card.Header>{BookCategoryMap[book.Category]}</Card.Header>
      <Card.Img
        as={Image}
        variant="top"
        src={`/api/image/books/${book.ImageUrl}`}
        alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
        fluid="md"
        className="book-card-img"
      />
      <Card.Body>
        <Card.Title>{book.Title}</Card.Title>
        <Card.Subtitle>
          {book.Author.Name}, {book.ReleaseYear}
        </Card.Subtitle>
        <Card.Text>{book.Description}</Card.Text>
      </Card.Body>
    </Card>
  );
}

export default BookCard;
