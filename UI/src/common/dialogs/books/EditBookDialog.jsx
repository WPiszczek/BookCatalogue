import { React, useState, useEffect } from "react";
import {
  Button,
  Modal,
  Row,
  FloatingLabel,
  Form,
  Alert
} from "react-bootstrap";
import axios from "axios";
import { BookCategoryMap } from "../../../utils/EnumMaps";

function EditBookDialog(props) {
  const bookId = props.book.Id;
  const [show, setShow] = useState(props.show);
  const [authors, setAuthors] = useState([]);
  const [title, setTitle] = useState(props.book.Title);
  const [releaseYear, setReleaseYear] = useState(props.book.ReleaseYear);
  const [description, setDescription] = useState(props.book.Description);
  const [authorId, setAuthorId] = useState(props.book.AuthorId);
  const [category, setCategory] = useState(props.book.Category);
  const [responseMessage, setResponseMessage] = useState();

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

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const validateEditBook = () => {
    if (!title) {
      setResponseMessage(<Alert variant="danger">Title is required.</Alert>);
      return false;
    }
    if (!releaseYear) {
      setResponseMessage(
        <Alert variant="danger">Release year is required.</Alert>
      );
      return false;
    }
    if (!authorId) {
      setResponseMessage(<Alert variant="danger">Author is required.</Alert>);
      return false;
    }
    if (!category.toString()) {
      setResponseMessage(<Alert variant="danger">Category is required.</Alert>);
      return false;
    }
    return true;
  };

  const editBook = async (event) => {
    event.preventDefault();
    if (!validateEditBook()) return;

    const book = {
      Title: title,
      AuthorId: parseInt(authorId),
      ReleaseYear: parseInt(releaseYear),
      Description: description,
      Category: parseInt(category)
    };
    console.log("Book ", book);

    await axios
      .put(`/api/books/${bookId}`, book)
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          props.fetchSingleBookData();
        } else {
          console.log("Fail ", response.data.Message);
          setResponseMessage(
            <Alert variant="danger">{response.data.Message}</Alert>
          );
        }
      })
      .catch((error) => {
        console.log(error.message);
        setResponseMessage(
          <Alert variant="danger">Something went wrong. Try again.</Alert>
        );
      });
  };

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

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Edit book</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={2}>
          <FloatingLabel label="Title" className="floating-label">
            <Form.Control
              required
              type="text"
              placeholder="Enter book title"
              value={title}
              name="Title"
              onChange={(e) => setTitle(e.target.value)}
            />
          </FloatingLabel>
          <FloatingLabel label="Release Year" className="floating-label">
            <Form.Control
              required
              type="number"
              min={900}
              max={new Date().getFullYear()}
              value={releaseYear}
              placeholder="Enter release year"
              name="ReleaseYear"
              onChange={(e) => setReleaseYear(e.target.value)}
            />
          </FloatingLabel>
        </Row>
        <Row md={2}>
          <FloatingLabel label="Author" className="floating-label">
            <Form.Select
              required
              name="Author"
              value={authorId}
              onChange={(e) => setAuthorId(e.target.value)}>
              <option value={null} default></option>
              {authorsSelect}
            </Form.Select>
          </FloatingLabel>
          <FloatingLabel label="Category" className="floating-label">
            <Form.Select
              required
              name="Category"
              value={category}
              onChange={(e) => setCategory(e.target.value)}>
              <option value={null} default></option>
              {categorySelect}
            </Form.Select>
          </FloatingLabel>
        </Row>
        <Row md={1}>
          <FloatingLabel label="Description" className="floating-label">
            <Form.Control
              as="textarea"
              style={{ height: "200px" }}
              value={description}
              placeholder="Enter book description"
              name="Description"
              onChange={(e) => setDescription(e.target.value)}
            />
          </FloatingLabel>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={editBook}>
          Confirm
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default EditBookDialog;
