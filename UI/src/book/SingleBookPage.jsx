import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import SingleBookCard from "./SingleBookCard";
import OtherBooksContainer from "../common/otherBooks/OtherBooksContainer";
import BookReviewsContainer from "./BookReviewsContainer";
import AddReviewDialog from "../common/dialogs/reviews/AddReviewDialog";
import EditReviewDialog from "../common/dialogs/reviews/EditReviewDialog";
import DeleteReviewDialog from "../common/dialogs/reviews/DeleteReviewDialog";
import EditBookDialog from "../common/dialogs/books/EditBookDialog";
import EditBookImageDialog from "../common/dialogs/books/EditBookImageDialog";
import DeleteBookDialog from "../common/dialogs/books/DeleteBookDialog";
import { sortBooks } from "../utils/sortBooks";

function SingleBookPage() {
  const { id } = useParams();
  const [book, setBook] = useState({});
  const [loading, setLoading] = useState(true);
  const [review, setReview] = useState("");
  const [reviewId, setReviewId] = useState("");

  const [showEditBookDialog, setShowEditBookDialog] = useState(false);
  const [showEditBookImageDialog, setShowEditBookImageDialog] = useState(false);
  const [showDeleteBookDialog, setShowDeleteBookDialog] = useState(false);
  const [showAddReviewDialog, setShowAddReviewDialog] = useState(false);
  const [showEditReviewDialog, setShowEditReviewDialog] = useState(false);
  const [showDeleteReviewDialog, setShowDeleteReviewDialog] = useState(false);

  const navigate = useNavigate();

  async function fetchSingleBookData() {
    try {
      await axios.get(`/api/books/${id}`).then((response) => {
        console.log("response", response.data.Data);
        setBook(response.data.Data);
        setLoading(false);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  useEffect(() => {
    fetchSingleBookData();
  }, [id]);

  const addReview = () => {
    console.log("Add review from page");
    setShowAddReviewDialog(true);
  };

  const editReview = (reviewId) => {
    console.log("Edit review from page", reviewId);
    const _review = book.Reviews.find((review) => {
      return review.Id === reviewId;
    });
    setReview(_review);
    setShowEditReviewDialog(true);
  };

  const deleteReview = (reviewId) => {
    console.log("Delete review from page", reviewId);
    setReviewId(reviewId);
    setShowDeleteReviewDialog(true);
  };

  const editBook = () => {
    console.log("Edit book from page", book.Id);
    setShowEditBookDialog(true);
  };

  const editBookImage = () => {
    console.log("Edit book image from page", book.Id);
    setShowEditBookImageDialog(true);
  };

  const deleteBook = () => {
    console.log("Delete book from page", book.Id);
    setShowDeleteBookDialog(true);
  };

  const hideDeleteReviewDialog = () => {
    setShowDeleteReviewDialog(false);
  };

  return (
    <Container>
      <h1 className="pointer-on-hover" onClick={() => navigate("/books")}>
        Books
      </h1>
      {showAddReviewDialog && (
        <AddReviewDialog
          show={true}
          close={() => setShowAddReviewDialog(false)}
          bookId={book.Id}
          fetchData={fetchSingleBookData}
        />
      )}
      {showEditReviewDialog && (
        <EditReviewDialog
          show={true}
          close={() => setShowEditReviewDialog(false)}
          review={review}
          fetchData={fetchSingleBookData}
        />
      )}
      {showDeleteReviewDialog && (
        <DeleteReviewDialog
          show={true}
          close={() => setShowDeleteReviewDialog(false)}
          reviewId={reviewId}
          fetchData={fetchSingleBookData}
          hideDeleteReviewDialog={hideDeleteReviewDialog}
        />
      )}
      {showEditBookDialog && (
        <EditBookDialog
          show={true}
          close={() => setShowEditBookDialog(false)}
          book={book}
          fetchSingleBookData={fetchSingleBookData}
        />
      )}
      {showEditBookImageDialog && (
        <EditBookImageDialog
          show={true}
          close={() => setShowEditBookImageDialog(false)}
          bookId={book.Id}
          bookTitle={book.Title}
          fetchSingleBookData={fetchSingleBookData}
        />
      )}
      {showDeleteBookDialog && (
        <DeleteBookDialog
          show={true}
          close={() => setShowDeleteBookDialog(false)}
          bookId={book.Id}
          bookTitle={book.Title}
        />
      )}
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          <SingleBookCard
            book={book}
            addReview={addReview}
            editBook={editBook}
            editBookImage={editBookImage}
            deleteBook={deleteBook}
          />
          {book.Author.Books.length > 0 && (
            <>
              <hr />
              <OtherBooksContainer
                books={sortBooks(book.Author.Books, "AverageRating", false)}
                author={book.Author}
                headerString={`See also from ${book.Author.Name}:`}
              />
            </>
          )}
          {book.Reviews.length > 0 && (
            <>
              <hr />
              <BookReviewsContainer
                reviews={book.Reviews}
                headerString={`Reviews of "${book.Title}":`}
                addReview={addReview}
                editReview={editReview}
                deleteReview={deleteReview}
              />
            </>
          )}
        </>
      )}
    </Container>
  );
}

export default SingleBookPage;
