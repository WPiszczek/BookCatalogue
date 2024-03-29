import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import SingleAuthorCard from "./SingleAuthorCard";
import OtherBooksContainer from "../common/otherBooks/OtherBooksContainer";
import AddBookDialog from "../common/dialogs/books/AddBookDialog";
import EditAuthorDialog from "../common/dialogs/authors/EditAuthorDialog";
import EditAuthorImageDialog from "../common/dialogs/authors/EditAuthorImageDialog";
import DeleteAuthorDialog from "../common/dialogs/authors/DeleteAuthorDialog";
import { sortBooks } from "../utils/sortBooks";

function SingleAuthorPage() {
  const { id } = useParams();
  const [author, setAuthor] = useState({});
  const [loading, setLoading] = useState(true);

  const [showAddBookDialog, setShowAddBookDialog] = useState(false);
  const [showEditAuthorDialog, setShowEditAuthorDialog] = useState(false);
  const [showEditAuthorImageDialog, setShowEditAuthorImageDialog] =
    useState(false);
  const [showDeleteAuthorDialog, setShowDeleteAuthorDialog] = useState(false);

  const navigate = useNavigate();

  async function fetchSingleAuthorData() {
    try {
      await axios.get(`/api/authors/${id}`).then((response) => {
        console.log("response", response.data.Data);
        setAuthor(response.data.Data);
        setLoading(false);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  useEffect(() => {
    fetchSingleAuthorData();
  }, [id]);

  const addBook = () => {
    console.log("Add book from page");
    setShowAddBookDialog(true);
  };

  const editAuthor = () => {
    console.log("Edit author from page", author.Id);
    setShowEditAuthorDialog(true);
  };

  const editAuthorImage = () => {
    console.log("Edit author image from page", author.Id);
    setShowEditAuthorImageDialog(true);
  };

  const deleteAuthor = () => {
    console.log("Delete author from page", author.Id);
    setShowDeleteAuthorDialog(true);
  };

  return (
    <Container>
      <h1 className="pointer-on-hover" onClick={() => navigate("/authors")}>
        Authors
      </h1>
      {showAddBookDialog && (
        <AddBookDialog
          show={true}
          close={() => setShowAddBookDialog(false)}
          authorId={author.Id}
          fetchData={fetchSingleAuthorData}
        />
      )}
      {showEditAuthorDialog && (
        <EditAuthorDialog
          show={true}
          close={() => setShowEditAuthorDialog(false)}
          author={author}
          fetchData={fetchSingleAuthorData}
        />
      )}
      {showEditAuthorImageDialog && (
        <EditAuthorImageDialog
          show={true}
          close={() => setShowEditAuthorImageDialog(false)}
          authorId={author.Id}
          authorName={author.Name}
          fetchSingleAuthorData={fetchSingleAuthorData}
        />
      )}
      {showDeleteAuthorDialog && (
        <DeleteAuthorDialog
          show={true}
          close={() => setShowDeleteAuthorDialog(false)}
          authorId={author.Id}
          authorName={author.Name}
        />
      )}
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          <SingleAuthorCard
            author={author}
            addBook={addBook}
            editAuthor={editAuthor}
            editAuthorImage={editAuthorImage}
            deleteAuthor={deleteAuthor}
          />
          {author.Books.length > 0 && (
            <>
              <hr />
              <OtherBooksContainer
                books={sortBooks(author.Books, "AverageRating", false)}
                author={author}
                headerString={`See books from ${author.Name}:`}
              />
            </>
          )}
        </>
      )}
    </Container>
  );
}

export default SingleAuthorPage;
