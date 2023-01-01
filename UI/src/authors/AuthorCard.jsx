import { React } from "react";
import { useNavigate } from "react-router-dom";
import { Card, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { AuthorStatusMap, getAuthorHeaderColor } from "../utils/EnumMaps";
// import { getAuthorHeaderColor } from "../utils/getAuthorHeaderColor";
import { roundToTwo } from "../utils/roundToTwo";

function AuthorCard(props) {
  const author = props.author;

  const navigate = useNavigate();

  return (
    <Card
      border="light"
      style={{ width: "19rem" }}
      className="book-card-container"
      onClick={() => navigate(`/authors/${author.Id}`)}>
      <Card.Header style={{ color: getAuthorHeaderColor(author.Status) }}>
        {AuthorStatusMap[author.Status]}
      </Card.Header>
      <Card.Body>
        <Card.Title className="book-card-rating">
          <FontAwesomeIcon icon={faStar} className="icon-star" />
          {author.AverageRating ? roundToTwo(author.AverageRating) : "?"}
          /10
        </Card.Title>
      </Card.Body>
      <Card.Img
        as={Image}
        variant="top"
        src={`/api/image/authors/${author.ImageUrl}`}
        alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
        fluid="md"
        className="book-card-img"
      />
      <Card.Body>
        <Card.Title>{author.Name}</Card.Title>
        <Card.Subtitle>{author.Country}</Card.Subtitle>
      </Card.Body>
    </Card>
  );
}

export default AuthorCard;
