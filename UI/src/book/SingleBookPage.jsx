import { React, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import SingleBookCard from "./SingleBookCard";
import SeeAlsoContainer from "./SeeAlsoContainer";

function SingleBookPage() {
  const { id } = useParams();
  const [book, setBook] = useState({});
  const [loading, setLoading] = useState(true);

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
    // TODO
  };

  const deleteBook = () => {
    console.log("Delete book from page", book.Id);
    // TODO
  };

  return (
    <Container>
      <h1 className="pointer-on-hover" onClick={() => navigate("/books")}>
        Books
      </h1>
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          <SingleBookCard
            book={book}
            editBook={editBook}
            deleteBook={deleteBook}
          />
          {book.Author.Books.length > 0 && (
            <SeeAlsoContainer books={book.Author.Books} author={book.Author} />
          )}
        </>
      )}
    </Container>
  );
}

export default SingleBookPage;
