import { React, useEffect, useState } from "react";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import BooksFilters from "./BooksFilters";
import BooksContainer from "./BooksContainer";
import AddBookDialog from "../common/dialogs/books/AddBookDialog";
import { sortBooks } from "../utils/sortBooks";

function BooksPage() {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showAddBookDialog, setShowAddBookDialog] = useState(false);

  useEffect(() => {
    fetchBooksData();
  }, []);

  async function fetchBooksData(title = "", authorId = "", category = "") {
    try {
      await axios
        .get(
          `/api/books?title=${title}&authorId=${authorId}&category=${category}`
        )
        .then((response) => {
          console.log("response", response.data.Data);
          setBooks(response.data.Data);
          setLoading(false);
        });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  function callSortBooks(column, ascending) {
    console.log("Sort books from page", column, ascending);
    setBooks(sortBooks(books, column, ascending));
  }

  function addBook() {
    console.log("Add book from books page");
    setShowAddBookDialog(true);
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
          {showAddBookDialog && (
            <AddBookDialog
              show={true}
              close={() => setShowAddBookDialog(false)}
              fetchData={fetchBooksData}
            />
          )}
          <BooksFilters
            fetchBooksData={fetchBooksData}
            addBook={addBook}
            sortBooks={callSortBooks}
          />
          {books.length > 0 ? (
            <BooksContainer books={books} addBook={addBook} />
          ) : (
            <Container className="center margin-top">
              <i>Books not found.</i>
            </Container>
          )}
        </>
      )}
    </Container>
  );
}

export default BooksPage;
