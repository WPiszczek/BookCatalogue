import { React } from "react";
import { Card, Button } from "react-bootstrap";
import "../css/Card.css";

function AddReviewCard(props) {
  const addReview = (event) => {
    event.preventDefault();
    props.addReview();
  };

  return (
    <Card border="dark" className="center review-card-container">
      <Button
        variant="success"
        className="center card-add-button"
        onClick={addReview}>
        Add new review
      </Button>
    </Card>
  );
}

export default AddReviewCard;
