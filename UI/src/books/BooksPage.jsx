import { React, useEffect, useState } from "react";
import { Container, Spinner } from "react-bootstrap";
import axios from "axios";
import BooksFilters from "./BooksFilters";
import BooksContainer from "./BooksContainer";
import AddBookDialog from "./AddBookDialog";

function BooksPage(props) {
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

  function sortBooks(column, ascending) {
    console.log("Sort books from page", column, ascending);
    const booksCopy = [...books];
    if (column == "Title") {
      booksCopy.sort((a, b) => {
        if (a.Title > b.Title) return ascending ? 1 : -1;
        if (a.Title < b.Title) return ascending ? -1 : 1;
        return 0;
      });
    } else if (column == "AverageRating") {
      booksCopy.sort((a, b) => {
        if (a.AverageRating === null) return 1;
        if (b.AverageRating === null) return -1;
        if (a.AverageRating > b.AverageRating) return ascending ? 1 : -1;
        if (a.AverageRating < b.AverageRating) return ascending ? -1 : 1;
        return 0;
      });
    }
    console.log(booksCopy);
    setBooks(booksCopy);
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
          {showAddBookDialog ? (
            <AddBookDialog
              show={true}
              close={() => setShowAddBookDialog(false)}
              fetchBooksData={fetchBooksData}
            />
          ) : (
            <></>
          )}
          <BooksFilters
            fetchBooksData={fetchBooksData}
            addBook={addBook}
            sortBooks={sortBooks}
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
