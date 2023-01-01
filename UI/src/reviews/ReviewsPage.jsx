import { React, useEffect, useState } from "react";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import ReviewsFilters from "./ReviewsFilters";
import ReviewsContainer from "./ReviewsContainer";
import AddReviewDialog from "../common/dialogs/reviews/AddReviewDialog";
import EditReviewDialog from "../common/dialogs/reviews/EditReviewDialog";
import DeleteReviewDialog from "../common/dialogs/reviews/DeleteReviewDialog";
import { sortReviews } from "../utils/sortReviews";

function ReviewsPage() {
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [review, setReview] = useState("");
  const [reviewId, setReviewId] = useState("");

  const [showAddReviewDialog, setShowAddReviewDialog] = useState(false);
  const [showEditReviewDialog, setShowEditReviewDialog] = useState(false);
  const [showDeleteReviewDialog, setShowDeleteReviewDialog] = useState(false);

  useEffect(() => {
    fetchReviewsData();
  }, []);

  async function fetchReviewsData(bookId = "", search = "") {
    try {
      await axios
        .get(`/api/reviews?bookId=${bookId}&search=${search}`)
        .then((response) => {
          console.log("response", response.data.Data);
          setReviews(response.data.Data);
          setLoading(false);
        });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  function callSortReviews(column, ascending) {
    console.log("Sort reviews from page", column, ascending);
    setReviews(sortReviews(reviews, column, ascending));
  }

  function addReview() {
    console.log("Add review from reviews page");
    setShowAddReviewDialog(true);
  }

  function editReview(reviewId) {
    console.log("Edit review from reviews page", reviewId);
    const _review = reviews.find((review) => {
      return review.Id === reviewId;
    });
    setReview(_review);
    setShowEditReviewDialog(true);
  }

  function deleteReview(reviewId) {
    console.log("Delete review from reviews page", reviewId);
    setReviewId(reviewId);
    setShowDeleteReviewDialog(true);
  }

  const hideDeleteReviewDialog = () => {
    setShowDeleteReviewDialog(false);
  };

  return (
    <Container>
      <h1>Reviews</h1>
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          {showAddReviewDialog && (
            <AddReviewDialog
              show={true}
              close={() => setShowAddReviewDialog(false)}
              fetchData={fetchReviewsData}
            />
          )}
          {showEditReviewDialog && (
            <EditReviewDialog
              show={true}
              close={() => setShowEditReviewDialog(false)}
              review={review}
              fetchData={fetchReviewsData}
            />
          )}
          {showDeleteReviewDialog && (
            <DeleteReviewDialog
              show={true}
              close={() => setShowDeleteReviewDialog(false)}
              reviewId={reviewId}
              fetchData={fetchReviewsData}
              hideDeleteReviewDialog={hideDeleteReviewDialog}
            />
          )}
          <ReviewsFilters
            fetchReviewsData={fetchReviewsData}
            addReview={addReview}
            sortReviews={callSortReviews}
          />
          {reviews.length > 0 ? (
            <ReviewsContainer
              reviews={reviews}
              addReview={addReview}
              editReview={editReview}
              deleteReview={deleteReview}
            />
          ) : (
            <Container className="center margin-top">
              <i>Reviews not found.</i>
            </Container>
          )}
        </>
      )}
    </Container>
  );
}

export default ReviewsPage;
