import { React } from "react";
import { Card, Col, Row, Container, Button, Image } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import moment from "moment/moment";
import { roundToTwo } from "../utils/roundToTwo";
import "../common/css/Card.css";
import { useNavigate } from "react-router-dom";

function ReviewCard(props) {
  const review = props.review;
  const navigate = useNavigate();

  const editReview = () => {
    console.log("Edit review", review.Id);
    props.editReview(review.Id);
  };

  const deleteReview = () => {
    console.log("Delete review", review.Id);
    props.deleteReview(review.Id);
  };

  return (
    <Card border="dark" className="review-card-container">
      <Card.Header>
        <Row>
          <Col md={4}>{review.Reviewer}</Col>
          <Col className="review-card-header">
            <span className="italic">
              Added {moment(review.DateAdded).format("YYYY-MM-DD, HH:mm")}
            </span>
          </Col>
        </Row>
      </Card.Header>
      <Row>
        <Col
          md={2}
          className="margin-right pointer-on-hover"
          onClick={() => navigate(`/books/${review.Book.Id}`)}>
          <Card.Body>
            <Card.Title>{review.Book.Title}</Card.Title>
            <Card.Subtitle>
              {review.Book.Author.Name}, {review.Book.ReleaseYear}
            </Card.Subtitle>
          </Card.Body>
          <Container className="center">
            <Card.Img
              as={Image}
              variant="top"
              src={`/api/image/books/${review.Book.ImageUrl}`}
              alt={<FontAwesomeIcon icon="fa-solid fa-image" />}
              fluid="md"
              className="card-img"
            />
          </Container>
        </Col>
        <Col md={10}>
          <Container fluid="md">
            <Card.Body>
              <Card.Title className="card-rating">
                <FontAwesomeIcon icon={faStar} className="icon-star" />
                {review.Rating ? roundToTwo(review.Rating) : "?"}
                /10
              </Card.Title>
              <Card.Title>{review.Title}</Card.Title>
            </Card.Body>
            <Card.Body>
              <Row>
                <Col md={9}>
                  <Card.Text>
                    <pre style={{ whiteSpace: "pre-wrap", font: "inherit" }}>
                      {review.Content}
                    </pre>
                  </Card.Text>
                </Col>
                <Col md={3}>
                  <Container className="review-button-container">
                    <Button
                      variant="primary"
                      className="single-button"
                      onClick={(e) => editReview(e)}>
                      Edit review
                    </Button>
                    <Button
                      variant="danger"
                      className="single-button"
                      onClick={(e) => deleteReview(e)}>
                      Delete review
                    </Button>
                  </Container>
                </Col>
              </Row>
            </Card.Body>
          </Container>
        </Col>
      </Row>
    </Card>
  );
}

export default ReviewCard;
