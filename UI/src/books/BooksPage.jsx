import { React, useEffect, useState } from "react";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import BooksFilters from "./BooksFilters";
import BooksContainer from "./BooksContainer";

function BooksPage(props) {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchBooksData();
  }, []);

  async function fetchBooksData() {
    try {
      await axios.get("/api/books").then((response) => {
        console.log("response", response.data.Data);
        setBooks(response.data.Data);
        setLoading(false);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  return (
    <Container>
      <h1>Books</h1>
      {loading ? (
        <>
          <Spinner animation="border" role="status" />
        </>
      ) : (
        <>
          <BooksFilters />
          <BooksContainer books={books} />
        </>
      )}
    </Container>
  );
}

export default BooksPage;
