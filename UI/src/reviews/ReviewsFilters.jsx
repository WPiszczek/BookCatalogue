import { React, useState, useEffect } from "react";
import {
  Row,
  Col,
  Form,
  FloatingLabel,
  Button,
  Dropdown
} from "react-bootstrap";
import axios from "axios";

function ReviewsFilters(props) {
  const [books, setBooks] = useState([]);
  const [bookId, setBookId] = useState("");
  const [search, setSearch] = useState("");

  useEffect(() => {
    fetchBooksData();
  }, []);

  async function fetchBooksData() {
    try {
      await axios.get("/api/books").then((response) => {
        console.log("response", response.data.Data);
        setBooks(response.data.Data);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  const booksSelect = books.map((book) => {
    return (
      <option key={book.Id} value={book.Id}>
        {book.Author.Name}, {book.Title}, {book.ReleaseYear}
      </option>
    );
  });

  const filterReviews = (event) => {
    event.preventDefault();
    console.log("Filter reviews");
    console.log(bookId, search);
    props.fetchReviewsData(bookId, search);
  };

  const addReview = (event) => {
    event.preventDefault();
    console.log("Add review");
    props.addReview();
  };

  const sortReviews = (event, column = "Title", ascending = true) => {
    event.preventDefault();
    console.log("Sort reviews ", column, ascending);
    props.sortReviews(column, ascending);
  };

  return (
    <Form>
      <Row>
        <Col md={6}>
          <Row md={1}>
            <FloatingLabel label="Search" className="floating-label">
              <Form.Control
                required
                type="text"
                placeholder="Search content or description"
                value={search}
                name="Search"
                onChange={(e) => setSearch(e.target.value)}
              />
            </FloatingLabel>
          </Row>
          <Row md={1}>
            <FloatingLabel label="Book" className="floating-label">
              <Form.Select
                name="Book"
                onChange={(e) => setBookId(e.target.value)}>
                <option value={null} default></option>
                {booksSelect}
              </Form.Select>
            </FloatingLabel>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Button
              variant="primary"
              className="books-filters-button"
              onClick={filterReviews}>
              Filter reviews
            </Button>
            <Button
              variant="secondary"
              className="books-filters-button"
              onClick={addReview}>
              Add new review
            </Button>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Dropdown>
              <Dropdown.Toggle
                variant="success"
                className="books-filters-button">
                Sort reviews
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item
                  onClick={(e) => sortReviews(e, "DateAdded", true)}>
                  Sort by date ascending
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={(e) => sortReviews(e, "DateAdded", false)}>
                  Sort by date descending
                </Dropdown.Item>
                <Dropdown.Item onClick={(e) => sortReviews(e, "Rating", true)}>
                  Sort by rating ascending
                </Dropdown.Item>
                <Dropdown.Item onClick={(e) => sortReviews(e, "Rating", false)}>
                  Sort by rating descending
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Row>
        </Col>
      </Row>
    </Form>
  );
}

export default ReviewsFilters;
