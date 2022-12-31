import { React } from "react";
import { useNavigate } from "react-router-dom";
import { Card, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { AuthorStatusMap } from "../utils/EnumMaps";
import { roundToTwo } from "../utils/roundToTwo";

function AuthorCard(props) {
  const author = props.author;

  const navigate = useNavigate();

  const getHeaderColor = () => {
    const status = AuthorStatusMap[author.Status];
    if (status === "Alive") {
      return "#198754";
    } else if (status === "Dead") {
      return "black";
    } else {
      return "grey";
    }
  };

  return (
    <Card
      border="light"
      style={{ width: "19rem" }}
      className="book-card-container"
      onClick={() => navigate(`/authors/${author.Id}`)}>
      <Card.Header style={{ color: getHeaderColor() }}>
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
        {/* <Card.Text>{book.Description}</Card.Text> */}
      </Card.Body>
    </Card>
  );
}

export default AuthorCard;
