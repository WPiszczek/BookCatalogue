import { React } from "react";
import { Card, Col, Row, Container, Button } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import moment from "moment/moment";
import "./BookReviewCard.css";

function BookReviewCard(props) {
  const review = props.review;

  const editReview = () => {
    console.log("Edit review", review.Id);
    props.editReview(review.Id);
  };

  const deleteReview = () => {
    console.log("Delete review", review.Id);
    props.deleteReview(review.Id);
  };

  return (
    <Card border="dark" className="book-review-card-container">
      <Card.Header>
        <Row>
          <Col md={4}>{review.Reviewer}</Col>
          <Col className="book-review-card-header">
            <span className="italic">
              Added {moment(review.DateAdded).format("YYYY-MM-DD, HH:mm")}
            </span>
          </Col>
        </Row>
      </Card.Header>
      <Card.Body>
        <Card.Title className="book-card-rating">
          <FontAwesomeIcon icon={faStar} className="icon-star" />
          {review.Rating ?? "?"}
          /10
        </Card.Title>
        <Card.Title>{review.Title}</Card.Title>
      </Card.Body>
      <Card.Body>
        <Row>
          <Col md={9}>
            <Card.Text>{review.Content}</Card.Text>
          </Col>
          <Col md={3}>
            <Container className="book-review-button-container">
              <Button
                variant="primary"
                className="single-book-button"
                onClick={(e) => editReview(e)}>
                Edit review
              </Button>
              <Button
                variant="danger"
                className="single-book-button"
                onClick={(e) => deleteReview(e)}>
                Delete review
              </Button>
            </Container>
          </Col>
        </Row>
      </Card.Body>
    </Card>
  );
}

export default BookReviewCard;
