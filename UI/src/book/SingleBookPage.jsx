import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import SingleBookCard from "./SingleBookCard";
import EditBookDialog from "./EditBookDialog";
import EditBookImageDialog from "./EditBookImageDialog";
import DeleteBookDialog from "./DeleteBookDialog";
import OtherBooksContainer from "../common/otherBooks/OtherBooksContainer";

function SingleBookPage() {
  const { id } = useParams();
  const [book, setBook] = useState({});
  const [loading, setLoading] = useState(true);

  const [showEditBookDialog, setShowEditBookDialog] = useState(false);
  const [showEditBookImageDialog, setShowEditBookImageDialog] = useState(false);
  const [showDeleteBookDialog, setShowDeleteBookDialog] = useState(false);

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

  return (
    <Container>
      <h1 className="pointer-on-hover" onClick={() => navigate("/books")}>
        Books
      </h1>
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
            editBook={editBook}
            editBookImage={editBookImage}
            deleteBook={deleteBook}
          />
          {book.Author.Books.length > 0 && (
            <OtherBooksContainer
              books={book.Author.Books}
              author={book.Author}
              headerString={`See also from ${book.Author.Name}:`}
            />
          )}
        </>
      )}
    </Container>
  );
}

export default SingleBookPage;
