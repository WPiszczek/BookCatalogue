import React from "react";
import { Container } from "react-bootstrap";
import ReviewCard from "./ReviewCard";
import AddReviewCard from "../common/reviews/AddReviewCard";

function ReviewsContainer(props) {
  const reviews = props.reviews.map((review) => {
    return (
      <ReviewCard
        key={review.Id}
        review={review}
        editReview={props.editReview}
        deleteReview={props.deleteReview}
      />
    );
  });

  return (
    <Container fluid="md" className="container">
      {reviews}
      <AddReviewCard addReview={props.addReview} />
    </Container>
  );
}

export default ReviewsContainer;
