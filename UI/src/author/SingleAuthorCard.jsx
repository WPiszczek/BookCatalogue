import { React } from "react";
import { Button, Card, Col, Container, Image, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { AuthorStatusMap, getAuthorHeaderColor } from "../utils/EnumMaps";
import { roundToTwo } from "../utils/roundToTwo";

function SingleAuthorCard(props) {
  const author = props.author;
  const navigate = useNavigate();

  const addBook = (event) => {
    event.preventDefault();
    console.log("Add book");
    props.addBook();
  };

  const editAuthor = (event) => {
    event.preventDefault();
    console.log("Edit author", author.Id);
    props.editAuthor();
  };

  const editAuthorImage = (event) => {
    event.preventDefault();
    console.log("Edit author image", author.Id);
    props.editAuthorImage();
  };

  const deleteAuthor = (event) => {
    event.preventDefault();
    console.log("Delete author", author.Id);
    props.deleteAuthor();
  };

  return (
    <Card border="light" className="single-book-card">
      <Card.Header style={{ color: getAuthorHeaderColor(author.Status) }}>
        {AuthorStatusMap[author.Status]}
      </Card.Header>
      <Row>
        <Col md={4}>
          <Card.Img
            as={Image}
            variant="top"
            src={`/api/image/authors/${author.ImageUrl}`}
            alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
            fluid="md"
            className="book-card-img"
          />
        </Col>
        <Col md={8}>
          <Card.Body>
            <Card.Title className="single-book-card-rating single-book-card-header">
              <FontAwesomeIcon icon={faStar} className="icon-star" />
              {author.AverageRating ? roundToTwo(author.AverageRating) : "?"}
              /10
            </Card.Title>
            <Row>
              <Col md={7}>
                <Card.Title className="single-book-card-title">
                  {author.Name}
                </Card.Title>
                <Card.Text>
                  <span className="single-book-card-header">Country: </span>
                  {author.Country}
                </Card.Text>
                <Card.Text>
                  <span className="single-book-card-header">
                    Date of birth:{" "}
                  </span>
                  {author.BirthDate.slice(0, 10)}
                </Card.Text>
                {author.DeathDate !== null && (
                  <Card.Text>
                    <span className="single-book-card-header">
                      Date of death:{" "}
                    </span>
                    {author.DeathDate.slice(0, 10)}
                  </Card.Text>
                )}
              </Col>
              <Col md={5} className="single-book-button-container">
                <Container>
                  <Button
                    variant="primary"
                    className="single-book-button"
                    onClick={(e) => editAuthor(e)}>
                    Edit author
                  </Button>
                  <Button
                    variant="secondary"
                    className="single-book-button"
                    onClick={(e) => editAuthorImage(e)}>
                    Edit author picture
                  </Button>
                  <Button
                    variant="success"
                    className="single-book-button"
                    onClick={(e) => addBook(e)}>
                    Add book
                  </Button>
                  <Button
                    variant="danger"
                    className="single-book-button"
                    onClick={(e) => deleteAuthor(e)}>
                    Delete author
                  </Button>
                </Container>
              </Col>
            </Row>
          </Card.Body>
        </Col>
      </Row>
    </Card>
  );
}

export default SingleAuthorCard;
