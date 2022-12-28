import { React, useState, useEffect } from "react";
import {
  Row,
  Col,
  Form,
  FloatingLabel,
  Button,
  Dropdown,
  DropdownButton,
} from "react-bootstrap";
import { BookCategoryMap } from "../utils/EnumMaps";
import axios from "axios";
import "./BooksFilters.css";

function BooksFilters(props) {
  const [authors, setAuthors] = useState([]);
  const [title, setTitle] = useState("");
  const [authorId, setAuthorId] = useState("");
  const [category, setCategory] = useState("");

  useEffect(() => {
    fetchAuthorsData();
  }, []);

  async function fetchAuthorsData() {
    try {
      await axios.get("/api/authors").then((response) => {
        console.log("response", response.data.Data);
        setAuthors(response.data.Data);
      });
    } catch (err) {
      console.log("ERROR");
      console.log(err.message);
    }
  }

  const authorsSelect = authors.map((author) => {
    return (
      <option key={author.Id} value={author.Id}>
        {author.Name}
      </option>
    );
  });

  const categorySelect = Object.keys(BookCategoryMap).map((key) => {
    return (
      <option key={key} value={key}>
        {BookCategoryMap[key]}
      </option>
    );
  });

  const filterBooks = (event) => {
    event.preventDefault();
    console.log("Filter books");
    console.log(title, authorId, category);
    props.fetchBooksData(title, authorId, category);
  };

  const addBook = (event) => {
    event.preventDefault();
    console.log("Add book");
    props.addBook();
  };

  const sortBooks = (event, column = "Title", ascending = true) => {
    event.preventDefault();
    console.log("Sort books ", column, ascending);
    props.sortBooks(column, ascending);
  };

  return (
    <Form>
      <Row>
        <Col md={6}>
          <Row md={1}>
            <FloatingLabel label="Title" className="floating-label">
              <Form.Control
                type="text"
                placeholder="Enter book title"
                name="Title"
                onChange={(e) => setTitle(e.target.value)}
              />
            </FloatingLabel>
          </Row>
          <Row md={2}>
            <FloatingLabel label="Author" className="floating-label">
              <Form.Select
                name="Author"
                onChange={(e) => setAuthorId(e.target.value)}>
                <option value={null} default></option>
                {authorsSelect}
              </Form.Select>
            </FloatingLabel>
            <FloatingLabel label="Category" className="floating-label">
              <Form.Select
                name="Category"
                onChange={(e) => setCategory(e.target.value)}>
                <option value={null} default></option>
                {categorySelect}
              </Form.Select>
            </FloatingLabel>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Button
              variant="primary"
              className="books-filters-button"
              onClick={filterBooks}>
              Filter books
            </Button>
            <Button
              variant="secondary"
              className="books-filters-button"
              onClick={addBook}>
              Add new book
            </Button>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Dropdown>
              <Dropdown.Toggle
                variant="success"
                className="books-filters-button">
                Sort books
              </Dropdown.Toggle>

              <Dropdown.Menu>
                <Dropdown.Item onClick={(e) => sortBooks(e, "Title", true)}>
                  Sort by title ascending
                </Dropdown.Item>
                <Dropdown.Item onClick={(e) => sortBooks(e, "Title", false)}>
                  Sort by title descending
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={(e) => sortBooks(e, "AverageRating", true)}>
                  Sort by rating ascending
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={(e) => sortBooks(e, "AverageRating", false)}>
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

export default BooksFilters;
