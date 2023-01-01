import { React } from "react";
import { Button, Card, Col, Container, Image, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar, faUpRightFromSquare } from "@fortawesome/free-solid-svg-icons";
import { BookCategoryMap } from "../utils/EnumMaps";
import { roundToTwo } from "../utils/roundToTwo";
import "../common/css/SingleCard.css";

function SingleBookCard(props) {
  const book = props.book;
  const navigate = useNavigate();

  const addReview = (event) => {
    event.preventDefault();
    console.log("Add review");
    props.addReview();
  };

  const editBook = (event) => {
    event.preventDefault();
    console.log("Edit book", book.Id);
    props.editBook();
  };

  const editBookImage = (event) => {
    event.preventDefault();
    console.log("Edit book image", book.Id);
    props.editBookImage();
  };

  const deleteBook = (event) => {
    event.preventDefault();
    console.log("Delete book", book.Id);
    props.deleteBook();
  };

  return (
    <Card border="light" className="single-card">
      <Card.Header>{BookCategoryMap[book.Category]}</Card.Header>
      <Row>
        <Col md={4}>
          <Card.Img
            as={Image}
            variant="top"
            src={`/api/image/books/${book.ImageUrl}`}
            alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
            fluid="md"
            className="card-img"
          />
        </Col>
        <Col md={8}>
          <Card.Body>
            <Card.Title className="single-card-rating single-card-header">
              <FontAwesomeIcon icon={faStar} className="icon-star" />
              {book.AverageRating ? roundToTwo(book.AverageRating) : "?"}
              /10
            </Card.Title>
            <Row>
              <Col md={7}>
                <Card.Title className="single-card-title">
                  {book.Title}
                </Card.Title>
                <Card.Text>
                  <span className="single-card-header"> Author: </span>
                  {book.Author.Name}{" "}
                  <FontAwesomeIcon
                    icon={faUpRightFromSquare}
                    className="icon-link"
                    onClick={() => navigate(`/authors/${book.Author.Id}`)}
                  />
                </Card.Text>
                <Card.Text>
                  <span className="single-card-header"> Release year: </span>
                  {book.ReleaseYear}
                </Card.Text>
                <Card.Text>
                  <span className="single-card-header"> Description: </span>
                </Card.Text>
                <Card.Text>{book.Description}</Card.Text>
              </Col>
              <Col md={5} className="single-button-container">
                <Container>
                  <Button
                    variant="primary"
                    className="single-button"
                    onClick={(e) => editBook(e)}>
                    Edit book
                  </Button>
                  <Button
                    variant="secondary"
                    className="single-button"
                    onClick={(e) => editBookImage(e)}>
                    Edit book picture
                  </Button>
                  <Button
                    variant="success"
                    className="single-button"
                    onClick={(e) => addReview(e)}>
                    Add review
                  </Button>
                  <Button
                    variant="danger"
                    className="single-button"
                    onClick={(e) => deleteBook(e)}>
                    Delete book
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

export default SingleBookCard;
