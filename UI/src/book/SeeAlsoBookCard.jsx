import { React } from "react";
import { Card, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { BookCategoryMap } from "../utils/EnumMaps";
import "./SeeAlsoBookCard.css";
import { useNavigate } from "react-router-dom";

function SeeAlsoBookCard(props) {
  const book = props.book;
  const navigate = useNavigate();

  return (
    <Card
      border="light"
      style={{ width: "12rem" }}
      className="see-also-book-card-container"
      onClick={() => navigate(`/books/${book.Id}`)}>
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
        <Card.Subtitle>{book.ReleaseYear}</Card.Subtitle>
      </Card.Body>
    </Card>
  );
}

export default SeeAlsoBookCard;
