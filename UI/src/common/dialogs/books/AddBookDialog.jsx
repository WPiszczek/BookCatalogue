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

function AddBookDialog(props) {
  const [show, setShow] = useState(props.show);
  const [authors, setAuthors] = useState([]);
  const [title, setTitle] = useState("");
  const [releaseYear, setReleaseYear] = useState(2000);
  const [description, setDescription] = useState("");
  const [authorId, setAuthorId] = useState(props.authorId ?? "");
  const [category, setCategory] = useState("");
  const [image, setImage] = useState();
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

  const validateAddBook = () => {
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
    if (!(category.toString())) {
      setResponseMessage(<Alert variant="danger">Category is required.</Alert>);
      return false;
    }
    if (!image) {
      setResponseMessage(<Alert variant="danger">Picture is required.</Alert>);
      return false;
    }
    return true;
  };

  const addBook = async (event) => {
    event.preventDefault();
    if (!validateAddBook()) return;

    const book = {
      Title: title,
      AuthorId: parseInt(authorId),
      ReleaseYear: parseInt(releaseYear),
      Description: description,
      Category: parseInt(category)
    };
    console.log("Book ", book);
    console.log("Image ", image);

    const formData = new FormData();
    formData.append("Image", image);
    formData.append("Json", JSON.stringify(book));

    await axios
      .post("/api/books", formData, {
        "Content-Type": "multipart/form-data"
      })
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          props.fetchData();
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
        <Modal.Title>Add new book</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={2}>
          <FloatingLabel label="Title" className="floating-label">
            <Form.Control
              required
              type="text"
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
              name="Description"
              onChange={(e) => setDescription(e.target.value)}
            />
          </FloatingLabel>
        </Row>
        <Row md={1}>
          <FloatingLabel label="Image" className="floating-label">
            <Form.Control
              required
              type="file"
              name="Image"
              onChange={(e) => setImage(e.target.files[0])}
            />
          </FloatingLabel>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="success" onClick={addBook}>
          Add book
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddBookDialog;
