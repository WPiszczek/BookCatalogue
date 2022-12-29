import { React } from "react";
import { Button, Card, Col, Container, Image, Row } from "react-bootstrap";
import { BookCategoryMap } from "../utils/EnumMaps";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar, faUpRightFromSquare } from "@fortawesome/free-solid-svg-icons";
import "./SingleBookCard.css";
import { useNavigate } from "react-router-dom";

function SingleBookCard(props) {
  const book = props.book;
  const navigate = useNavigate();

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
    <Card border="light" className="single-book-card">
      <Card.Header>{BookCategoryMap[book.Category]}</Card.Header>
      <Row>
        <Col md={4}>
          <Card.Img
            as={Image}
            variant="top"
            src={`/api/image/books/${book.ImageUrl}`}
            alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
            fluid="md"
            className="book-card-img"
          />
        </Col>
        <Col md={8}>
          <Card.Body>
            <Card.Title className="single-book-card-rating single-book-card-header">
              <FontAwesomeIcon icon={faStar} className="icon-star" />
              {book.AverageRating ?? "?"}
              /10
            </Card.Title>
            <Row>
              <Col md={7}>
                <Card.Title className="single-book-card-title">
                  {book.Title}
                </Card.Title>
                <Card.Text>
                  <span className="single-book-card-header"> Author: </span>
                  {book.Author.Name}{" "}
                  <FontAwesomeIcon
                    icon={faUpRightFromSquare}
                    className="icon-link"
                    onClick={() => navigate(`/authors/${book.Author.Id}`)}
                  />
                </Card.Text>
                <Card.Text>
                  <span className="single-book-card-header">
                    {" "}
                    Release year:{" "}
                  </span>
                  {book.ReleaseYear}
                </Card.Text>
                <Card.Text>
                  <span className="single-book-card-header">
                    {" "}
                    Description:{" "}
                  </span>
                </Card.Text>
                <Card.Text>{book.Description}</Card.Text>
              </Col>
              <Col md={5} className="single-book-button-container">
                <Container>
                  <Button
                    variant="primary"
                    className="single-book-button"
                    onClick={(e) => editBook(e)}>
                    Edit book
                  </Button>
                  <Button
                    variant="success"
                    className="single-book-button"
                    onClick={(e) => editBookImage(e)}>
                    Edit book picture
                  </Button>
                  <Button
                    variant="secondary"
                    className="single-book-button"
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
