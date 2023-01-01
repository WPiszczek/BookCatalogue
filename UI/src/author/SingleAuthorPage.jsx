import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import SingleAuthorCard from "./SingleAuthorCard";
import OtherBooksContainer from "../common/otherBooks/OtherBooksContainer";
import AddBookDialog from "../common/books/AddBookDialog";

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
    // TODO
  };

  const editAuthorImage = () => {
    console.log("Edit author image from page", author.Id);
    setShowEditAuthorImageDialog(true);
    // TODO
  };

  const deleteAuthor = () => {
    console.log("Delete author from page", author.Id);
    setShowDeleteAuthorDialog(true);
    // TODO
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
      {/* {showEditAuthorDialog && (
        <EditAuthorDialog
          show={true}
          close={() => setShowEditAuthorDialog(false)}
          author={author}
          fetchSingleAuthorData={fetchSingleAuthorData}
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
      )} */}
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
            <OtherBooksContainer
              books={author.Books}
              author={author}
              headerString={`See also books from ${author.Name}:`}
            />
          )}
        </>
      )}
    </Container>
  );
}

export default SingleAuthorPage;
