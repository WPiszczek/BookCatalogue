import { React, useState, useEffect } from "react";
import {
  Button,
  Modal,
  Row,
  Col,
  FloatingLabel,
  Form,
  Alert,
  InputGroup
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import axios from "axios";

function EditReviewDialog(props) {
  const id = props.review.Id;
  const [show, setShow] = useState(props.show);
  const [title, setTitle] = useState(props.review.Title);
  const [bookId, setBookId] = useState(props.review.BookId);
  const [reviewer, setReviewer] = useState(props.review.Reviewer);
  const [content, setContent] = useState(props.review.Content);
  const [rating, setRating] = useState(props.review.Rating);
  const [responseMessage, setResponseMessage] = useState();
  const [books, setBooks] = useState([]);

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

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const addReview = async (event) => {
    event.preventDefault();
    const review = {
      Title: title,
      BookId: parseInt(bookId),
      Rating: parseInt(rating),
      Content: content,
      Reviewer: reviewer
    };
    console.log("Review ", review);

    await axios
      .put(`/api/reviews/${id}`, review)
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

  const booksSelect = books.map((book) => {
    return (
      <option key={book.Id} value={book.Id}>
        {book.Author.Name}, {book.Title}, {book.ReleaseYear}
      </option>
    );
  });

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Edit review</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row md={1}>
          <FloatingLabel label="Book" className="floating-label">
            <Form.Select
              required
              name="Book"
              value={bookId}
              onChange={(e) => setBookId(e.target.value)}>
              <option value={null} default></option>
              {booksSelect}
            </Form.Select>
          </FloatingLabel>
        </Row>
        <Row>
          <Col md={8}>
            <FloatingLabel label="Your name" className="floating-label">
              <Form.Control
                required
                type="text"
                placeholder="Enter your name"
                value={reviewer}
                name="Reviewer"
                onChange={(e) => setReviewer(e.target.value)}
              />
            </FloatingLabel>
          </Col>
          <Col md={4} className="center">
            <InputGroup className="floating-label">
              <InputGroup.Text>
                <FontAwesomeIcon icon={faStar} className="icon-star" />
              </InputGroup.Text>
              <Form.Control
                required
                type="number"
                min={0}
                max={10}
                placeholder="Enter rating"
                value={rating}
                name="Rating"
                onChange={(e) => setRating(e.target.value)}
              />
              <InputGroup.Text>/10</InputGroup.Text>
            </InputGroup>
          </Col>
        </Row>
        <Row md={1}>
          <FloatingLabel label="Title" className="floating-label">
            <Form.Control
              type="text"
              value={title}
              placeholder="Enter review title"
              name="Title"
              onChange={(e) => setTitle(e.target.value)}
            />
          </FloatingLabel>
        </Row>
        <Row md={1}>
          <FloatingLabel label="Content" className="floating-label">
            <Form.Control
              as="textarea"
              style={{ height: "200px" }}
              value={content}
              placeholder="Enter review content"
              name="Content"
              onChange={(e) => setContent(e.target.value)}
            />
          </FloatingLabel>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={addReview}>
          Confirm
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default EditReviewDialog;