import { React } from "react";
import { Card, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { BookCategoryMap } from "../utils/EnumMaps";
import "./BookCard.css";

function BookCard(props) {
  const book = props.book;

  return (
    <Card
      border="light"
      style={{ width: "19rem" }}
      className="book-card-container">
      <Card.Header>{BookCategoryMap[book.Category]}</Card.Header>
      <Card.Body>
        <Card.Title className="book-card-rating">
          <FontAwesomeIcon icon={faStar} className="icon-star" />
          {book.AverageRating ?? "?"}
          /10
        </Card.Title>
      </Card.Body>
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
