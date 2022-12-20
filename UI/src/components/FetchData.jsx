import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table, Spinner } from "react-bootstrap";

function FetchData() {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    populateBooksData();
  }, []);

  async function populateBooksData() {
    try {
      await axios.get("/api/authors").then((response) => {
        console.log("response", response.data.Data);
        setBooks(response.data.Data);
        setLoading(false);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  function renderBooksTable(books) {
    return (
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Title</th>
            <th>Release year</th>
            <th>Description</th>
            <th>Category</th>
          </tr>
        </thead>
        <tbody>
          {books.map((book) => (
            <tr key={book.Id}>
              <td>{book.Name}</td>
              <td>{book.BirthDate}</td>
              <td>{book.Name}</td>
              <td>{book.BirthDate}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  }

  const contents = loading ? (
    <div className="center">
      <Spinner animation="border" role="status" />
    </div>
  ) : (
    renderBooksTable(books)
  );

  return (
    <div>
      <h1 id="tabelLabel">Books</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );
}

export default FetchData;
