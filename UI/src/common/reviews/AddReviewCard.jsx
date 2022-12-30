import { React } from "react";
import { Card, Button } from "react-bootstrap";
import "./AddReviewCard.css";

function AddReviewCard(props) {
  const addReview = (event) => {
    event.preventDefault();
    props.addReview();
  };

  return (
    <Card border="dark" className="center add-review-card-container">
      <Button
        variant="success"
        className="center add-review-card-button"
        onClick={addReview}>
        Add new review
      </Button>
    </Card>
  );
}

export default AddReviewCard;
