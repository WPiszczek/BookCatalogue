import { React, useState } from "react";
import {
  Collapse,
  Container,
  OverlayTrigger,
  Row,
  Tooltip
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleUp, faAngleDown } from "@fortawesome/free-solid-svg-icons";
import BookReviewCard from "./BookReviewCard";
import AddReviewCard from "../common/reviews/AddReviewCard";

function BookReviewsContainer(props) {
  const [collapse, setCollapse] = useState(false);

  const bookReviewsView = props.reviews.map((review) => {
    return (
      <BookReviewCard
        key={review.Id}
        review={review}
        editReview={props.editReview}
        deleteReview={props.deleteReview}
      />
    );
  });

  const addReview = () => {
    console.log("Add review");
    props.addReview();
  };

  return (
    <Container fluid="md">
      <Row md={1}>
        <OverlayTrigger
          placement="top"
          overlay={
            <Tooltip>
              {collapse ? "Click to uncollapse" : "Click to collapse"}
            </Tooltip>
          }>
          <h4
            className="pointer-on-hover container-header-string"
            onClick={() => setCollapse(!collapse)}>
            {props.headerString}{" "}
            <FontAwesomeIcon icon={collapse ? faAngleDown : faAngleUp} />
          </h4>
        </OverlayTrigger>
      </Row>
      <Collapse in={!collapse}>
        <Container>
          {bookReviewsView}
          <AddReviewCard addReview={addReview} />
        </Container>
      </Collapse>
    </Container>
  );
}

export default BookReviewsContainer;
